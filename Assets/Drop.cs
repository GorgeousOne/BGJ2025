using UnityEngine;

public class Drop : MonoBehaviour
{
	
	public SpriteRenderer itemDisplay;
	public Ingredient ingredient;
	
    void Update()
    {
        if(transform.localPosition.y < -1000){ Destroy(gameObject); }
    }

    public void setItem(Ingredient item) {
		ingredient = item;
		itemDisplay.sprite = ingredient.ingredientImage;
	}

	public void setFalling() {
		gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
	}
}
