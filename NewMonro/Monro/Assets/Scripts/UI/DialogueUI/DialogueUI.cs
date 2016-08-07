using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;

public class DialogueUI : Yarn.Unity.DialogueUIBehaviour {

	public Transform ConversationPrefab;

	[Tooltip("How quickly to show the text, in seconds per character")]
	public float textSpeed = 0.025f;

	private Transform instantiatedConversation;

	private Transform thePlayer;
	private Transform theNPC;


	void Awake() {
		instantiatedConversation =  Instantiate(ConversationPrefab, new Vector2(), Quaternion.identity) as Transform;
		thePlayer = GameObject.FindGameObjectWithTag("Player").transform;

		instantiatedConversation.SetParent(thePlayer);

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//YARN INTERFACE IMPLEMENTATION
	// Show a line of dialogue, gradually
	public override IEnumerator RunLine (Yarn.Line line) {

		// Show the text
		Text theText = instantiatedConversation.gameObject.GetComponentInChildren<Text>();
		theText.gameObject.SetActive (true);

		if (textSpeed > 0.0f) {
			// Display the line one character at a time
			var stringBuilder = new StringBuilder ();

			foreach (char c in line.text) {
				stringBuilder.Append (c);
				theText.text = stringBuilder.ToString ();
				yield return new WaitForSeconds (textSpeed);
			}
		} else {
			// Display the line immediately if textSpeed == 0
			theText.text = line.text;
		}

		// Wait for any user input
		while (Input.anyKeyDown == false) {
			yield return null;
		}

		// Hide the text and prompt
		theText.gameObject.SetActive (false);

		yield break;
	}

	// Show a list of options, and wait for the player to make a selection.
	public override IEnumerator RunOptions (Yarn.Options optionsCollection, 
		Yarn.OptionChooser optionChooser)
	{
		yield break;
	}

	public override IEnumerator RunCommand (Yarn.Command command) {
		yield break;
	}

	public override IEnumerator DialogueStarted () {

		Debug.Log ("Dialogue starting!");

		// Enable the dialogue controls.
		if (instantiatedConversation != null)
			instantiatedConversation.gameObject.SetActive(true);
		

		yield break;
	}

	public override IEnumerator DialogueComplete () {
		yield break;
	}
		
}
