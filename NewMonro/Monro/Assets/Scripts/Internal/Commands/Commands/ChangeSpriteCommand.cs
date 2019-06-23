using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ChangeSpriteCommandParamters: ICommandParamters {
	public GameObject target;
	public Sprite replacement;
	public bool onlyFlip;

	public CommandType GetCommandType() {
		return CommandType.ChangeSpriteCommandType;
	}
}

/**
 * Changes the sprite of the target
*/
public class ChangeSpriteCommand : ICommand {

	public GameObject target;
	public Sprite replacement;
	public bool onlyFlip;

	public ChangeSpriteCommand() {
		
	}

	public ChangeSpriteCommand(ICommandParamters parameters) {
		ChangeSpriteCommandParamters p = (ChangeSpriteCommandParamters)parameters;
		this.target = p.target;
		this.replacement = p.replacement;
		this.onlyFlip = p.onlyFlip;
	}

	public ChangeSpriteCommand(bool onlyFlip, GameObject target, Sprite replacement = null) {
		this.target = target;
		this.replacement = replacement;
		this.onlyFlip = onlyFlip;
	}

	public override void Prepare() {
		
	}

	public override void WillStart() {
        isRunning = true;


    }

    public override void UpdateCommand()
    {
        if (this.isRunning)
        {
            if (this.onlyFlip)
            {
                if (target.GetComponent<GameEntity>().type == GameEntity.GameEntityType.Player
                    || target.GetComponent<GameEntity>().type == GameEntity.GameEntityType.NPC)
                {
                    Moveable pl = target.GetComponent<Moveable>();
                    pl.SwapFacingDirectionTo(pl.lastFacingDirection);
                }
                isRunning = false;
                return;
            }

            SpriteRenderer spRdr = this.target.GetComponent<SpriteRenderer>();
            spRdr.sprite = this.replacement;
            isRunning = false;
        }
    }

	public override CommandType GetCommandType() { 
		return CommandType.ChangeSpriteCommandType; 
	}
}
