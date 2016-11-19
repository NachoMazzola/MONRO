using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.UI;

public class Character : MonoBehaviour, IWorldInteractionObserver {

	public enum CharacterType
	{
		Player,
		NPC
	}

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
	public CharacterType characterType;
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
		lastFacingDirection = currentFacingDirection;
	}

	virtual public void OnStart() {
		WorldInteractionController.getComponent().AddObserver(this);
	}

	virtual public void OnUpdate() {
	}

	virtual public Transform GetConversationCaptionCanvas() {
		Transform theCaption = this.transform.FindChild("TextCaption");

		return theCaption;
	}

	virtual public void ResetState() {
		
	}

	virtual public void IWOTapped(Vector2 tapPos, GameObject other) {
		
	}

	virtual public void IWOTapHold(Vector2 tapPos, GameObject other) {
		
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
		
}
