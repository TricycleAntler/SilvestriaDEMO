using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
      public Transform target;
      public Vector3 offset;

    // Camera rotation
    private float targetAngle = 0;
    const float rotationAmount = 1.5f;
    public float rotationDistance = 1.0f;
    public float rotationSpeed = 1.0f;

    // Update is called once per frame
    void Update()


    {
        //Camera follows player
        transform.position = target.position + offset;

        // Camera Rotation

        if (Input.GetKeyDown("q") || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetAngle -= 90.0f;
        }

        else if (Input.GetKeyDown("e") || Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetAngle += 90.0f;
        }

        if (targetAngle != 0)
        {
            Rotate();
        }
    }

    // Camera Rotation
    protected void Rotate()
    {

        float step = rotationSpeed * Time.deltaTime;
        float orbitCircumfrance = 2F * rotationDistance * Mathf.PI;
        float distanceDegrees = (rotationSpeed / orbitCircumfrance) * 360;
        float distanceRadians = (rotationSpeed / orbitCircumfrance) * 2 * Mathf.PI;

        if (targetAngle > 0)
        {
            transform.RotateAround(target.transform.position, Vector3.up, -rotationAmount);
            targetAngle -= rotationAmount;
        }
        else if (targetAngle < 0)
        {
            transform.RotateAround(target.transform.position, Vector3.up, rotationAmount);
            targetAngle += rotationAmount;
        }

    }
}
