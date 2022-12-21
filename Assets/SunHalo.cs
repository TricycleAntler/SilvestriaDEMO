using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunHalo : MonoBehaviour
{
    public float haloSize = 1.0f; // Adjust this value to control the size of the halo
    public Color haloColor = Color.yellow; // Adjust this value to control the color of the halo

    private void OnDrawGizmos()
    {
        // Draw the halo around the sun object
        Gizmos.color = haloColor;
        Gizmos.DrawWireSphere(transform.position, haloSize);
    }
}

