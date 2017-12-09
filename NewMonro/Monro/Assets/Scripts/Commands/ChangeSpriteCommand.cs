using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteCommand : ICommand {

	private SpriteRenderer target;
	private Sprite replacement;
	private bool onlyFlip;

	public ChangeSpriteCommand(bool onlyFlip, SpriteRenderer target, Sprite replacement = null) {
		this.target = target;
		this.replacement = replacement;
		this.onlyFlip = onlyFlip;
	}

	public override void Prepare() {
		
	}

	public override void WillStart() {
		if (this.onlyFlip) {
			if (target.gameObject.GetComponent<GameEntity>().type == GameEntity.GameEntityType.Player 
				|| target.gameObject.GetComponent<GameEntity>().type == GameEntity.GameEntityType.NPC) {
				Character pl = target.gameObject.GetComponent<Character>();
				pl.SwapFacingDirectionTo(pl.lastFacingDirection);

				finished = true;
			}
			return;
		}

		this.target.sprite = this.replacement;
	}

	public override void UpdateCommand () {
		
	}

	public override bool Finished() {
		return finished;
	}
}
