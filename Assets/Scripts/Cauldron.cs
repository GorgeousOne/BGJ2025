using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public SpriteRenderer cauldronSprite;
    LevelManager level;
    List<Ingredient> currentIngredients = new();
    float correctAmount;
    void Start()
    {
        level = LevelManager.instance;
    }

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if(string.Compare(other.tag, "Ingredient") == 0){
            currentIngredients.Add(other.GetComponent<Drop>().ingredient);
            CheckIngredients();
            Destroy(other.gameObject);
        }
    }

    void CheckIngredients()
    {
        for (int i = 0; i < currentIngredients.Count; i++){
            if(currentIngredients[i] != level.correctIngredients[i]){
                // DIE -- EXPLOSION -- 
                Debug.Log("THINGS ARE CURRENTLY EXPLODING");
                StopAllCoroutines();
                LeanTween.cancel(gameObject);
                StartCoroutine(WrongColor());
            }
            else{
                LeanTween.cancel(gameObject);
                StartCoroutine(RightColor());
            }
        }
    }

    IEnumerator WrongColor()
    {
        Color currentColor = cauldronSprite.color;
        LeanTween.color(gameObject, Color.black, 0.5f);
        yield return new WaitForSeconds(0.5f);
        LeanTween.color(gameObject, currentColor, 0.5f);
    }

    IEnumerator RightColor()
    {
        Color currentColor = cauldronSprite.color;
        LeanTween.color(gameObject, Color.yellow, 0.5f);
        yield return new WaitForSeconds(0.5f);
        LeanTween.color(gameObject, currentColor, 0.5f);
    }
}
