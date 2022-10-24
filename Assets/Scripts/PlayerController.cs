using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

	private Rigidbody rb;
	private NavMeshAgent agent;

	[SerializeField] private float jumpForce;

	private bool _isGrounded = true;



	void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		if (agent == null)
		{
			gameObject.AddComponent<NavMeshAgent>();
			agent = GetComponent<NavMeshAgent>();
		}
		SetUpRigidbody();
	}

	void Update()
	{

		if (Input.GetMouseButton(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit, Mathf.Infinity))
			{
				agent.SetDestination(hit.point);
			}
		}

		if (Input.GetMouseButtonDown(1))
		{
			TryJump();
		}
	}

	public void TryJump()
	{
		if (_isGrounded) return;
		DoJump();
	}

	private void DoJump()
	{
		DisableNavMeshAgent();
		EnableRB();
		rb.AddRelativeForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
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
		rb.isKinematic = false;
		rb.useGravity = true;
	}

	private void EnableNavMeshAgent()
	{
		agent.updatePosition = true;
		agent.updateRotation = true;
		agent.isStopped = false;
	}

	private void DisableNavMeshAgent()
	{
		agent.SetDestination(transform.position); // I believe this will prevent the agent from break if we jump while moving.
		agent.updatePosition = false;
		agent.updateRotation = false;
		agent.isStopped = true;
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.collider != null && other.collider.tag == "Ground" && !_isGrounded)
		{ // Condition assumes all ground is tagged with "Ground".
			DisableRB();
			EnableNavMeshAgent();
			_isGrounded = true;
		}
	}
}