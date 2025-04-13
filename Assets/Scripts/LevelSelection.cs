using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class LevelSelection : MonoBehaviour
{
    public GameObject levelSelectionHolder;
    public Transform ingredientsHolder;
    public CanvasGroup completeScreen;
    public GameObject ingredientPrefab, sticker;
    public Button startLevelButton, lastLevelButton, nextLevelButton, menuButton, failButton;
    public Potion[] levels;
    public TMPro.TMP_Text potionName, completePotionName, potionDescription;
    public Image potionColorImage, completePotionColorImage;
    public AudioClip pageTurnSound;
    public LevelManager levelManager;
    public TMPro.TMP_FontAsset gibberishFont, normalFont;
    bool menuOpen = true;
    List<GameObject> currentIngredients = new();
    int currentLevel;
    public static LevelSelection instance;
    AudioSource source;

    void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        startLevelButton.onClick.AddListener(StartLevel);
        lastLevelButton.onClick.AddListener(LastLevel);
        nextLevelButton.onClick.AddListener(NextLevel);
        menuButton.onClick.AddListener(OpenMenu);
        failButton.onClick.AddListener(OpenMenu);
        SetLevel(0);
    }

    public void LevelComplete(int index)
    {
        completeScreen.gameObject.SetActive(true);
        LeanTween.alphaCanvas(completeScreen, 1, 0.5f);
        PlayerPrefs.SetInt(levels[index].potionName, 1);
        completePotionName.text = levels[index].potionName;

    }

    public void BackToMenu()
    {
        if(!menuOpen){ OpenMenu(); }
    }

    public void SetMenuOpen(bool value){ menuOpen = value; }

    public bool GetMenuOpen(){ return menuOpen; }

    void OpenMenu()
    {
        menuOpen = true;
        completeScreen.alpha = 0;
        levelManager.endscreen.SetActive(false);
        levelManager.uiEndScreen.gameObject.SetActive(false);
        levelManager.uiEndScreen.alpha = 0;
        completeScreen.gameObject.SetActive(false);
        levelSelectionHolder.SetActive(true);
        SetLevel(0);
    }

    void LastLevel()
    {
        potionName.font = gibberishFont;
        potionDescription.font = gibberishFont;
        sticker.SetActive(false);
        if(currentLevel > 0){ currentLevel--; }
        else{ currentLevel = levels.Length-1; }
        SetLevel(currentLevel);
        source.pitch = Random.Range(0.9f, 1.1f);
        source.PlayOneShot(pageTurnSound);
    }

    void NextLevel()
    {
        potionName.font = gibberishFont;
        potionDescription.font = gibberishFont;
        sticker.SetActive(false);
        if(currentLevel < levels.Length-1){ currentLevel++; }
        else{ currentLevel = 0; }
        SetLevel(currentLevel);
        source.pitch = Random.Range(0.9f, 1.1f);
        source.PlayOneShot(pageTurnSound);
    }

    void StartLevel()
    {
        levelManager.currentPotion = levels[currentLevel];
        levelSelectionHolder.SetActive(false);
        levelManager.StartLevel(currentLevel);
        menuOpen = false;
    }

    void SetLevel(int index)
    {
        if(PlayerPrefs.GetInt(levels[index].potionName) == 1){
            potionName.font = normalFont;
            potionName.text = levels[index].potionName;
            potionDescription.font = normalFont;
            potionDescription.text = levels[index].description;
            sticker.SetActive(true);
        }
        else{
            potionName.text = "<Uppercase>" + levels[index].potionName + "</Uppercase";
            potionDescription.text = "<Uppercase>" + levels[index].description + "</Uppercase";
            foreach (GameObject ingredient in currentIngredients){ Destroy(ingredient); }
        }
        foreach (GameObject ingredient in currentIngredients){ Destroy(ingredient); }
        currentIngredients.Clear();
        foreach (LevelIngredient ingredient in levels[currentLevel].ingredients){
            GameObject current = Instantiate(ingredientPrefab, ingredientsHolder);
            current.GetComponent<MenuIngredient>().image.sprite = ingredient.ingredient.ingredientImages[0];
            currentIngredients.Add(current);
        }
        potionColorImage.color = levels[index].correctColor;
        completePotionColorImage.color = levels[index].correctColor;
    }
}
