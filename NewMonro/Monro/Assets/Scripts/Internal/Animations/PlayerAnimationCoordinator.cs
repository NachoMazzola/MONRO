using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationCoordinator : AnimationCoordinator {

	override public void OnAwake() {
		base.OnAwake();

		this.DefaultLoopedAnimation = Animations.PlayerIdle;

		Dictionary<string, bool> paramsInitialStates = new Dictionary<string, bool>();
		paramsInitialStates.Add(PlayerAnimations.animParamIsWalking, false);
		paramsInitialStates.Add(PlayerAnimations.animParamIsPickingUp, false);
		paramsInitialStates.Add(PlayerAnimations.animParamIsTalking, false);

		this.animationParamsStatesForAnimation = paramsInitialStates;
	}

	override public IEnumerator PlayAndWaitForAnimation(Animations animName, string triggerParam) {
		yield return base.PlayAndWaitForAnimation(animName, triggerParam);

		this.currentAnimation = Animations.PlayerIdle;
		/**
		When we stop all animations of the player, it will play the default PlayerIdle animation
		*/
		this.StopCurrentAnimation();
	}
}
