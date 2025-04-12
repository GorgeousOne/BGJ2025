using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public SpriteRenderer cauldronSprite;
    LevelManager level;
    List<IngredientList> currentIngredients = new();
    float currentValue = 0.5f, currentIngredientsAmount;
    void Start()
    {
        level = LevelManager.instance;
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
            if(currentIngredientsAmount > level.currentPotion.maxIngredients){ level.FailLevel(); }
        }
        Destroy(other.gameObject);
    }

    public void PrepareIngredients(LevelIngredient[] _ingredient)
    {
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
        LeanTween.color(gameObject, lerpColor, 0.5f);
    }

    void RightColor()
    {
        currentValue += 0.1f;
        Color lerpColor = Color.Lerp(level.currentPotion.wrongColor, level.currentPotion.correctColor, currentValue);
        LeanTween.color(gameObject, lerpColor, 0.5f);
    }
}

[System.Serializable]
public class IngredientList
{
    public Ingredient ingredient;
    public int amount;
    public float percentage;
}