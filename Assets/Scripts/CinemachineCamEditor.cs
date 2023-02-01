using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;


public class CinemachineCamEditor : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputProvider;
    [SerializeField] private CinemachineFreeLook freelook;
    [SerializeField] private float _rotationDegree = 90f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _zoomSpeed = 1f;
    [SerializeField] private float zoomAcceleration = 2.5f;
    [SerializeField] private float zoomInnerRange = 3f;
    [SerializeField] private float zoomOuterRange = 10f;

    private float currentMiddleRigRadius = 3f;
    private float newMiddleRigRadius = 3f;
    private float newAngle;
    private float camDistance;
    private bool isRotating;
    private bool mYAxisValCorrect = false;

    [SerializeField] private float zoomYAxis = 0;

    public float ZoomYAxis
    {
        get => zoomYAxis;
        set
        {
            zoomYAxis = value;
            AdjustCameraZoomIndex(ZoomYAxis);
        }

    }
    private void Awake()
    {
        inputProvider.FindActionMap("CameraControls").FindAction("Camera Rotation Left").performed += UpdateCinemachineCamRotationLeft;
        inputProvider.FindActionMap("CameraControls").FindAction("Camera Rotation Right").performed += UpdateCinemachineCamRotationRight;
        inputProvider.FindActionMap("CameraControls").FindAction("Camera Zoom").performed += x => ZoomYAxis = x.ReadValue<float>();
        inputProvider.FindActionMap("CameraControls").FindAction("Camera Zoom").canceled += x => ZoomYAxis = 0;
    }

    private void OnEnable()
    {
        inputProvider.FindAction("Camera Rotation Left").Enable();
        inputProvider.FindAction("Camera Rotation Right").Enable();
        inputProvider.FindAction("Camera Zoom").Enable();
    }

    void LateUpdate()
    {
        //its just a quick fix. find out why the value cannot be edited in the editor
        if(mYAxisValCorrect){
            freelook.m_YAxis.Value = 0.6f;
        }
        if(isRotating)
        {
            freelook.m_XAxis.Value = Quaternion.Slerp(Quaternion.Euler(0,freelook.m_XAxis.Value, 0), Quaternion.Euler(0,newAngle, 0), _rotationSpeed * Time.deltaTime).eulerAngles.y;
            if(Mathf.Abs(newAngle) == Mathf.Abs(freelook.m_XAxis.Value))
            {
                isRotating = false;
            }
        }

        UpdateCameraZoom();
    }

    private void UpdateCameraZoom()
    {
        //change the  Y axis value to zoom the camera in and out
        if(currentMiddleRigRadius == newMiddleRigRadius)
        {
            return;
        }
        currentMiddleRigRadius = Mathf.Lerp(currentMiddleRigRadius, newMiddleRigRadius, zoomAcceleration * Time.deltaTime);
        currentMiddleRigRadius = Mathf.Clamp(currentMiddleRigRadius, zoomInnerRange, zoomOuterRange);
        freelook.m_Orbits[1].m_Radius = currentMiddleRigRadius;
        freelook.m_Orbits[0].m_Height = freelook.m_Orbits[1].m_Radius;
        freelook.m_Orbits[2].m_Height = freelook.m_Orbits[1].m_Radius;
    }
    private void UpdateCinemachineCamRotationLeft(InputAction.CallbackContext context)
    {
        newAngle = freelook.m_XAxis.Value + _rotationDegree;
        isRotating = true;
    }
    private void UpdateCinemachineCamRotationRight(InputAction.CallbackContext context)
    {
        newAngle = freelook.m_XAxis.Value - _rotationDegree;
        isRotating = true;
    }

    public void AdjustCameraZoomIndex(float ZoomYAxis)
    {
        if (ZoomYAxis == 0)
        {
            return;
        }

        if(ZoomYAxis < 0)
        {
            newMiddleRigRadius = currentMiddleRigRadius + _zoomSpeed;
        }

        if(ZoomYAxis > 0)
        {
            newMiddleRigRadius = currentMiddleRigRadius - _zoomSpeed;
        }
    }

    private void OnDisable()
    {
        inputProvider.FindAction("Camera Rotation Left").Disable();
        inputProvider.FindAction("Camera Rotation Right").Disable();
        inputProvider.FindAction("Camera Zoom").Disable();
    }
}
