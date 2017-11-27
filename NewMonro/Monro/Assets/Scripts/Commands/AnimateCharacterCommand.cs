using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCharacterCommand : ICommand {

	Transform targetTransform;
	string trigger;
	private Animator theAnimator;

	public AnimateCharacterCommand(IAnimatable target, string triggerParamName) {
		this.targetTransform = target.GetTransform();
		this.trigger = triggerParamName;

		this.theAnimator = target.GetAnimator();
	}

	public override void Prepare() {
		this.theAnimator.SetBool(this.trigger, true);
	}

	public override void UpdateCommand() {
		
	}

	public override bool Finished() {
		return finished;
	}

}
