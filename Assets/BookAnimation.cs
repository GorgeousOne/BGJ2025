using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BookAnimation : MonoBehaviour
{
	public RectTransform book;
	public RectTransform potion;
	public RectTransform sticker;
	
	public TextMeshProUGUI potionLabel;
	public TMP_FontAsset crypticFont;
	public TMP_FontAsset clearFont;
	
	public float BookSlideTime = 1f;
	public float StickerSlideTime = 1f;
	Vector2 bookOffset = new Vector2(-500, 0);
	Vector2 stickerOffset = new Vector2(500, 0);

	private Vector2 bookPos;
	private Vector2 stickerPos;
	
	void OnEnable() {
		InitAnims();
		ResetAnims();
		book.gameObject.SetActive(false);
	}

	public void ShowUnsolvedPotion(Potion potion) {
		ResetAnims();
		SetEncrypted(true);
		potionLabel.text = potion.potionName;
		AnimateSlideIn();
	}

	private void SetEncrypted(bool state) {
		FontStyles style = state ? FontStyles.UpperCase : FontStyles.Normal;
		TMP_FontAsset font = state ? crypticFont : clearFont;
		potionLabel.fontStyle = style;
		potionLabel.font = font;
	}
	
	public void ShowSolvedPotion(Potion potion, List<Ingredient> usedItems) {
		ResetAnims();
		SetEncrypted(false);
		
		potionLabel.text = potion.potionName;
		AnimateSlideIn();
	}
	
	//read all the target positions
	private void InitAnims() {
		bookPos = book.anchoredPosition;
		stickerPos = sticker.anchoredPosition;
	}
	
	private void ResetAnims() {
		book.anchoredPosition = bookPos + bookOffset;
		sticker.anchoredPosition = stickerPos + stickerOffset;
	}

	void AnimateSlideIn() {
		book.gameObject.SetActive(true);
		
		LeanTween.move(book, bookPos, BookSlideTime)
			.setEase(LeanTweenType.easeOutCubic);

		LeanTween.move(sticker, stickerPos, StickerSlideTime)
			.setEase(LeanTweenType.easeOutCubic)
			.setDelay(BookSlideTime);
	}
}
