using UnityEngine;
using System.Collections;

public class CameraFacingMarker : MonoBehaviour
{

    private Camera _thisCamera;
    public float scaleMin = 0.1f;
    public float scaleMax = 5;
    public float scaleFactor = 1;


    // Use this for initialization
    void Awake()
    {
        _thisCamera = Camera.main;
    }

    void LateUpdate()
    {
        //transform.LookAt(transform.position + _thisCamera.transform.rotation * Vector3.forward,
        //                 _thisCamera.transform.rotation * Vector3.up);
        transform.rotation = Quaternion.LookRotation(this.transform.position - _thisCamera.transform.position, Vector3.up);


        //float tempScale = scaleMin + ((1 - (Mathf.Clamp(Vector3.Distance(_thisCamera.transform.position, transform.position), 0, 1250) / 1250)) * (scaleMax - scaleMin));
        //transform.localScale = new Vector3(tempScale, tempScale, 1);
    }
}


