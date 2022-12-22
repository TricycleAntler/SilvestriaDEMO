using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour
{

	private Rigidbody rb;
	private NavMeshAgent agent;
	[SerializeField] private float jumpForce;
	[SerializeField] private float slideOnLand;
	[SerializeField] private UI_Inventory uiInventory;
	[SerializeField] private InputActionAsset inputProvider;
	[SerializeField] private float moveSpeed = 2f;
	private Vector3 _horizontalJumpVelocity = Vector3.zero;
	private Vector3 dropPosition;
	private Vector2 moveVals;
	private bool itemDragStarted;
	private bool inventoryOpened;
	private bool playerAutoMove;

    public bool PlayerAutoMove { get => playerAutoMove; set => playerAutoMove = value; }

    void Awake()
	{
		inventoryOpened = false;
		itemDragStarted = false;
		agent = GetComponent<NavMeshAgent>();
		if (agent == null)
		{
			gameObject.AddComponent<NavMeshAgent>();
			agent = GetComponent<NavMeshAgent>();
		}
		//SetUpRigidbody();
		//inputProvider.FindActionMap("PlayerMovements").FindAction("Directional Movements").performed += ManagePlayerMovement;
	}

	void OnEnable() {
		inputProvider.FindAction("Directional Movements").Enable();
		inputProvider.FindAction("Item Pickup").Enable();
		DragAndDrop.OnUIActionStart += SetItemDragBool;
		DragAndDrop.OnSeedDrop += GetItemDropPosition;
	}

	void Update()
	{
		MoveCharacter();
		//player moves to the position where the seed is dropped
		if(PlayerAutoMove) {
			agent.SetDestination(dropPosition);
			PlayerAutoMove = false;
		}
		SetInventoryActiveStatus();
	}

	public void OnPlayerMove(InputAction.CallbackContext context) {
		moveVals = context.ReadValue<Vector2>();
	}

	public void OnMouseClick(InputAction.CallbackContext context) {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
		//this bool checks if there is something over the UI
		if (Physics.Raycast(ray, out hit, Mathf.Infinity) && !inventoryOpened && !itemDragStarted)
		{
			ItemTemplate itemTemplate = hit.collider.GetComponent<ItemTemplate>();
			if(itemTemplate != null) {
				Inventory inventoryObject = this.GetComponent<Player>().GetPlayerInventory();
				inventoryObject.AddItem(itemTemplate.GetItem());
				itemTemplate.DestroyItemTemplate();
			}
		}
	}

	private void MoveCharacter() {
		Vector3 movement = new Vector3(moveVals.y,0f,-moveVals.x);
		if(movement != Vector3.zero) {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
			transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
		}
	}

	public void SetItemDragBool(bool dragVal) {
		itemDragStarted = dragVal;
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
		dropPosition = position;
		PlayerAutoMove = true;
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
		DragAndDrop.OnUIActionStart -= SetItemDragBool;
		DragAndDrop.OnSeedDrop -= GetItemDropPosition;
	}
}
