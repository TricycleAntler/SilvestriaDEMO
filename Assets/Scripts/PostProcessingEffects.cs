using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class PostProcessingEffects : MonoBehaviour
{
    public float focusDistanceNormal = 3f;
    public float focusDistanceDialogueMode = 1f;
    public Volume volume;
    private VolumeProfile volumeProfile;
    private void Awake()
    {
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
