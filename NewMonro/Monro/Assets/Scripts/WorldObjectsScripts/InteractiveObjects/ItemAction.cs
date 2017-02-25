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
		theItem = this.GetComponent<WorldItem>().inventoryItemRepresentation;

		if (theItem.itemId != MatchesWithId) {
			TriggerCaption(onCharacter);
			if (TriggerOptionalAnimation != null && TriggerOptionalAnimation != "") {
				TriggerIncorrectAnimation(onCharacter);
			}

			this.GetComponent<WorldItem>().StopDragging();
			return;
		}

		if (TriggerOptionalAnimation != null && TriggerOptionalAnimation != "") {
			//anima esta mierda
			TriggerAnimation(onCharacter);
		}

		this.GetComponent<WorldItem>().ItemHasBeenUsed = true;
		this.GetComponent<WorldItem>().StopDragging();

		//DO SHIT IN CHILDREN
	}
}
