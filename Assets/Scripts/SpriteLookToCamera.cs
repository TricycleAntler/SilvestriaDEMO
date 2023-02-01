using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLookToCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotationVector = transform.rotation.eulerAngles;
        rotationVector.y = Camera.main.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(rotationVector);
    }
}
