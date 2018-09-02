using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talkable : IMenuRenderableTrait, IAnimatable {
	public string StartingNode;

	/** Text Configuration */
	public Color TextColor = Color.green;
	public int TextSize = 30;
	public Font textFont;
	public Sprite talkableImage;


	private AnimationsCoordinatorHub animCoordinator;

	public override void OnAwake () {
		base.OnAwake();
		this.associatedTraitAction = TraitType.Talk;
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
