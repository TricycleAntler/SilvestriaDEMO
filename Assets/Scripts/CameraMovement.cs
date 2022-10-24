using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{

    public float rotationAmount;
    public float rotationSpeed;
    public Vector3 destEuler = new Vector3(0, 0, 0);
    private Vector3 currEuler = new Vector3(30, 0, 0);

    // Use this for initialization
    void Start()
    {

        rotationAmount = 90.0f;
        rotationSpeed = 20.0f;
        transform.eulerAngles = destEuler;
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            destEuler.y += rotationAmount;
        }

        currEuler = Vector3.Lerp(currEuler, destEuler, Time.deltaTime * rotationSpeed);
        transform.eulerAngles = currEuler;


    }
}