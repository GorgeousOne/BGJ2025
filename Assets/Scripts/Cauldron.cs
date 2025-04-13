using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public SpriteRenderer liquidSprite, liquidTextureSprite;
    public GameObject splashVFX;
    public Color startColor;
    LevelManager level;
    List<IngredientList> currentIngredients = new();
    Vector3 originalRot;
    float currentValue = 0.5f, currentIngredientsAmount;
    void Start()
    {
        level = LevelManager.instance;
        originalRot = transform.rotation.eulerAngles;
    }

    void FixedUpdate()
    {
        if(currentValue < 0.5f){
            float magnitude = 1 - currentValue * 2;
            float z = Random.Range(-2f + currentValue * 2, 2f - currentValue * 2) * magnitude;
            transform.rotation = Quaternion.Euler(new Vector3(originalRot.x,originalRot.y, originalRot.z + z));
        }
    }

    public void OnTriggerEnter2D(Collider2D other) 
    {
        Ingredient currentIngredient = other.GetComponent<Drop>().ingredient;
        if(string.Compare(other.tag, "Ingredient") == 0){
            currentIngredientsAmount++;
            for (int i = 0; i < currentIngredients.Count; i++){
                if(currentIngredient == currentIngredients[i].ingredient){
                    currentIngredients[i].amount++;
                    CheckIngredients(currentIngredients[i]);
                }
            }
            CheckPercentages();
            if(currentIngredientsAmount > level.currentPotion.maxIngredients){ currentValue = 0.5f; level.FailLevel(); }
        }
        Instantiate(splashVFX, other.transform.position + new Vector3(0f, 0f, -1f), Quaternion.identity);
        LeanTween.cancel(other.gameObject);
        Destroy(other.gameObject);
    }

    public void PrepareIngredients(LevelIngredient[] _ingredient)
    {
        ResetCauldron();
        foreach (LevelIngredient item in _ingredient){
            float amount = _ingredient.Length;
            IngredientList listElement = new(){
                ingredient = item.ingredient,
                amount = 1,
                percentage = 1 / amount
            };
            currentIngredients.Add(listElement);
            currentIngredientsAmount++;
        }
    }

    public void ResetCauldron()
    {
        currentIngredients.Clear();
        currentIngredientsAmount = 0;
        currentValue = 0.5f;
        liquidSprite.color = startColor;
        liquidTextureSprite.color = startColor;
    }

    void CheckPercentages()
    {
        for (int i = 0; i < currentIngredients.Count; i++){
            currentIngredients[i].percentage = currentIngredients[i].amount / currentIngredientsAmount;
        }
        bool wrongPercentage = false;
        for (int i = 0; i < currentIngredients.Count; i++){
            if(currentIngredients[i].percentage >= level.currentPotion.ingredients[i].correctAmount - level.currentPotion.errorMargin 
                && currentIngredients[i].percentage <= level.currentPotion.ingredients[i].correctAmount + level.currentPotion.errorMargin){
                //Percentage correct
            }
            else{ wrongPercentage = true; }
        }
        if (!wrongPercentage){ level.CompleteLevel(); }
    }


    void CheckIngredients(IngredientList ingredient)
    {
        for (int i = 0; i < level.currentPotion.ingredients.Length; i++){
            if(ingredient.ingredient == level.currentPotion.ingredients[i].ingredient){
                if(ingredient.percentage < level.currentPotion.ingredients[i].correctAmount){ RightColor(); }
                else{ WrongColor(); }
            }
        }
    }

    void WrongColor()
    {
        currentValue -= 0.1f;
        if(currentValue < 0){ level.FailLevel(); }
        Color lerpColor = Color.Lerp(level.currentPotion.wrongColor, level.currentPotion.correctColor, currentValue);
        LeanTween.color(liquidSprite.gameObject, lerpColor, 0.5f);
        LeanTween.color(liquidTextureSprite.gameObject, lerpColor, 0.5f);
    }

    void RightColor()
    {
        currentValue += 0.1f;
        Color lerpColor = Color.Lerp(level.currentPotion.wrongColor, level.currentPotion.correctColor, currentValue);
        LeanTween.color(liquidSprite.gameObject, lerpColor, 0.5f);
        LeanTween.color(liquidTextureSprite.gameObject, lerpColor, 0.5f);
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalRot = transform.rotation.eulerAngles;
        float elapsed = 0.0f;

        while(elapsed < duration){
            float z = Random.Range(-1f, 1f) * magnitude;

            transform.rotation = Quaternion.Euler(new Vector3(originalRot.x,originalRot.y, originalRot.z + z));

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.rotation = Quaternion.Euler(originalRot);
    }
}

[System.Serializable]
public class IngredientList
{
    public Ingredient ingredient;
    public int amount;
    public float percentage;
}