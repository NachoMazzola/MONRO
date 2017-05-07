using UnityEngine;
using System.Collections;

public class ItemAction: MonoBehaviour {

	public string IncorrectItemCaption;
	public string TriggerOptionalIncorrectAnimation;
	public string TriggerOptionalAnimation;
	public string MatchesWithId;

	private Item theItem;

	void Awake() {
		
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
		
	private void TriggerCaption(Character onCharacter) {
		if (onCharacter.characterType == Character.CharacterType.Player) {
			Player p = onCharacter.GetComponent<Player>();
			p.ShowCaption(IncorrectItemCaption);
		}
	}

	public void TriggerAnimation(Character onCharacter) {
		
	}

	public void TriggerIncorrectAnimation(Character onCharacter) {

	}

	public void ExecuteAction(Character onCharacter) {
		theItem = this.GetComponent<DraggableWorldItem>().itemModel;

		if (theItem.itemId != MatchesWithId) {
			TriggerCaption(onCharacter);
			if (TriggerOptionalAnimation != null && TriggerOptionalAnimation != "") {
				TriggerIncorrectAnimation(onCharacter);
			}

			this.GetComponent<DraggableWorldItem>().StopDragging();
			return;
		}

		if (TriggerOptionalAnimation != null && TriggerOptionalAnimation != "") {
			//anima esta mierda
			TriggerAnimation(onCharacter);
		}

		this.GetComponent<DraggableWorldItem>().itemModel.ItemHasBeenUsed = true;
		this.GetComponent<DraggableWorldItem>().StopDragging();

		//DO SHIT IN CHILDREN
	}
}
