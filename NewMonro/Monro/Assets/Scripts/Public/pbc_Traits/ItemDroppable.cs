using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDroppable : Tappable
{
	public Transform InventroyItem;
    private PlayerMoveAndPickUpCommand moveAndPickUpCommand;

	void Awake() {
		this.associatedTraitAction = TraitType.Pickup;
    }

    public override void DoubleClick()
    {
        base.DoubleClick();
        if (this.moveAndPickUpCommand != null && this.moveAndPickUpCommand.isRunning)
        {
            return;
        }

        this.moveAndPickUpCommand = new PlayerMoveAndPickUpCommand(this.gameObject);
        this.moveAndPickUpCommand.Prepare();
        this.moveAndPickUpCommand.WillStart();

        PuzzleManager.UpdatePuzzleWithAction(PuzzleActionType.PickUp, this.gameObject.transform);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.moveAndPickUpCommand != null)
        {
            this.moveAndPickUpCommand.UpdateCommand();
        }
    }
}
