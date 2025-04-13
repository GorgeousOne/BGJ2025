using UnityEngine;

public class Slot : MonoBehaviour {

	public Ingredient item;
	public SpriteRenderer itemDisplay;

	
	public void Clear() {
		item = null;
		itemDisplay.sprite = null;	
	}

	public void SetItem(Ingredient ingredient)
	{
		item = ingredient;
		itemDisplay.sprite = ingredient.ingredientImages[0];
	}
}
