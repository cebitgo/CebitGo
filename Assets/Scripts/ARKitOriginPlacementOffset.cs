using System.Collections;
using UnityEngine;
using UnityEngine.XR.iOS;

public class ARKitOriginPlacementOffset : MonoBehaviour
{

    [SerializeField()]
    private Transform _offsetTarget = null;

    public void CalculateWorldCoordinateSystem(Transform cameraParent)
    {
        Matrix4x4 camPose = UnityARSessionNativeInterface.GetARSessionNativeInterface().GetCameraPose();

        //Quaternion camRot = UnityARMatrixOps.GetRotation(camPose);
        //Quaternion camWorldYRot = Quaternion.Inverse(Quaternion.Euler(0, camRot.eulerAngles.y, 0));

        //cameraParent.transform.localRotation = Quaternion.LookRotation(ARKitOriginPlacement.WorldForward, -Vector3.Cross(ARKitOriginPlacement.WorldRight, ARKitOriginPlacement.WorldForward)) * camWorldYRot;

        Input.compass.enabled = true;
        // Orient an object to point to magnetic north.
        //cameraParent.transform.rotation = Quaternion.Euler(0, -Input.compass.magneticHeading + (180), 0); // Quaternion.LookRotation(ARKitOriginPlacement.WorldForward, -Vector3.Cross(ARKitOriginPlacement.WorldRight, ARKitOriginPlacement.WorldForward)) * Quaternion.Euler(0, -Input.compass.magneticHeading, 0);
        //cameraParent.transform.Rotate(0, -31, 0, Space.Self);
        cameraParent.transform.position = ARKitOriginPlacement.ZeroY(_offsetTarget.localPosition);

    }

}