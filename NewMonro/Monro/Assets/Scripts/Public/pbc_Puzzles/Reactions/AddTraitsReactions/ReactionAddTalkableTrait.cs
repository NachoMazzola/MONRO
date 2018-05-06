using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionAddTalkableTrait: IPReaction {

	public GameObject target;
	public string ConversationName;
	public string StartingNode;

	public Transform Textbox;
	public Color TextColor = Color.black;

	override public bool Execute (Transform actionReceiver, Puzzle puzzle, PAction theAction) {
		target.AddComponent<Talkable>();
		Talkable trait =  target.GetComponent<Talkable>();
		trait.ConversationName = this.ConversationName;
		trait.StartingNode = this.StartingNode;

		TextboxDisplayer tbDisplayer = target.GetComponent<TextboxDisplayer>();
		tbDisplayer.TextColor = TextColor;
		tbDisplayer.Textbox = Textbox;

		return true;
	}
}

