using System.Collections;
using UnityEngine;

public class Drop : MonoBehaviour
{
	
	public SpriteRenderer itemDisplay;
	public Sprite[] sprites;
	public Ingredient ingredient;
	public float maxChangeSpriteTimer, secondSpriteTimer;
	float changeSpriteTimer;
	bool isChanging;
    void Update()
    {
        if(transform.localPosition.y < -1000){ Destroy(gameObject); }
		if(changeSpriteTimer > 0){changeSpriteTimer -= Time.deltaTime;}
		else if(!isChanging){ ChangeSprite(); }
    }

    public void SetItem(Ingredient item) {
		ingredient = item;
		sprites = ingredient.ingredientImages;
		itemDisplay.sprite = ingredient.ingredientImages[0];
	}

	public void SetFalling() {
		gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
		LeanTween.rotateZ(gameObject, Random.Range(-50f, 50f), Random.Range(0.5f, 1f));
	}

	void ChangeSprite()
	{
		isChanging = true;
		StartCoroutine(WaitForSprite());
	}

	IEnumerator WaitForSprite()
	{	
		itemDisplay.sprite = sprites[1];
		yield return new WaitForSeconds(secondSpriteTimer);
		itemDisplay.sprite = sprites[0];
		changeSpriteTimer = Random.Range(maxChangeSpriteTimer-0.5f, maxChangeSpriteTimer+0.5f);
		isChanging = false;
	}
}
