using UnityEngine;
using System.Collections;

public class HighlightableObject : MonoBehaviour {

	public Sprite HighlightedSprite;
	private SpriteRenderer theSpriteRenderer;
	private Sprite originalSprite;

	void Awake() {
		theSpriteRenderer = GetComponent<SpriteRenderer> ();	
		originalSprite = theSpriteRenderer.sprite;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	public void HighlightObject ()
	{
		theSpriteRenderer.sprite = HighlightedSprite;
	}

	public void RemoveHighlight ()
	{
		theSpriteRenderer.sprite = originalSprite;
	}
}
