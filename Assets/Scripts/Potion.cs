using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Potions")]
public class Potion : ScriptableObject
{
    public string potionName, description;
    public LevelIngredient[] ingredients;
    public int maxIngredients;
    public float errorMargin;
    public Color correctColor, wrongColor;
}
