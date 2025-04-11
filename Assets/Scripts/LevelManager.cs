using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Ingredient[] correctIngredients;
    public static LevelManager instance;

    void Start()
    {
        instance = this;
    }

}
