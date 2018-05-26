using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationCoordintator {
	protected Animator animator;
	protected Animations currentAnimation;
	protected MonoBehaviour monoScriptOwner;
	protected GameEntity ownerEntity;

	public AnimationCoordintator(Animator animator, MonoBehaviour monoScript) {
		this.animator = animator;
		this.monoScriptOwner = monoScript;
	}
		
	public void PlayAnimation (Animations animName, GameEntity gameEntity) {
		this.currentAnimation = animName;
		this.ownerEntity = gameEntity;
		this.OnPlayAnimation(animName);
		this.monoScriptOwner.StartCoroutine(this.PlayAndWaitForAnimation(animName));
	}

	virtual public IEnumerator PlayAndWaitForAnimation(Animations animName) {
		yield return new WaitForSeconds(this.animator.GetCurrentAnimatorStateInfo(0).length);
	}

	virtual public Animations GetCurrentAnimationName () {
		return currentAnimation;
	}
		
	virtual public void StopCurrentAnimation () {}

	protected virtual void OnPlayAnimation(Animations animName) {}
}
