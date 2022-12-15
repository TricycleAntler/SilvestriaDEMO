using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAMFollowPlayer : MonoBehaviour
{
    private Transform _thisTransform;
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _rotationDegree = 10f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _rotationThreshold = Mathf.Epsilon;


    private bool _isRotating = false;
    private Quaternion _rotationTarget = Quaternion.identity;
    private float _currentAngle = 0f;

    void Awake() => _thisTransform = transform;
    void Update()
    {
        _thisTransform.position = _targetTransform.position;
        if (Input.GetKeyDown("q") || (Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            //UpdateRotationTarget(-_rotationDegree);
        }
        if (Input.GetKeyDown("e") || (Input.GetKeyDown(KeyCode.RightArrow)))
        {
            //UpdateRotationTarget(_rotationDegree);
        }
        if (_isRotating)
        {
            _thisTransform.rotation = Quaternion.Slerp(_thisTransform.rotation, _rotationTarget, _rotationSpeed * Time.deltaTime);
            float rotDelta = _thisTransform.localEulerAngles.y + _rotationThreshold - _rotationTarget.eulerAngles.y;
            rotDelta = Mathf.Sign(rotDelta) > 0f ? _rotationTarget.eulerAngles.y + _rotationThreshold - _thisTransform.localEulerAngles.y : rotDelta;
            if (Mathf.Sign(rotDelta) > 0f)
            {
                _thisTransform.rotation = _rotationTarget;
                _isRotating = false;
            }
        }
    }

    private void UpdateRotationTarget(float degree)
    {
        _currentAngle += degree;
        if (_currentAngle > 360f) _currentAngle = degree;
        else if (_currentAngle < 0f) _currentAngle = 360f - Mathf.Abs(degree);
        _rotationTarget = Quaternion.AngleAxis(_currentAngle, _thisTransform.up);
        _isRotating = true;
    }
}
