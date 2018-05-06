using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationCoordinator: MonoBehaviour
{
	protected Animator animator;
	protected Animations currentAnimation;

	protected Dictionary<string, bool> animationParamsStatesForAnimation;

	public Animations DefaultLoopedAnimation = Animations.UnknownAnim;
	public string DefaultLoopedAnimationTriggerParam = "";

	void Awake () {
		this.OnAwake ();
	}

	public void PlayAnimation (Animations animName, string triggerParam) {
		this.OnPlayAnimation(animName, triggerParam);
		StartCoroutine(this.PlayAndWaitForAnimation(animName, triggerParam));
	}

	virtual public IEnumerator PlayAndWaitForAnimation(Animations animName, string triggerParam) {
		yield return new WaitForSeconds(this.animator.GetCurrentAnimatorStateInfo(0).length);
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

	protected void OnPlayAnimation(Animations animName, string triggerParam) {
		this.currentAnimation = animName;
		this.animator.SetBool (triggerParam, true);

		//reset all other params except the one passed as parameter
		foreach (string animParam in animationParamsStatesForAnimation.Keys) {
			if (animParam != triggerParam) {
				this.animator.SetBool (animParam, false);
			}
		}
	}
}
