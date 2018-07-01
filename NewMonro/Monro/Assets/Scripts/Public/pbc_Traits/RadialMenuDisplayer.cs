using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tappable))]
public class RadialMenuDisplayer : Trait {

	public Transform MenuCanvasPrefab;
	private InteractiveMenu theMenu;

	public override void OnAwake () {
		base.OnAwake();
		this.gameObject.AddComponent<InteractiveMenu>();
		this.theMenu = this.gameObject.GetComponent<InteractiveMenu>();
		this.theMenu.interactiveMenuCanvas = this.MenuCanvasPrefab;
	}

	public void ShowMenu() {
		this.theMenu.ToggleMenu();
	}
}
