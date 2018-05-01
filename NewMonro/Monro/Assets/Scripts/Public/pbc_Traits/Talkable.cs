using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextboxDisplayer))]
public class Talkable : IMenuRenderableTrait {
	public string ConversationName;
	public string StartingNode;

	private GameEntity gameEntity;
	private AnimationCoordinator animCoordinator;

	void Awake() {
		this.gameEntity = this.GetComponent<GameEntity>();
		this.animCoordinator = this.GetComponent<AnimationCoordinator>();
		this.AssociatedMenuCommandType = CommandType.TalkCommandType;
	}

	public void HandleTalkingAnimation() {
		if (this.gameEntity == null || this.animCoordinator == null) {
			Debug.Log("WARNING: You are trying to animate a Talkable GameObject (" + this.gameObject + ") that does not have an animator nor a GameEntity component!! Check if any of those is missing");
			return;
		}

		if (this.gameEntity.type == GameEntity.GameEntityType.Player) {
			this.animCoordinator.PlayAnimation(Animations.PlayerTalk, PlayerAnimations.animParamIsTalking);
			//this.animCoordinator.PlayAndWaitForAnimation(Animations.PlayerTalk, PlayerAnimations.animParamIsTalking);
		}
	}

	public void StopTalkingAnimation() {
		if (this.gameEntity == null || this.animCoordinator == null) {
			Debug.Log("WARNING: You are trying to animate a Talkable GameObject (" + this.gameObject + ") that does not have an animator nor a GameEntity component!! Check if any of those is missing");
			return;
		}

		this.animCoordinator.StopCurrentAnimation();
	}

	public IEnumerator WaitForAnimation(Animator anim, string name)
	{
		anim.Play(name);
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
	}
}
