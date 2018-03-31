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
		finished = false;
	}

	public override void Prepare() {
		this.theAnimator.speed = 0;
		this.theAnimator.SetBool(this.trigger, true);
	}

	public override void WillStart() {
		
	}

	public override void UpdateCommand() {
		if (this.theAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !this.theAnimator.IsInTransition(0)) {
			finished = true;
			//this.theAnimator.SetBool(this.trigger, false);
			Debug.Log("FINITO ***************************************");
		}
		else {
			this.theAnimator.speed = 1;
		}

	}

	public override bool Finished() {
		if (finished == true) {
			this.theAnimator.SetBool(this.trigger, false);
		}

		return finished;
	}

	public override CommandType GetCommandType() { 
		return CommandType.AnimateCharacterCommandType; 
	}
}
