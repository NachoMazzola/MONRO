using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AnimateCharacterCommandParameters: ICommandParamters {
	public string Trigger;
	public Transform Target;

	public CommandType GetCommandType() {
		return CommandType.AnimateCharacterCommandType;
	}
}


public class AnimateCharacterCommand : ICommand {

	public string Trigger;
	public Transform Target;

	private Animator TheAnimator;

	public AnimateCharacterCommand() {
		
	}

	public AnimateCharacterCommand(ICommandParamters parameters) {
		AnimateCharacterCommandParameters animParams = (AnimateCharacterCommandParameters)parameters;
		this.Trigger = animParams.Trigger;
		this.Target = animParams.Target;
		this.TheAnimator = this.Target.GetComponent<Animator>();

		Debug.Assert(this.TheAnimator != null, "AnimateCharacterCommand: Target does not have an Animator component!!");
	}

	public AnimateCharacterCommand(Transform target, string triggerParamName) {
		this.Trigger = triggerParamName;
		this.Target = target;
		this.TheAnimator = this.Target.GetComponent<Animator>();

		Debug.Assert(this.TheAnimator != null, "AnimateCharacterCommand: Target does not have an Animator component!!");

		isRunning = false;
	}

	public override void Prepare() {
		this.TheAnimator.speed = 0;
		this.TheAnimator.SetBool(this.Trigger, true);
	}

	public override void WillStart() {
		
	}

	public override void UpdateCommand() {
		if (this.TheAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !this.TheAnimator.IsInTransition(0)) {
			isRunning = true;
			//this.theAnimator.SetBool(this.trigger, false);
			Debug.Log("FINITO ***************************************");
		}
		else {
			this.TheAnimator.speed = 1;
            this.isRunning = false;
		}

	}

	public override bool Finished() {
		if (isRunning == false) {
			this.TheAnimator.SetBool(this.Trigger, false);
		}

		return isRunning;
	}

	public override CommandType GetCommandType() { 
		return CommandType.AnimateCharacterCommandType; 
	}
}
