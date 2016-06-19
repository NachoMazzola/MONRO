using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InteractiveMenu : MonoBehaviour {

	public Transform interactiveMenuCanvas;
	public float ButtonDistance = 35.0f;

	private RectTransform menu;
	private RectTransform[] instantiatedButtons;
	private bool menuOn = false;


	// Use this for initialization
	void Start () {
		menu = Instantiate(interactiveMenuCanvas, transform.position, Quaternion.identity) as RectTransform;
		menu.SetParent(this.transform);


		IMActionButton[] buttons = GetComponents<IMActionButton>();
		if (buttons == null || buttons.Length == 0) {
			Debug.Log("WARNING: MENU DOES NOT HAVE ANY BUTTON ACTIONS!");
			return;
		}
			
		instantiatedButtons = new RectTransform[buttons.Length];

		int iter = 0;
		foreach (IMActionButton comp in buttons) {
			if (comp.enabled == false) {
				continue;
			}

			RectTransform theButton = Instantiate(comp.ButtonPrefab, this.transform.position, Quaternion.identity) as RectTransform;
			theButton.SetParent(menu);

			Button buttonComp = theButton.GetComponent<Button>();
			buttonComp.onClick.RemoveAllListeners();
			buttonComp.onClick.AddListener(comp.ExecuteAction);

			instantiatedButtons[iter] =  theButton;


//			float offset = 45.0f;
//			if (iter == 3) {
//				offset = 45.0f * 2;
//			}
//
			//float ang = ((iter * 180.0f) / buttons.Length) - 45.0f;
			float ang = ((iter * 360.0f) / buttons.Length);
			Vector2 pos = new Vector2();
			pos.x = transform.position.x + ButtonDistance * Mathf.Sin(ang * Mathf.Deg2Rad);
			pos.y = transform.position.y + ButtonDistance * Mathf.Cos(ang * Mathf.Deg2Rad);

			theButton.anchoredPosition = pos;

			iter++;
		}
			
		menu.gameObject.SetActive(menuOn);
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

	public void caca() {
		Debug.Log("CONCHIPAPI");
	}
}
