using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionExecutePlayerMoveAndTalkTo : ExecuteCommandReaction {

	public GameObject TalkTo;

	void Awake() {
        this.theCommand = new PlayerMoveAndPickUpCommand(this.TalkTo);
	}
}
