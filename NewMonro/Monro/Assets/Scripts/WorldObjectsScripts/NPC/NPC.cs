using UnityEngine;
using System.Collections;

public class NPC : Character {

	public string ConversationNode;

	void Awake() {
		characterType = CharacterType.NPC;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	override public void ResetState() {
		InteractiveObject intObj = this.GetComponent<InteractiveObject>();
		intObj.allowInteraction = true;

	}
}
