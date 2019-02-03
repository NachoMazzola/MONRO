﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactionAddTalkableTrait: IPReaction {
	public GameObject target;
	public string StartingNode;

	public Color TextColor = Color.green;
	public int TextSize = 30;
	public Font textFont;
	public Sprite talkableImage;

	override public bool Execute (Transform actionReceiver, Puzzle puzzle, PAction theAction) {
		if (target.GetComponent<Talkable>() != null) {
			return false;
		}
			
		target.AddComponent<Talkable>();
		if (target.GetComponent<VerbPanelHighlighter>() == null) {
			target.AddComponent<VerbPanelHighlighter>();	
		}

		Talkable trait =  target.GetComponent<Talkable>();
		trait.StartingNode = this.StartingNode;
		trait.TextColor = this.TextColor;
		trait.TextSize = this.TextSize;
		trait.textFont = this.textFont;
		trait.talkableImage = this.talkableImage;

		return true;
	}
}

