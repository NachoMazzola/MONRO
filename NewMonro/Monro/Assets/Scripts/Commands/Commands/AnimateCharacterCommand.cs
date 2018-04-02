using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct AnimateCharacterCommandParameters: ICommandParamters {
	public string Trigger;
	public Animator TheAnimator;

	public CommandType GetCommandType() {
		return CommandType.AnimateCharacterCommandType;
	}
}


public class AnimateCharacterCommand : ICommand {

	public string Trigger;
	public Animator TheAnimator;

	public AnimateCharacterCommand() {
		
	}

	public AnimateCharacterCommand(ICommandParamters parameters) {
		AnimateCharacterCommandParameters animParams = (AnimateCharacterCommandParameters)parameters;
		this.Trigger = animParams.Trigger;
		this.TheAnimator = animParams.TheAnimator;
	}

	public AnimateCharacterCommand(IAnimatable target, string triggerParamName) {
		this.Trigger = triggerParamName;
		this.TheAnimator = target.GetAnimator();

		finished = false;
	}

	public override void Prepare() {
		this.TheAnimator.speed = 0;
		this.TheAnimator.SetBool(this.Trigger, true);
	}

	public override void WillStart() {
		
	}

	public override void UpdateCommand() {
		if (this.TheAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !this.TheAnimator.IsInTransition(0)) {
			finished = true;
			//this.theAnimator.SetBool(this.trigger, false);
			Debug.Log("FINITO ***************************************");
		}
		else {
			this.TheAnimator.speed = 1;
		}

	}

	public override bool Finished() {
		if (finished == true) {
			this.TheAnimator.SetBool(this.Trigger, false);
		}

		return finished;
	}

	public override CommandType GetCommandType() { 
		return CommandType.AnimateCharacterCommandType; 
	}
}
