using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Potions")]
public class Potion : ScriptableObject
{
    public string potionName, description;
    public Ingredient[] possibleIngredients;
}
