using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    LevelManager level;
    List<Ingredient> currentIngredients = new();
    void Start()
    {
        level = LevelManager.instance;
    }

    public void OnTriggerEnter(Collider other) 
    {
        if(string.Compare(other.tag, "Ingredient") == 0){
            // currentIngredients.Add(other.GetComponent<IngredientItem>().ingredient);
            CheckIngredients();
        }
    }

    void CheckIngredients()
    {
        for (int i = 0; i < level.correctIngredients.Length; i++){
            if(currentIngredients[i] != level.correctIngredients[i]){
                // DIE -- EXPLOSION -- 
            }
        }
    }
}
