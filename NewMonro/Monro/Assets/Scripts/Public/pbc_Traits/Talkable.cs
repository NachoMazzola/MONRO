using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Tappable))]
public class Talkable : IMenuRenderableTrait, IAnimatable
{
	public string StartingNode;

	/** Text Configuration */
	public Color TextColor = Color.green;
	public int TextSize = 30;
	public Font textFont;
	public Sprite talkableImage;

	public bool allowDefaoultTalkPosition = true;

	private AnimationsCoordinatorHub animCoordinator;
	private Transform talkPositionGO;


	public override void OnAwake ()
	{
		base.OnAwake ();
		this.associatedTraitAction = TraitType.Talk;
		this.animCoordinator = this.GetComponent<AnimationsCoordinatorHub> ();
		this.AssociatedMenuCommandType = CommandType.TalkCommandType;

		this.SetTalkPositionGameObject ();
	}

	public IEnumerator WaitForAnimation (Animator anim, string name)
	{
		anim.Play (name);
		yield return new WaitForSeconds (anim.GetCurrentAnimatorStateInfo (0).length + anim.GetCurrentAnimatorStateInfo (0).normalizedTime);
	}

	public void PlayAnimation ()
	{
		if (this.gameEntity == null || this.animCoordinator == null) {
			Debug.Log ("WARNING: You are trying to animate a Talkable GameObject (" + this.gameObject + ") that does not have an animator nor a GameEntity component!! Check if any of those is missing");
			return;
		}

		this.animCoordinator.PlayAnimation (Animations.Talk, this.gameEntity);
	}

	public void StopAnimation ()
	{
		if (this.gameEntity == null || this.animCoordinator == null) {
			Debug.Log ("WARNING: You are trying to animate a Talkable GameObject (" + this.gameObject + ") that does not have an animator nor a GameEntity component!! Check if any of those is missing");
			return;
		}

		this.animCoordinator.StopAnimations ();
	}

	void Reset ()
	{
		this.SetTalkPositionGameObject ();
	}

	public Vector2 GetTalkPosition ()
	{
		return this.talkPositionGO.position;
	}

	private void SetTalkPositionGameObject ()
	{
		this.talkPositionGO = this.transform.Find ("TalkPosition");
		if (this.talkPositionGO == null) {
			Debug.Log("TALK POSITION MISSIN IN " + this.gameObject);
		}
	}
}
