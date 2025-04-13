using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Ingredient", order = 0)]
public class Ingredient : ScriptableObject 
{
    public string ingredientName;
    public Sprite[] ingredientImages;
    public AudioClip sound;
}
