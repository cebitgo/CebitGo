using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.iOS;


public class ARKitOriginPlacement : MonoBehaviour
{
    public enum ARKitOriginComponent
    {
        Origin,
        Right,
        Forward
    }

    private static Vector3 _worldPositionAROrigin = Vector3.zero;
    private static Vector3 _worldPositionARRight = Vector3.right;
    private static Vector3 _worldPositionARForward = Vector3.forward;


    public Transform m_HitTransform;

    public static Vector3 WorldRight
    { get { return ZeroY(_worldPositionARRight - _worldPositionARForward).normalized; } }
    public static Vector3 WorldForward
    { get { return ZeroY(_worldPositionARRight - _worldPositionARForward).normalized; } }

    [SerializeField()]
    private ARKitOriginComponent _component = ARKitOriginComponent.Origin;
    [SerializeField()]
    private Color _indicatorColor;
    [SerializeField()]
    private Shader _indicatorShader;

    [SerializeField()]
    private UnityEvent _onPlaced = new UnityEvent();


    public static Vector3 ZeroY(Vector3 value)
    {
        return new Vector3(value.x, 0.0f, value.z);
    }

    private void Start()
    {
        Material m = new Material(_indicatorShader);
        m.SetColor("_Color", _indicatorColor);
        this.GetComponent<MeshRenderer>().material = m;
    }

    public static bool HitTestFiltered(ARPoint point, ARHitTestResultType resultTypes)
    {
        UnityARSessionNativeInterface session = UnityARSessionNativeInterface.GetARSessionNativeInterface();
        List<ARHitTestResult> hitResults = session.HitTest(point, resultTypes);
        if (hitResults.Count > 0)
        {
            foreach (var hitResult in hitResults)
            {
                return true;
            }
        }
        return false;
    }

    public void PlaceOnCamera(Camera camera)
    {
        Vector3 viewPortPos = Vector3.zero;
        switch (_component)
        {
            case ARKitOriginComponent.Origin:
                viewPortPos = Camera.main.ScreenToViewportPoint(Vector3.forward);
                break;
            case ARKitOriginComponent.Right:
                viewPortPos = Camera.main.ScreenToViewportPoint(new Vector3(0.5f, 0.0f, 1.0f));
                break;
            case ARKitOriginComponent.Forward:
                viewPortPos = Camera.main.ScreenToViewportPoint(new Vector3(0.0f, 0.5f, 1.0f));
                break;
        }
        Vector3 screenPosition = Camera.main.ScreenToViewportPoint(viewPortPos);
        ARPoint point = new ARPoint
        {
            x = screenPosition.x,
            y = screenPosition.y
        };

        // prioritize reults types
        ARHitTestResultType[] resultTypes = {
                        ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent,
                        ARHitTestResultType.ARHitTestResultTypeHorizontalPlane,
                        ARHitTestResultType.ARHitTestResultTypeFeaturePoint
                    };
        foreach (ARHitTestResultType resultType in resultTypes)
        {
            UnityARSessionNativeInterface session = UnityARSessionNativeInterface.GetARSessionNativeInterface();
            List<ARHitTestResult> hitResults = session.HitTest(point, resultType);
            if (hitResults.Count > 0)
            {
                foreach (var hitResult in hitResults)
                {
                    m_HitTransform.position = UnityARMatrixOps.GetPosition(hitResult.worldTransform);
                    m_HitTransform.rotation = UnityARMatrixOps.GetRotation(hitResult.worldTransform);
                }
            }
        }
    }

    public void CalculateWorldCoordinateSystem(Transform cameraParent)
    {
        cameraParent.transform.localRotation = Quaternion.LookRotation(WorldForward, Vector3.Cross(WorldRight, WorldForward));
    }

    bool HitTestWithResultType(ARPoint point, ARHitTestResultType resultTypes)
    {
        UnityARSessionNativeInterface session = UnityARSessionNativeInterface.GetARSessionNativeInterface();
        List<ARHitTestResult> hitResults = session.HitTest(point, resultTypes);
        if (hitResults.Count > 0)
        {
            foreach (var hitResult in hitResults)
            {
                Debug.Log("Got hit!");
                m_HitTransform.position = UnityARMatrixOps.GetPosition(hitResult.worldTransform);
                if (_component == ARKitOriginComponent.Origin)
                    _worldPositionAROrigin = UnityARMatrixOps.GetPosition(hitResult.worldTransform);
                if (_component == ARKitOriginComponent.Right)
                    _worldPositionARRight = UnityARMatrixOps.GetPosition(hitResult.worldTransform);
                if (_component == ARKitOriginComponent.Forward)
                    _worldPositionARForward = UnityARMatrixOps.GetPosition(hitResult.worldTransform);
                m_HitTransform.rotation = UnityARMatrixOps.GetRotation(hitResult.worldTransform);
                Debug.Log(string.Format("x:{0:0.######} y:{1:0.######} z:{2:0.######}", m_HitTransform.position.x, m_HitTransform.position.y, m_HitTransform.position.z));
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && m_HitTransform != null)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);
                ARPoint point = new ARPoint
                {
                    x = screenPosition.x,
                    y = screenPosition.y
                };

                // prioritize reults types
                ARHitTestResultType[] resultTypes = {
                        ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent,
                        ARHitTestResultType.ARHitTestResultTypeHorizontalPlane,
                        ARHitTestResultType.ARHitTestResultTypeFeaturePoint
                    };

                foreach (ARHitTestResultType resultType in resultTypes)
                {
                    if (HitTestWithResultType(point, resultType))
                    {
                        if (_onPlaced != null)
                            _onPlaced.Invoke();
                        return;
                    }
                }
            }
        }
    }


}

