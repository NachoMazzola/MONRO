﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InteractiveMenu : MonoBehaviour {

	public Transform interactiveMenuCanvas;

	private RectTransform menu;
	private RectTransform[] instantiatedButtons;
	private bool menuOn = false;

	// Use this for initialization
	void Start () {
		menu = Instantiate(interactiveMenuCanvas, transform.position, Quaternion.identity) as RectTransform;
		menu.SetParent(this.transform);


		IIMButtonInterface[] buttons = GetComponents<IIMButtonInterface>();
		instantiatedButtons = new RectTransform[buttons.Length];

		int iter = 0;
		foreach (IIMButtonInterface comp in buttons) {
			RectTransform theButton = Instantiate(comp.getPrefab(), this.transform.position, Quaternion.identity) as RectTransform;
			theButton.SetParent(menu);

			instantiatedButtons[iter] =  theButton;

			float topY = menu.sizeDelta.y/2 - (theButton.sizeDelta.y/2)*theButton.localScale.y;
			float leftX = -(menu.sizeDelta.x/2 - (theButton.sizeDelta.x/2)*theButton.localScale.x);
			float rightX = menu.sizeDelta.x/2 - (theButton.sizeDelta.x/2)*theButton.localScale.x;

			if (buttons.Length == 1) {
				theButton.anchoredPosition = new Vector2(0, topY);
			}
			else if (buttons.Length == 2) {
				if (iter == 0) {
					theButton.anchoredPosition = new Vector2(leftX, 0);
				}
				else {
					theButton.anchoredPosition = new Vector2(rightX, 0);
				}
			}
			else if (buttons.Length == 3) {
				if (iter == 0) {
					theButton.anchoredPosition = new Vector2(leftX, 0);
				}
				else if (iter == 1) {
					theButton.anchoredPosition = new Vector2(0, topY);
				}
				else if (iter == 2) {
					theButton.anchoredPosition = new Vector2(rightX, 0);
				}
					
			}
				
			iter++;
		}
			
		menu.gameObject.SetActive(menuOn);
	}
		
	// Update is called once per frame
	void Update () {
	
	}

	public void ToggleMenu() {
		menuOn = !menuOn;
		menu.gameObject.SetActive(menuOn);
	}
}