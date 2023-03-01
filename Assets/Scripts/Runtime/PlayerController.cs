using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using System;
using FMOD.Studio;

public class PlayerController : MonoBehaviour
{
    //Audio
    private EventInstance playerFootstep;
    //LayerMask
	public LayerMask groundLayer;
	//
    private Rigidbody rb;
	private NavMeshAgent agent;
	[SerializeField] private float jumpForce;
	[SerializeField] private float slideOnLand;
	[SerializeField] private UI_Inventory uiInventory;
	[SerializeField] private InputActionAsset inputProvider;
	[SerializeField] private float moveSpeed; //= 2f;
	//[SerializeField] private Animator frontAnim;
	//2D anim movement properties
	[SerializeField] private Animator anim;
	[SerializeField] private SpriteRenderer sr;
	//[SerializeField] private Animator backAnim;
	private Vector3 _horizontalJumpVelocity = Vector3.zero;
	private Vector3 dropPosition;
	private Vector2 moveVals;
	//private bool itemDragStarted;
	private Item item;
	private Player player;
	private bool itemClicked;
	private bool inventoryOpened;
	private bool playerAutoMove;
    public bool PlayerAutoMove { get => playerAutoMove; set => playerAutoMove = value; }
	public PlayerStates currentState;
	public enum PlayerStates
    {
		IDLE,
		WALK
    }
	PlayerStates CurrentState
    {
		set
        {
			currentState = value;

			switch(currentState)
            {
				case PlayerStates.IDLE:
					anim.Play("Idle");
                    playerFootstep.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    break;
				case PlayerStates.WALK:
					anim.Play("Movement");
                    playerFootstep.start();
					break;
			}
        }
    }
	//testing INK script variable changes

    void Awake()
	{
		inventoryOpened = false;
		//itemDragStarted = false;
		itemClicked = false;
		agent = GetComponent<NavMeshAgent>();
		if (agent == null)
		{
			gameObject.AddComponent<NavMeshAgent>();
			agent = GetComponent<NavMeshAgent>();
		}
		player = this.GetComponent<Player>();
		//SetUpRigidbody();
		//inputProvider.FindActionMap("PlayerMovements").FindAction("Directional Movements").performed += ManagePlayerMovement;
	}

	void OnEnable() {
		inputProvider.FindAction("Directional Movements").Enable();
		inputProvider.FindAction("Item Pickup").Enable();
		DragAndDrop.OnUIActionStart += SetItemClickBool;
		DragAndDrop.OnSeedDrop += GetItemDropPosition;
	}

	void Update()
	{
		MoveCharacter();
		SetInventoryActiveStatus();
		//player moves to the position where the seed is dropped
		// if(PlayerAutoMove) {
		// 	agent.isStopped = false;
		// 	agent.SetDestination(dropPosition);
		// 	float playerXPos = transform.position.x;
		// 	float destinationXPos = dropPosition.x;
		// 	if(playerXPos == destinationXPos) {
		// 		//Debug.Log("Disabling nav mesh agent");
		// 		PlayerAutoMove = false;
		// 		agent.isStopped = true;
		// 	}
		// }
		// else {
		// 	MoveCharacter();
		// 	SetInventoryActiveStatus();
		// }
	}

	public void OnPlayerMove(InputAction.CallbackContext context) {
		//get player input values
		moveVals = context.ReadValue<Vector2>();
		if(moveVals != Vector2.zero)
        {
			CurrentState = PlayerStates.WALK;
			anim.SetFloat("Horizontal", moveVals.x);
			anim.SetFloat("Vertical", moveVals.y);

			//Set character facing direction
			if(moveVals.x > 0)
            {
				sr.flipX = true;
            }
			else if(moveVals.x < 0)
            {
				sr.flipX = false;
            }
        }
        else
        {
			CurrentState = PlayerStates.IDLE;
        }
	}

	public void OnMouseClick(InputAction.CallbackContext context) {
		if(context.performed) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
			//this bool checks if there is something over the UI
			//distance should be changed from mathf.infinity
			if (Physics.Raycast(ray, out hit, 10f))
			{
				if(!inventoryOpened && !itemClicked) {
					//add item to inventory
					ItemTemplate itemTemplate = hit.collider.GetComponent<ItemTemplate>();
					if(itemTemplate != null) {
						Inventory inventoryObject = this.GetComponent<Player>().GetPlayerInventory();
						inventoryObject.AddItem(itemTemplate.GetItem());
						QuestManager.Instance.CheckCollectingQuestStatus(itemTemplate.GetItem().itemID);
						itemTemplate.DestroyItemTemplate();
					}
				}
				else if (itemClicked) {
					itemClicked = false;
					Inventory inventory = player.GetPlayerInventory();
					GetItemDropPosition(hit.point);
					QuestManager.Instance.CheckPlantingQuestStatus(this.item.itemID);
					//TODO :Instantiate planting logic here
					//get item id from this.item
					if(playerAutoMove) {
						PlantTemplate.SpawnPlant(dropPosition, new Plant {plantID = this.item.itemID});
						inventory.DropItem(this.item);
						this.item = null;
						PlayerAutoMove = false;
					}
				}
			}
		}

	}

	private void MoveCharacter() {
		//Get normalized directional vectors of the cameras
		Vector3 camForward = Camera.main.transform.forward;
		Vector3 camRight = Camera.main.transform.right;
		camForward.y = 0;
		camRight.y = 0;
		camForward = camForward.normalized;
		camRight = camRight.normalized;

		//get direction-relative input vectors
		Vector3 forwardRelativeVerticalInput = moveVals.y * camForward;
		Vector3 rightRelativeVerticalInput = moveVals.x * camRight;

		//get camera relative movement vector
		Vector3 camRelativeMovement = forwardRelativeVerticalInput + rightRelativeVerticalInput;
		if(camRelativeMovement != Vector3.zero) {
			//Rotation is not necessary for the 2D sprites
			//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(camRelativeMovement), 0.15f);
			transform.Translate(camRelativeMovement * moveSpeed * Time.deltaTime,Space.World);
			//agent.Move(camRelativeMovement * moveSpeed * Time.deltaTime);
		}
	}


	// public void SetItemDragBool(bool dragVal) {
	// 	itemDragStarted = dragVal;
	// }

	public void SetItemClickBool(bool clickValue, Item item) {
		itemClicked = clickValue;
		this.item = item;
		Debug.Log("The Item is : "+this.item);
	}

	public void SetInventoryActiveStatus() {
		if(uiInventory.isActiveAndEnabled){
			inventoryOpened = true;
		}
		else {
			inventoryOpened = false;
		}
	}

	public void GetItemDropPosition(Vector3 position) {
		RaycastHit hit;
		position.y = 12f;
		if(Physics.Raycast(position, Vector3.down,out hit,Mathf.Infinity,groundLayer)) {
                float yPosition = hit.point.y + 0.5f;
                position.y = yPosition;
				dropPosition = position;
				Debug.Log("drop position assigned");
				PlayerAutoMove = true; //just a temp solution to check if the spawn position is assigned correctly
        }
	}

    //Adudio
    private void Start()
    {
        playerFootstep = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerFootstep);
    }


    /*
    if (Input.GetMouseButtonDown(1))
    {
        TryJump();
    }
}

public void TryJump()
{
    if (!_isGrounded) return;
    Debug.Log("Do jump tried");
    DoJump();
}

private void DoJump()
{
    _horizontalJumpVelocity = agent.velocity;
    DisableNavMeshAgent();
    EnableRB();
    _isGrounded = false;
    rb.AddForce(new Vector3(_horizontalJumpVelocity.x, jumpForce, _horizontalJumpVelocity.z), ForceMode.Impulse);
}

private void SetUpRigidbody()
{
    rb = GetComponent<Rigidbody>();
    if (rb == null)
    {
        gameObject.AddComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
    }
    DisableRB();
    rb.freezeRotation = true;
}

// Effectively enables rigidbody.
private void EnableRB()
{
    rb.isKinematic = false;
    rb.useGravity = true;
}

// Effectively disables rigidbody. Technically still enabled but won't do much.
private void DisableRB()
{
    rb.isKinematic = true;
    rb.useGravity = false;
}

private void EnableNavMeshAgent()
{
    agent.Warp(transform.position);
    if(Mathf.Abs(_horizontalJumpVelocity.x) > 0f || Mathf.Abs(_horizontalJumpVelocity.z) > 0f) agent.SetDestination(transform.position + (transform.forward * slideOnLand));
    agent.updatePosition = true;
    agent.updateRotation = true;
    agent.isStopped = false;
}

private void DisableNavMeshAgent()
{
    agent.updatePosition = false;
    agent.updateRotation = false;
    agent.isStopped = true;
}

private void OnCollisionEnter(Collision other)
{
    Debug.Log(other.collider.tag);
    if (other.collider != null && other.collider.tag == "Ground" && !_isGrounded && rb.velocity.y <= 0f)
    { // Condition assumes all ground is tagged with "Ground".
        Debug.Log("Condition met!");
        DisableRB();
        EnableNavMeshAgent();
    */
    //_isGrounded = true;

    void OnDisable() {
		inputProvider.FindAction("Directional Movements").Disable();
		inputProvider.FindAction("Item Pickup").Disable();
		DragAndDrop.OnUIActionStart -= SetItemClickBool;
		DragAndDrop.OnSeedDrop -= GetItemDropPosition;
	}
}
