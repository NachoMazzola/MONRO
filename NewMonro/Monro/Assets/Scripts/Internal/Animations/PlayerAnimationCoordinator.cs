using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationCoordinator : AnimationCoordinator {

	override public void OnAwake() {
		base.OnAwake();

		Dictionary<string, bool> paramsInitialStates = new Dictionary<string, bool>();
		paramsInitialStates.Add(PlayerAnimations.animParamIsWalking, false);
		paramsInitialStates.Add(PlayerAnimations.animParamIsPickingUp, false);
		paramsInitialStates.Add(PlayerAnimations.animParamIsTalking, false);

		this.animationParamsStatesForAnimation = paramsInitialStates;
	}
}
