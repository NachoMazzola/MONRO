using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsCoordinatorHub: MonoBehaviour {
	
	private Dictionary<GameEntity.GameEntityType, AnimationCoordintator> animCoordinators;
	private GameEntity ownerEntity;
	private Animator animator;
	private AnimationCoordintator currentAnimCoord;

	void Awake() {
		this.animCoordinators = new Dictionary<GameEntity.GameEntityType, AnimationCoordintator>();
		this.animator = this.GetComponent<Animator>();
		this.ownerEntity = this.gameObject.GetComponent<GameEntity>();
		Debug.Assert(this.ownerEntity != null, this.gameObject + ": GameEntity component missing.");

		this.CreateCoordinators();
	}

	private void CreateCoordinators() {
		switch (this.ownerEntity.type) {
			case GameEntity.GameEntityType.Player: 
				PlayerAnimationCoordinator playerCoord = new PlayerAnimationCoordinator(this.animator, this);
				this.animCoordinators.Add(GameEntity.GameEntityType.Player, playerCoord);
			break;

			//TODO: ADD MORE!
//		default:
//			Debug.Log("No Animation Coordinator set for type: " + this.ownerEntity.type);
		}
	}

	public void PlayAnimation(Animations animName, GameEntity gameEntity) {
		AnimationCoordintator animCoord = null;
		this.animCoordinators.TryGetValue(gameEntity.type, out animCoord);
		if (animCoord != null) {
			this.currentAnimCoord = animCoord;
			currentAnimCoord.PlayAnimation(animName, gameEntity);
		}
	}

	public void StopAnimations() {
		if (this.currentAnimCoord != null) {
			this.currentAnimCoord.StopCurrentAnimation();	
		}
	}
}
