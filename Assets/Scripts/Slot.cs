using UnityEngine;

public class Slot : MonoBehaviour {

	public Ingredient item;

	public SpriteRenderer itemDisplay;
	
	public bool isFull() {
		return item != null;
	}
	
	public void clear() {
		item = null;
		itemDisplay.sprite = null;	
	}

	public void setItem(Ingredient ingredient)
	{
		item = ingredient;
		itemDisplay.sprite = ingredient.ingredientImage;
		Debug.Log("Set item: " + ingredient.ingredientName);
	}
}
