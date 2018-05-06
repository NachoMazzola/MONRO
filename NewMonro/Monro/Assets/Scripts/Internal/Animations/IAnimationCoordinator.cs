using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationCoordinator: MonoBehaviour
{
	protected Animator animator;
	protected Animations currentAnimation;

	protected Dictionary<string, bool> animationParamsStatesForAnimation;

	void Awake () {
		this.OnAwake ();
	}

	public void PlayAnimation (Animations animName, string triggerParam) {
		this.currentAnimation = animName;
		this.animator.SetBool (triggerParam, true);

		//reset all other params except the one passed as parameter
		foreach (string animParam in animationParamsStatesForAnimation.Keys) {
			if (animParam != triggerParam) {
				this.animator.SetBool (animParam, false);
			}
		}
	}

	public IEnumerator PlayAndWaitForAnimation(Animations animName, string triggerParam) {
		this.currentAnimation = animName;
		this.animator.SetBool (triggerParam, true);

		//reset all other params except the one passed as parameter
		foreach (string animParam in animationParamsStatesForAnimation.Keys) {
			if (animParam != triggerParam) {
				this.animator.SetBool (animParam, false);
			}
		}
		yield return new WaitForSeconds(this.animator.GetCurrentAnimatorStateInfo(0).length + this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
	}

	virtual public void OnAwake () {
		this.animator = GetComponent<Animator> ();
	}

	virtual public void StopCurrentAnimation () {
		foreach (string animParam in animationParamsStatesForAnimation.Keys) {
			this.animator.SetBool (animParam, false);
		}
	}

	virtual public Animations GetCurrentAnimationName () {
		return currentAnimation;
	}
}
