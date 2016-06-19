﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class InteractiveObject : MonoBehaviour {

	public Transform Item;
	public string Caption;
	public Sprite HighlightedSprite;

	private SpriteRenderer theSpriteRenderer;
	private Sprite originalSprite;


	private bool isShowingMenu;

	void Awake() {
		theSpriteRenderer = GetComponent<SpriteRenderer>();	
		originalSprite = theSpriteRenderer.sprite;
	
		isShowingMenu = false;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D[] hitColliders = Physics2D.OverlapPointAll(targetPosition); 

			//2 colliders means that the "tappable" collider has been tapped.. we ignore
			//if the circle collider has been tapped or not. We only care if the "tappable" was tapped
			if (hitColliders != null && hitColliders.Length == 2) {
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
		
	public BoxCollider2D GetTappbleCollider() {
		return GetComponent<BoxCollider2D>();
	}
}