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
        bool hasIngredient = false;
        if(string.Compare(other.tag, "Ingredient") == 0){
            currentIngredientsAmount++;
            for (int i = 0; i < currentIngredients.Count; i++){
                if(currentIngredient == currentIngredients[i].ingredient){
                    currentIngredients[i].amount++;
                    CheckIngredients(currentIngredients[i]);
                    hasIngredient = true;
                }
            }
            if(!hasIngredient){
                IngredientList listElement = new(){
                    ingredient = other.GetComponent<Drop>().ingredient,
                    amount = 1
                };
                currentIngredients.Add(listElement);   
            }
            CheckPercentages();
            if(currentIngredientsAmount > level.maxIngredients){
                level.FailLevel();
            }
        }
        Destroy(other.gameObject);
    }

    void CheckPercentages()
    {
        for (int i = 0; i < currentIngredients.Count; i++){
            currentIngredients[i].percentage = currentIngredients[i].amount / currentIngredientsAmount;
        }
        for (int i = 0; i < currentIngredients.Count; i++){
            if(currentIngredients[i].percentage == level.ingredients[i].correctAmount + level.errorMargin && currentIngredients[i].percentage == level.ingredients[i].correctAmount - level.errorMargin){
                //Percentage correct
            }
            else{ return; }
            level.CompleteLevel();
        }
    }


    void CheckIngredients(IngredientList ingredient)
    {
        for (int i = 0; i < level.ingredients.Length; i++){
            if(ingredient.ingredient == level.ingredients[i].ingredient){
                if(ingredient.percentage < level.ingredients[i].correctAmount){ RightColor(); }
                else{ WrongColor(); }
            }
        }
    }

    void WrongColor()
    {
        currentValue -= 0.1f;
        Color lerpColor = Color.Lerp(level.wrongColor, level.correctColor, currentValue);
        LeanTween.color(gameObject, lerpColor, 0.5f);
    }

    void RightColor()
    {
        currentValue += 0.1f;
        Color lerpColor = Color.Lerp(level.wrongColor, level.correctColor, currentValue);
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