using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LevelManager : MonoBehaviour
{
    public Potion currentPotion;
    public Volume globalVolume;
    public GameObject endscreen;
    public CanvasGroup uiEndScreen;
    ColorAdjustments colorAdjustments;
    public Slot[] slots;
    public Cauldron cauldron;
    public Camera mainCam;
    int index;
    LevelSelection levelselection;
    public static LevelManager instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        levelselection = LevelSelection.instance;
        globalVolume.profile.TryGet(out colorAdjustments);
    }

    public void StartLevel(int _index)
    {
        index = _index;
        for(int i = 0; i < currentPotion.ingredients.Length; i++){ slots[i].SetItem(currentPotion.ingredients[i].ingredient); }
        cauldron.PrepareIngredients(currentPotion.ingredients);
    }

    public void FailLevel()
    {
        StartCoroutine(Flashbang());
    }

    public void CompleteLevel()
    {
        Debug.Log("Level complete");
        levelselection.LevelComplete(index);
    }

    IEnumerator Flashbang()
    {
        LeanTween.value(gameObject, 0f, 9f, 0.1f).setOnUpdate((float val) => { colorAdjustments.postExposure.Override(val); } );
        StartCoroutine(Screenshake(3f, 0.3f));
        yield return new WaitForSeconds(1f);
        endscreen.SetActive(true);
        uiEndScreen.gameObject.SetActive(true);
        LeanTween.value(gameObject, 6f, 0f, 6f).setOnUpdate((float val) => { colorAdjustments.postExposure.Override(val); } );
        yield return new WaitForSeconds(2f);
        LeanTween.alphaCanvas(uiEndScreen, 1f, 1f);
    }

    public IEnumerator Screenshake(float duration, float magnitude)
    {
        Vector3 originalPos = mainCam.transform.localPosition;
        float elapsed = 0.0f;

        while(elapsed < duration){
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            mainCam.transform.localPosition = new Vector3(originalPos.x + x,originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        mainCam.transform.localPosition = originalPos;
    }
}

[System.Serializable]
public class LevelIngredient
{
    public Ingredient ingredient;
    public float correctAmount;
}