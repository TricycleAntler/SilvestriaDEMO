using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public GameObject Rotationtarget;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;

        if (Input.GetKey ("e"))
        {
            transform.RotateAround(Rotationtarget.transform.position, Vector3.up, 20 * Time.deltaTime);
        }
    }

}
