using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBinding : MonoBehaviour, IUpdate
{
    public RectTransform PlayerCanvas;

    private Vector3 _diffPositionPlayerCanvas;
    private Quaternion _rotationPlayerCanvas;

    private Transform _transformParent;
    private Vector3 _diffPositionParent;
    private Quaternion _rotation;

    void Start()
    {
        _transformParent = transform.parent.transform;
        _diffPositionParent = _transformParent.position - transform.position;
        _rotation = transform.rotation;

        _diffPositionPlayerCanvas = PlayerCanvas.position - transform.position;
        _rotationPlayerCanvas = new Quaternion(_rotation.x, _rotation.y, -_rotation.z, _rotation.w);
    }

    public void LaunchUpdate()
    {
        transform.position = _transformParent.position - _diffPositionParent;
        transform.rotation = _rotation;

        PlayerCanvas.position = transform.position + _diffPositionPlayerCanvas;
        PlayerCanvas.rotation = _rotationPlayerCanvas;
    }
}
