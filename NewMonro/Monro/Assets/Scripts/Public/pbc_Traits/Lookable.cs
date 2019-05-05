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

        lCommand.Prepare();
        lCommand.WillStart();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        lCommand.UpdateCommand();

    }
}
