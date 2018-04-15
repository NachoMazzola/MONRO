using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class InteractiveMenu : MonoBehaviour {

	public Transform interactiveMenuCanvas;
	public float ButtonDistance = 35.0f;

	private RectTransform menu;
	private List<RectTransform> instantiatedButtons;
	private bool menuOn = false;
	private int maxButtons = 4;
	private List<IMenuRenderableTrait> buttons;
	private int lastAddedButtonIdx = 0;

	private PlayerCommandBuilder uiCommandBuilder;


	void Awake() {
		this.uiCommandBuilder = new PlayerCommandBuilder();
		this.uiCommandBuilder.uiType = UIType.RadialMenu;
		this.uiCommandBuilder.target = this.gameObject;

		//theBoxCollider = this.GetComponent<BoxCollider2D>();
	}

	// Use this for initialization
	void Start () {
		menu = Instantiate(interactiveMenuCanvas, Vector3.zero, Quaternion.identity) as RectTransform;
		menu.SetParent(this.transform);
		menu.anchoredPosition = Vector3.zero;


		buttons = GetComponents<IMenuRenderableTrait>().ToList();
		if (buttons == null) {
			instantiatedButtons = new List<RectTransform>();//  new RectTransform[buttons.Count];	
			buttons = new List<IMenuRenderableTrait>();
		}
		else {
			instantiatedButtons = new List<RectTransform>(buttons.Count);//  new RectTransform[buttons.Count];
		}

		int iter = 0;
		foreach (IMenuRenderableTrait comp in buttons) {
			if (comp.enabled == false) {
				continue;
			}

			if (iter == maxButtons) {
				break;
			}

			AddButtonToMenu(comp, iter);

			iter++;
			lastAddedButtonIdx = iter;
		}
			
		menu.gameObject.SetActive(menuOn);
	}
		
	private void AddButtonToMenu(IMenuRenderableTrait button, int btnIndex) {
		RectTransform theButton = Instantiate(button.prefab, this.transform.position, Quaternion.identity) as RectTransform;
		theButton.SetParent(menu);

		Button buttonComp = theButton.GetComponent<Button>();
		buttonComp.onClick.RemoveAllListeners();
		buttonComp.onClick.AddListener(delegate { RunCommandForButton(button); } );

		instantiatedButtons.Add(theButton);
		//instantiatedButtons[btnIndex] =  theButton;

		//			float offset = 45.0f;
		//			if (iter == 3) {
		//				offset = 45.0f * 2;
		//			}
		//
		//float ang = ((iter * 180.0f) / buttons.Length) - 45.0f;

		float ang = ((btnIndex * 360.0f) / buttons.Count);
		Vector2 pos = new Vector2();
		pos.x = transform.position.x + ButtonDistance * Mathf.Sin(ang * Mathf.Deg2Rad);
		pos.y = transform.position.y + ButtonDistance * Mathf.Cos(ang * Mathf.Deg2Rad);

		theButton.anchoredPosition = pos;
	}

	// Update is called once per frame
	void Update () {
	
	}

	public bool ToggleMenu() {
		menuOn = !menuOn;
		menu.gameObject.SetActive(menuOn);

		return menuOn;
	}

	public bool menuIsOn() {
		return menuOn;
	}

	private void RunCommandForButton(IMenuRenderableTrait trait) {
		GameObject menuOwner = trait.gameObject;
		switch(trait.AssociatedMenuCommandType) {
		case CommandType.LookAtCommandType:
			this.uiCommandBuilder.CreateLookAtCommand();
			break;

		case CommandType.TalkCommandType:
			this.uiCommandBuilder.CreateTalkToCommand();
			break;

		case CommandType.PutItemInInventoryCommandType:
			this.uiCommandBuilder.CreatePickUpCommand();
			break;
		}
	}

}
