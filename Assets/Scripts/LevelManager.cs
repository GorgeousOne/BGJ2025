using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LevelManager : MonoBehaviour
{
    public Ingredient[] levelIngredients;
    public LevelIngredient[] ingredients;
    public int maxIngredients;
    public float errorMargin;
    public Color correctColor, wrongColor;
    public Volume globalVolume;
    ColorAdjustments colorAdjustments;
    public Slot[] slots;
    public static LevelManager instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        for(int i = 0; i < levelIngredients.Length; i++){
            slots[i].SetItem(levelIngredients[i]);
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
        LeanTween.value(gameObject, 0f, 6f, 0.1f).setOnUpdate((float val) => { colorAdjustments.postExposure.Override(val); } );
        yield return new WaitForSeconds(0.5f);
        LeanTween.value(gameObject, 6f, 0f, 8f).setOnUpdate((float val) => { colorAdjustments.postExposure.Override(val); } );
    }
}

[System.Serializable]
public class LevelIngredient
{
    public Ingredient ingredient;
    public float correctAmount;
}