using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Tappable))]
public class Lookable : Tappable {

    private LookAtCommand lCommand;

	public override void OnAwake () {
		base.OnAwake();
        this.associatedTraitAction = TraitType.LookAt;
	}


    public override void SingleClick()
    {
        base.SingleClick();

        if (this.lCommand != null && this.lCommand.isRunning)
        {
            return;
        }

        lCommand = new LookAtCommand();
        lCommand.whoLooks = WorldObjectsHelper.getPlayerGO();
        lCommand.lookable = this.gameObject;

        lCommand.Prepare();
        lCommand.WillStart();
        
        PuzzleManager.UpdatePuzzleWithAction(PuzzleActionType.LookAt, this.gameObject.transform);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.lCommand == null)
        {
            return;
        }
        lCommand.UpdateCommand();
    }
}
