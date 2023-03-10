using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertigoCamera : MonoBehaviour
{
    [SerializeField] private bool _zoomIn;
    [SerializeField] private float _duration, _strength = 1;
    [SerializeField] private Camera _camera;

    private float _defaultFOV, _vertigoTimer;

    void Start()
    {
        _defaultFOV = _camera.fieldOfView;
        _vertigoTimer = _duration;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.localPosition = CalculateLocalPositionBasedOnDuration(_duration, _strength);
        _camera.fieldOfView = CalculateVertigoFOVBasedOnMotion(_defaultFOV, _strength);
    }

    private Vector3 CalculateLocalPositionBasedOnDuration(float duration, float strength)
    {
        Vector3 targetPos = transform.localPosition;

        if (_zoomIn)
        {
            _vertigoTimer -= Time.deltaTime;

            targetPos.x = strength * (1 - _vertigoTimer / duration);

            if (_vertigoTimer <= duration / 2)
            {
                _zoomIn = false;
            }
        }
        else
        {
            if (_vertigoTimer <= duration / 2 && _vertigoTimer > 0)
            {
                _vertigoTimer -= Time.deltaTime;

                targetPos.x = strength * (_vertigoTimer / duration);
            }
            else
            {
                _vertigoTimer = duration;
            }
        }

        return targetPos;
    }

    private float CalculateVertigoFOVBasedOnMotion(float defaultFOV, float strength)
    {
        float vertigoFOV = defaultFOV + (strength); //7 gets the desired POV effect

        float percentageMoved = 1 - ((strength - transform.localPosition.x) / strength);

        float targetFOV = defaultFOV + ((vertigoFOV - defaultFOV) * percentageMoved);

        return targetFOV;
    }
}
