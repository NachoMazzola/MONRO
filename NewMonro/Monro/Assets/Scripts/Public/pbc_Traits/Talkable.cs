using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextboxDisplayer))]
public class Talkable : IMenuRenderableTrait {
	public string ConversationName;
	public string StartingNode;

	void Awake() {
		this.AssociatedMenuCommandType = CommandType.TalkCommandType;
	}
}
