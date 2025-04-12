using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public GameObject levelSelectionHolder;
    public Transform ingredientsHolder;
    public GameObject ingredientPrefab;
    public Button startLevelButton, lastLevelButton, nextLevelButton;
    public Potion[] levels;
    public TMPro.TMP_Text potionName, potionDescription;
    public LevelManager levelManager;
    List<GameObject> currentIngredients = new();
    int currentLevel;

    void Start()
    {
        startLevelButton.onClick.AddListener(StartLevel);
        lastLevelButton.onClick.AddListener(LastLevel);
        nextLevelButton.onClick.AddListener(NextLevel);

        SetLevel(0);
    }

    void LastLevel()
    {
        if(currentLevel > 0){ currentLevel--; }
        else{ currentLevel = levels.Length-1; }
        SetLevel(currentLevel);
    }

    void NextLevel()
    {
        if(currentLevel < levels.Length-1){ currentLevel++; }
        else{ currentLevel = 0; }
        SetLevel(currentLevel);
    }

    void StartLevel()
    {
        levelManager.currentPotion = levels[currentLevel];
        levelSelectionHolder.SetActive(false);
        levelManager.StartLevel();
    }

    void SetLevel(int index)
    {
        potionName.text = levels[index].potionName;
        potionDescription.text = levels[index].description;
        foreach (GameObject ingredient in currentIngredients){ Destroy(ingredient); }
        currentIngredients.Clear();
        foreach (LevelIngredient ingredient in levels[currentLevel].ingredients){
            GameObject current = Instantiate(ingredientPrefab, ingredientsHolder);
            current.GetComponent<MenuIngredient>().image.sprite = ingredient.ingredient.ingredientImage;
            currentIngredients.Add(current);
        }
    }
}
