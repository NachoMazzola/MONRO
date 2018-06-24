using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(BoxCollider2DSizeFitter))]
[RequireComponent(typeof(Rigidbody2D))]
public class VerbPanelHighlighter : MonoBehaviour {

	private TraitType tType;

	// Use this for initialization
	void Awake () {
		Rigidbody2D rigidBody = this.GetComponent<Rigidbody2D>();
		rigidBody.gravityScale = 0;

//		Trait aTrait = this.GetComponent<Trait>();
//		this.tType = aTrait.traitType;
	}
		

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other == null) {
			return;
		}

		VerbsButtonPanelHandler verbsPanel = WorldObjectsHelper.VerbsPanelUIGO().GetComponent<VerbsButtonPanelHandler>();
		verbsPanel.HighlightVerb(this.tType, true);
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other == null) {
			return;
		}

		VerbsButtonPanelHandler verbsPanel = WorldObjectsHelper.VerbsPanelUIGO().GetComponent<VerbsButtonPanelHandler>();
		verbsPanel.HighlightVerb(this.tType, false);
	}
}
