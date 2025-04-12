using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button startLevelButton, lastLevelButton, nextLevelButton;
    public Potion[] levels;
    public TMPro.TMP_Text potionName, potionDescription;
    public LevelManager levelManager;
    int currentLevel;

    void Start()
    {
        startLevelButton.onClick.AddListener(StartLevel);
        lastLevelButton.onClick.AddListener(LastLevel);
        nextLevelButton.onClick.AddListener(NextLevel);
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
    }

    void SetLevel(int index)
    {
        potionName.text = levels[index].potionName;
        potionDescription.text = levels[index].description;
    }
}
