using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BoxCollider2DSizeFitter : MonoBehaviour {

	void Awake() {
		
	}

	void Start() {
		SpriteRenderer itemSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
		if (itemSpriteRenderer != null) {
			Sprite itemSprite = itemSpriteRenderer.sprite; 
			this.FitSizeForSprite(itemSprite);
			return;
		}
		else if (this.transform.parent != null) {
			SpriteRenderer parentSPRenderer = this.transform.parent.GetComponent<SpriteRenderer>(); 
			if (parentSPRenderer != null) {
				Sprite itemSprite = parentSPRenderer.sprite; 
				this.FitSizeForSprite(itemSprite);
				return;	
			}
		}

		Image itemImage = this.gameObject.GetComponent<Image>();
		if (itemImage != null) {
			this.FitZiseForImage(itemImage);
		}
	}

	public void FitSizeForSprite(Sprite itemSprite) {
		Vector2 S = Vector3.Scale(itemSprite.bounds.size, this.gameObject.transform.localScale);
		gameObject.GetComponent<BoxCollider2D>().size = S;
		this.GetComponent<BoxCollider2D>().offset = new Vector2 (0, 0);
	}

	public void FitZiseForImage(Image itemImage) {
		Rect imageRect = itemImage.rectTransform.rect;
		Vector2 S = Vector3.Scale(new Vector3(imageRect.width, imageRect.height, 1), this.gameObject.transform.localScale);
		this.gameObject.GetComponent<BoxCollider2D>().size = S;
		this.gameObject.GetComponent<BoxCollider2D>().offset = new Vector2 (0, 0);	
	}
}
