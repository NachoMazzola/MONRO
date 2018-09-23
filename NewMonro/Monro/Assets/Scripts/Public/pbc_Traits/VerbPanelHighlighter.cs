using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class VerbPanelHighlighter : MonoBehaviour {

	private TraitType tType;

	private IMenuRenderableTrait[] aTraits;

	// Use this for initialization
	void Awake () {
		Rigidbody2D rigidBody = this.GetComponent<Rigidbody2D>();
		rigidBody.gravityScale = 0;

		this.aTraits = this.gameObject.GetComponents<IMenuRenderableTrait>();
	}
		

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other == null) {
			return;
		}

		VerbsButtonPanelHandler verbsPanel = WorldObjectsHelper.VerbsPanelUIGO().GetComponent<VerbsButtonPanelHandler>();
		foreach (IMenuRenderableTrait trait in this.aTraits) {
			verbsPanel.HighlightVerb(trait.associatedTraitAction, true);	
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other == null) {
			return;
		}

		VerbsButtonPanelHandler verbsPanel = WorldObjectsHelper.VerbsPanelUIGO().GetComponent<VerbsButtonPanelHandler>();
		foreach (IMenuRenderableTrait trait in this.aTraits) {
			verbsPanel.HighlightVerb(trait.associatedTraitAction, false);	
		}
	}
}
