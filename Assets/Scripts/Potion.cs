using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Potions")]
public class Potion : ScriptableObject
{
    public string potionName;
    [TextArea(2,10)]
    public string description;
    public LevelIngredient[] ingredients;
    public int maxIngredients;
    public float errorMargin;
    public Color correctColor, wrongColor;
}
