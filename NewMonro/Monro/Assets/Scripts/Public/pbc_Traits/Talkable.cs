using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Talkable : Tappable, IAnimatable
{
    public string DialogueStartingNode;
    public List<GameObject> ConversationParticipants;


    /** Text Configuration */
    public Color TextColor = Color.green;
	public int TextSize = 30;
	public Font textFont;
	public Sprite talkableImage;

    public bool allowDefaoultTalkPosition = true;


    private AnimationsCoordinatorHub animCoordinator;
	private Transform talkPositionGO;

    private TalkableCommand talkCommand;

	public override void OnAwake ()
	{
		base.OnAwake ();
		this.associatedTraitAction = TraitType.Talk;
		this.animCoordinator = this.GetComponent<AnimationsCoordinatorHub> ();
		//this.AssociatedMenuCommandType = CommandType.TalkCommandType;

		this.SetTalkPositionGameObject ();

        if (this.ConversationParticipants.Contains(WorldObjectsHelper.getPlayerGO()))
        {
            this.talkCommand = new PlayerMoveAndTalkCommand(this.gameObject, "", ConversationParticipants);
        }
        else
        {
            this.talkCommand = new TalkCommand("", this.ConversationParticipants);
        }
	}

    public override void OnUpdate() {
        base.OnUpdate();
        if (talkCommand != null)
        {
            talkCommand.GetCommand().UpdateCommand();
        }
    }

    public override void DoubleClick()
    {
        base.DoubleClick();
        //if (((ICommand)talkCommand).Finished() == false)
        //{
        //    return;
        //}

        this.talkCommand.SetStartingNode(this.DialogueStartingNode);
        talkCommand.GetCommand().Prepare();
        talkCommand.GetCommand().WillStart();
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
