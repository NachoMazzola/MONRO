using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationCoordinator : AnimationCoordintator {

	private string animParamIsWalking = "isMoving";
	private string animParamIsPickingUp = "isPickingUp";
	private string animParamIsTalking = "isTalking";
	private string animParamShouldWakeUp = "shouldWakeUp";

	public PlayerAnimationCoordinator(Animator animator, MonoBehaviour monoScript): base(animator, monoScript) {
		
	}

	override public IEnumerator PlayAndWaitForAnimation(Animations animName) {
		if (this.currentAnimation == Animations.Talk) {
			yield return base.PlayAndWaitForAnimation(animName);

			this.currentAnimation = Animations.Idle;
			/**
			When we stop all animations of the player, it will play the default PlayerIdle animation
			*/
			this.StopCurrentAnimation();
		}
	}

	override protected void OnPlayAnimation(Animations animName) {
		if (this.ownerEntity.type != GameEntity.GameEntityType.Player) {
			Debug.Log("WARNING: Animation on Player will not play. Forgot to set GameEntity type?");
			return;
		}

		this.currentAnimation = animName;
		switch(this.currentAnimation) {
		case Animations.Idle: 
			this.animator.SetBool(this.animParamIsWalking, false);
			this.animator.SetBool(this.animParamIsPickingUp, false);
			this.animator.SetBool(this.animParamIsTalking, false);
			//this.animator.SetBool(this.animParamShouldWakeUp, false);
			break;

		case Animations.Walk:
			this.animator.SetBool(this.animParamIsWalking, true);
			this.animator.SetBool(this.animParamIsPickingUp, false);
			this.animator.SetBool(this.animParamIsTalking, false);
			//this.animator.SetBool(this.animParamShouldWakeUp, false);
			break;

		case Animations.PickUp:
			this.animator.SetBool(this.animParamIsWalking, false);
			this.animator.SetBool(this.animParamIsPickingUp, true);
			this.animator.SetBool(this.animParamIsTalking, false);
			//this.animator.SetBool(this.animParamShouldWakeUp, false);
			break;

		case Animations.Talk:
			this.animator.SetBool(this.animParamIsTalking, true);
			this.animator.SetBool(this.animParamIsPickingUp, false);
			this.animator.SetBool(this.animParamIsWalking, false);
			//this.animator.SetBool(this.animParamShouldWakeUp, false);

			break;
		}
	}

	override public void StopCurrentAnimation () {
		this.PlayAnimation(Animations.Idle, this.ownerEntity);
	}
}
