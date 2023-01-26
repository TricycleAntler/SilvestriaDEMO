using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLookToCamera : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 rotationVector;

    void Awake() {
        mainCam = Camera.main;
        rotationVector = transform.rotation.eulerAngles;
    }
    // Update is called once per frame
    void Update()
    {
        rotationVector.y = mainCam.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(rotationVector);
    }
}
