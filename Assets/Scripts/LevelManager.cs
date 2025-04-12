using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Ingredient[] levelIngredients;
    public LevelIngredient[] ingredients;
    public int maxIngredients;
    public float errorMargin;
    public Color correctColor, wrongColor;
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
    }
}

[System.Serializable]
public class LevelIngredient
{
    public Ingredient ingredient;
    public float correctAmount;
}