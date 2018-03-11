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
	private List<IMActionButton> buttons;
	private int lastAddedButtonIdx = 0;


	void Awake() {
		//theBoxCollider = this.GetComponent<BoxCollider2D>();
	}

	// Use this for initialization
	void Start () {
		menu = Instantiate(interactiveMenuCanvas, Vector3.zero, Quaternion.identity) as RectTransform;
		menu.SetParent(this.transform);
		menu.anchoredPosition = Vector3.zero;


		buttons = GetComponents<IMActionButton>().ToList();
		if (buttons == null) {
			instantiatedButtons = new List<RectTransform>();//  new RectTransform[buttons.Count];	
			buttons = new List<IMActionButton>();
		}
		else {
			instantiatedButtons = new List<RectTransform>(buttons.Count);//  new RectTransform[buttons.Count];
		}

		int iter = 0;
		foreach (IMActionButton comp in buttons) {
			if (comp.enabled == false) {
				continue;
			}

			if (iter == maxButtons) {
				break;
			}

			comp.menu = this;

			AddButtonToMenu(comp, iter);

			iter++;
			lastAddedButtonIdx = iter;
		}
			
		menu.gameObject.SetActive(menuOn);
	}
		
	private void AddButtonToMenu(IMActionButton button, int btnIndex) {
		RectTransform theButton = Instantiate(button.ButtonPrefab, this.transform.position, Quaternion.identity) as RectTransform;
		theButton.SetParent(menu);

		Button buttonComp = theButton.GetComponent<Button>();
		buttonComp.onClick.RemoveAllListeners();
		buttonComp.onClick.AddListener(button.ExecuteAction);

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

	public void AddButton(IMActionButton newButton) {
		if (lastAddedButtonIdx == maxButtons) {
			Debug.Log("WARNING: NO SE PUEDEN AGREGAR MAS BOTONES!");
			return;
		}

		bool alreadyContainsButton = false;
		foreach (IMActionButton b in buttons) {
			if (b.buttonType == newButton.buttonType) {
				alreadyContainsButton = true;
				break;
			}
		}

		if (alreadyContainsButton == false) {
			buttons.Add(newButton);
			AddButtonToMenu(newButton, lastAddedButtonIdx);	
		}

	}

}
