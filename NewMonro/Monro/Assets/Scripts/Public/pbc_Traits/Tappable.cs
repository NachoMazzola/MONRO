using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/**
 * Adds click/tap interaction to a GameObject
*/

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(BoxCollider2DSizeFitter))]
[RequireComponent(typeof(VerbPanelHighlighter))]
public class Tappable : Trait {

	[HideInInspector]
	public ArrayList colliderList;


	public override void OnAwake () {
		base.OnAwake();

		BoxCollider2D[] boxColliders = this.GetComponents<BoxCollider2D>();
		this.colliderList = new ArrayList();
		this.colliderList.AddRange(boxColliders);

		foreach (BoxCollider2D box in this.colliderList) {
			box.isTrigger = true;
		}
	}
}
