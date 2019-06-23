using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Tappable))]
public class Lookable : Tappable {

    private LookAtCommand lCommand;

	public override void OnAwake () {
		base.OnAwake();
		this.associatedTraitAction = TraitType.LookAt;

        lCommand = new LookAtCommand();
        lCommand.whoLooks = WorldObjectsHelper.getPlayerGO();
        lCommand.lookable = this.gameObject;
	}


    public override void SingleClick()
    {
        base.SingleClick();

        if (lCommand.isRunning)
        {
            return;
        }

        lCommand.Prepare();
        lCommand.WillStart();
        
        PuzzleManager.UpdatePuzzleWithAction(PuzzleActionType.LookAt, this.gameObject.transform);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (!lCommand.isRunning)
        {
            return;
        }
        lCommand.UpdateCommand();

    }
}
