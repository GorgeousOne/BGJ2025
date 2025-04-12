using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Ingredient[] levelIngredients;
    public Ingredient[] correctIngredients;
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
