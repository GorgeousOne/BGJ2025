using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public SpriteRenderer cauldronSprite;
    LevelManager level;
    List<IngredientList> currentIngredients = new();
    float correctAmount, wrongAmount, currentValue = 0.5f;
    void Start()
    {
        level = LevelManager.instance;
    }

    public void OnTriggerEnter2D(Collider2D other) 
    {
        Ingredient currentIngredient = other.GetComponent<Drop>().ingredient;
        bool hasIngredient = false;
        if(string.Compare(other.tag, "Ingredient") == 0){
            for (int i = 0; i < currentIngredients.Count; i++){
                if(currentIngredient == currentIngredients[i].ingredient){
                    currentIngredients[i].amount++;
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
                
            CheckIngredients(other.GetComponent<Drop>().ingredient);
            Destroy(other.gameObject);
        }
    }

    void CheckIngredients(Ingredient ingredient)
    {
        for (int i = 0; i < level.correctIngredients.Length; i++){
            if(ingredient == level.correctIngredients[i]){
                LeanTween.cancel(gameObject);
                RightColor();
                correctAmount++;
            }
            else if(ingredient == level.wrongIngredients[i]){
                Debug.Log("THINGS ARE CURRENTLY EXPLODING");
                LeanTween.cancel(gameObject);
                WrongColor();
                wrongAmount++;
            }
        }
    }

    void WrongColor()
    {
        Color currentColor = cauldronSprite.color;
        currentValue -= 0.5f / level.correctIngredients.Length;
        Debug.Log(correctAmount / level.correctIngredients.Length);
        Color lerpColor = Color.Lerp(currentColor, level.wrongColor, currentValue);
        LeanTween.color(gameObject, lerpColor, 0.5f);
    }

    void RightColor()
    {
        Color currentColor = cauldronSprite.color;
        currentValue += 0.5f / level.correctIngredients.Length;
        Debug.Log(correctAmount / level.correctIngredients.Length);
        Color lerpColor = Color.Lerp(currentColor, level.correctColor, currentValue);
        LeanTween.color(gameObject, lerpColor, 0.5f);
    }
}

[System.Serializable]
public class IngredientList
{
    public Ingredient ingredient;
    public int amount;
}