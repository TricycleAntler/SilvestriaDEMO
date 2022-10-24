// BillboardSprite.cs
using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour
{

	public Transform Target;
	private Transform MyTransform;
	public bool alignNotLook = true;

	// Use this for initialization
	void Start()
	{
		MyTransform = this.transform;
		Target = Camera.main.transform;
	}

	// Update is called once per frame
	void LateUpdate()
	{
		var target = Camera.main.transform.position;
		target.y = transform.position.y;
		transform.LookAt(target);
	}
}