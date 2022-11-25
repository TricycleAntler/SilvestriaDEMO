using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

	private Rigidbody rb;
	private NavMeshAgent agent;
	[SerializeField] private float jumpForce;
	[SerializeField] private float slideOnLand;

	//private bool _isGrounded = true;
	private Vector3 _horizontalJumpVelocity = Vector3.zero;
	private bool itemDragStarted;


	void Awake()
	{
		itemDragStarted = false;
		agent = GetComponent<NavMeshAgent>();
		if (agent == null)
		{
			gameObject.AddComponent<NavMeshAgent>();
			agent = GetComponent<NavMeshAgent>();
		}
		//SetUpRigidbody();
	}

	void OnEnable() {
		DragAndDrop.OnUIActionStart += SetItemDragBool;
	}

	void Update()
	{

		if (Input.GetMouseButton(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			//this bool checks if there is something over the UI
			bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
			if (Physics.Raycast(ray, out hit, Mathf.Infinity) && !isOverUI && !itemDragStarted)
			{
				ItemTemplate itemTemplate = hit.collider.GetComponent<ItemTemplate>();
				if(itemTemplate != null){
					Inventory inventoryObject = this.GetComponent<Player>().GetPlayerInventory();
					inventoryObject.AddItem(itemTemplate.GetItem());
					itemTemplate.DestroyItemTemplate();
				}
				agent.SetDestination(hit.point);
			}
			if(itemDragStarted) {
				Debug.Log("item Dragging started");
			}
		}
	}

	public void SetItemDragBool(bool dragVal) {
		itemDragStarted = dragVal;
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
		DragAndDrop.OnUIActionStart -= SetItemDragBool;
	}
}
