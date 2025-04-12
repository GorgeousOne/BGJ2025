using System;
using UnityEngine;

public class BookAnimation : MonoBehaviour
{
	
	public RectTransform book;
	public RectTransform potion;
	public RectTransform sticker;

	public float BookSlideTime = 1f;
	public float StickerSlideTime = 1f;

	Vector2 bookOffset = new Vector2(-500, 0);
	Vector2 stickerOffset = new Vector2(500, 0);

	private Vector2 bookPos;
	private Vector2 stickerPos;
	
	void OnEnable() {
		InitAnims();
		ResetAnims();
		AnimateSlideIn();
	}

	//TODO pass the used items
	public void PopUpBook() {
		ResetAnims();
		AnimateSlideIn();
		
		//TODO visualize the items
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
		LeanTween.move(book, bookPos, BookSlideTime)
			.setEase(LeanTweenType.easeOutCubic);

		LeanTween.move(sticker, stickerPos, StickerSlideTime)
			.setEase(LeanTweenType.easeOutCubic)
			.setDelay(BookSlideTime);
	}
}
