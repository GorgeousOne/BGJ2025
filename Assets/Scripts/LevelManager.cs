using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LevelManager : MonoBehaviour
{
    public Potion currentPotion;
    public Volume globalVolume;
    public GameObject endscreen;
    ColorAdjustments colorAdjustments;
    public Slot[] slots;
    public static LevelManager instance;

    void Awake()
    {
        instance = this;
    }

    public void StartLevel()
    {
        for(int i = 0; i < currentPotion.ingredients.Length; i++){
            slots[i].SetItem(currentPotion.ingredients[i].ingredient);
        }
        globalVolume.profile.TryGet(out colorAdjustments);
    }

    public void FailLevel()
    {
        StartCoroutine(Flashbang());
    }

    public void CompleteLevel()
    {
        // show book
    }

    IEnumerator Flashbang()
    {
        LeanTween.value(gameObject, 0f, 9f, 0.1f).setOnUpdate((float val) => { colorAdjustments.postExposure.Override(val); } );
        yield return new WaitForSeconds(0.5f);
        endscreen.SetActive(true);
        LeanTween.value(gameObject, 6f, 0f, 6f).setOnUpdate((float val) => { colorAdjustments.postExposure.Override(val); } );
    }
}

[System.Serializable]
public class LevelIngredient
{
    public Ingredient ingredient;
    public float correctAmount;
}