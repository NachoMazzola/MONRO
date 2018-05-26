using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.UI;

public class Character : MonoBehaviour, IWorldInteractionObserver {

	public enum MovingDirection
	{
		MovingRight,
		MovingLeft
	}

	[HideInInspector]
	public IState currentState;

	[HideInInspector]
	public IState lastState;

	[HideInInspector]
	public PlayerStateMachine animStateMachine;

	[HideInInspector]
	public SpriteRenderer characterSprite;
	[HideInInspector]
	public bool canMove = false;
	[HideInInspector]
	public MovingDirection currentFacingDirection;
	[HideInInspector]
	public MovingDirection lastFacingDirection;


	public string ConversationName;
	public Color CharacterTalkColor;
	public float MovementSpeed = 4.0f;


	void Awake() {
		
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	virtual public void OnAwake() {
		characterSprite = this.GetComponent<SpriteRenderer>();
		Vector2 theScale = this.characterSprite.transform.localScale;

		currentFacingDirection = theScale.x > 0 ? MovingDirection.MovingRight : MovingDirection.MovingLeft;
		lastFacingDirection = currentFacingDirection == MovingDirection.MovingRight ? MovingDirection.MovingLeft : MovingDirection.MovingRight;
	}

	virtual public void OnStart() {
		WorldInteractionController.getComponent().AddObserver(this);
	}

	virtual public void OnUpdate() {
		WorldInteractionController.getComponent().RemoveObserver(this);
	}

	void OnDestroy() {
	}

//	virtual public Transform GetConversationCaptionCanvas() {
//		Transform theCaption = this.transform.Find("TextBox");
//
//		return theCaption;
//	}
//
//	public IEnumerator ShowCaption(string caption, Transform textBox, TextBox.DisappearMode removalMode = TextBox.DisappearMode.WaitInput) {
//		textBox.gameObject.SetActive(true);
//
//		TextBox pCaption = textBox.GetComponent<TextBox>();
//		pCaption.TextColor = CharacterTalkColor;
//		return pCaption.ShowCaptionFromGameObject(caption, this.gameObject, true, removalMode);
//	}
//
//	public IEnumerator HideCaption(float afterSeconds) {
//		Transform theCaption = GetConversationCaptionCanvas();
//		TextBox pCaption = theCaption.GetComponent<TextBox>();
//
//		return pCaption.RemoveCaptionAfterSeconds(0.0f, pCaption.gameObject);
//	}
//
	public void setPosition(Vector2 newPos) {
		Vector2 theNewPos = new Vector2(Screen.width - newPos.x, Screen.height - newPos.y);
		this.transform.position = Camera.main.ScreenToWorldPoint(theNewPos);
	}

	virtual public void ResetState() {
		
	}

	virtual public void IWOTapped(Vector2 tapPos, GameObject other) {
		
	}

	virtual public void IWOTapHold(Vector2 tapPos, GameObject other) {
		
	}

	virtual public void IWOInterruptInteractions() {
		
	}

	virtual public Transform IWOGetTransform() {
		return this.transform;
	}

	virtual public void ChangeToState(PlayerStateMachine.PlayerStates newState) {
		
	}

	public void SwapFacingDirectionTo(MovingDirection newFacingDir) {
		float theScaleFacingRight = 1;
		float theScaleFacingLeft = -1;

		Vector2 theScale = this.characterSprite.transform.localScale;
		theScale.x = newFacingDir == MovingDirection.MovingRight ? theScaleFacingRight : theScaleFacingLeft;
		this.characterSprite.transform.localScale = theScale;

		lastFacingDirection = currentFacingDirection;
		currentFacingDirection = newFacingDir;
	}
		
	public Animator GetAnimator() {
		return this.animStateMachine.stateMachineAnimator;
	}

	public Transform GetTransform() {
		return this.transform;
	}
}
