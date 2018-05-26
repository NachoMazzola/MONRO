using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextboxDisplayer))]
public class Talkable : IMenuRenderableTrait, IAnimatable {
	public string ConversationName;
	public string StartingNode;

	private GameEntity gameEntity;
	private AnimationsCoordinatorHub animCoordinator;

	void Awake() {
		this.gameEntity = this.GetComponent<GameEntity>();
		this.animCoordinator = this.GetComponent<AnimationsCoordinatorHub>();
		this.AssociatedMenuCommandType = CommandType.TalkCommandType;
	}
		
	public IEnumerator WaitForAnimation(Animator anim, string name) {
		anim.Play(name);
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
	}

	public void PlayAnimation() {
		if (this.gameEntity == null || this.animCoordinator == null) {
			Debug.Log("WARNING: You are trying to animate a Talkable GameObject (" + this.gameObject + ") that does not have an animator nor a GameEntity component!! Check if any of those is missing");
			return;
		}

		this.animCoordinator.PlayAnimation(Animations.Talk, this.gameEntity);
	}

	public void StopAnimation() {
		if (this.gameEntity == null || this.animCoordinator == null) {
			Debug.Log("WARNING: You are trying to animate a Talkable GameObject (" + this.gameObject + ") that does not have an animator nor a GameEntity component!! Check if any of those is missing");
			return;
		}

		this.animCoordinator.StopAnimations();
	}
}
