using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class PostProcessingEffects : MonoBehaviour
{
    public float focusDistanceNormal; // = 4f;
    public float focusDistanceInteraction; // = 1f;
    public Volume volume;
    private VolumeProfile volumeProfile;
    private void Awake()
    {
        focusDistanceNormal = 4f;
        focusDistanceInteraction = 1f;
        volumeProfile = volume.sharedProfile;
        ChangeDepthOfField(focusDistanceNormal);
    }
    public void ChangeDepthOfField(float val)
    {
        if (volumeProfile.TryGet<DepthOfField>(out DepthOfField dof))
        {
            dof.focusDistance.value = val;
        }
    }

}
