using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set;}

    private void Awake() {
        Instance = this;
    }

    [SerializeField] private Transform _marketTopdownTransform;
    [SerializeField] private Transform _marketPovTransform;

    private void Start() {
        SetCameraTopdown();
    }

    public void SetCameraTopdown()
    {
        /* GetComponent<Camera>().orthographic = false; */
        transform.DOMove(_marketTopdownTransform.position, 1f);
        transform.DORotate(_marketTopdownTransform.eulerAngles, 1f);
    }

    public void SetCameraPov()
    {
        transform.DOMove(_marketPovTransform.position, 1f);
        transform.DORotate(_marketPovTransform.eulerAngles, 1f);
        //.OnComplete(() => GetComponent<Camera>().orthographic = true);
    }

}
