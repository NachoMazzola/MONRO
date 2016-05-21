using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractiveObject : MonoBehaviour {

	public Transform Item;
	public string Caption;
	public Sprite HighlightedSprite;

	private SpriteRenderer theSpriteRenderer;
	private Sprite originalSprite;

	private CircleCollider2D highlightRangeCollider;
	private BoxCollider2D tappableCollider;

	private bool isShowingMenu;

	void Awake() {
		theSpriteRenderer = GetComponent<SpriteRenderer>();	
		originalSprite = theSpriteRenderer.sprite;

		highlightRangeCollider = GetComponent<CircleCollider2D>();
		tappableCollider = GetComponent<BoxCollider2D>();

		isShowingMenu = false;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D hitCollider = Physics2D.OverlapPoint(targetPosition);
			if (hitCollider != null && hitCollider == tappableCollider) {
				InteractiveMenu intMenuComp = this.GetComponent<InteractiveMenu>();
				isShowingMenu = intMenuComp.ToggleMenu();
			}
		}

	}

	//Detecting Collition with a HotSpot
	void OnTriggerEnter2D(Collider2D other) {
		if (other && other.gameObject.tag == "Player" && !isShowingMenu) {
			HighlightObject();
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other && other.gameObject.tag == "Player" && !isShowingMenu) {
			RemoveHighlight();
		}
	}

	public void HighlightObject() {
		theSpriteRenderer.sprite = HighlightedSprite;
	}

	public void RemoveHighlight() {
		theSpriteRenderer.sprite = originalSprite;
	}
}
