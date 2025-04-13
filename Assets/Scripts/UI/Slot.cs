using UnityEngine;

public class Slot : MonoBehaviour {

	public Ingredient item;
	public SpriteRenderer[] itemDisplays;

	
	public void Clear() {
		item = null;
		foreach (SpriteRenderer item in itemDisplays){ item.sprite = null; }	
	}

	public void SetItem(Ingredient ingredient)
	{
		item = ingredient;
		foreach (SpriteRenderer item in itemDisplays){ item.sprite = ingredient.ingredientImages[0]; }
	}
}
