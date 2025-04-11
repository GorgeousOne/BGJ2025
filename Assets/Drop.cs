using UnityEngine;

public class Drop : MonoBehaviour
{
	
	public SpriteRenderer itemDisplay;
	public Ingredient item;
	
	//the getcompinent should work but it doesnt!
	void Start() {
		// itemDisplay = GetComponent<SpriteRenderer>();
	}

	public void setItem(Ingredient ingredient) {
		item = ingredient;
		itemDisplay.sprite = item.ingredientImage;
	}

	public void setFalling() {
		gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
	}
}
