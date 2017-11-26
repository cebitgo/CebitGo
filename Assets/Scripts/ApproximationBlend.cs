using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproximationBlend : MonoBehaviour
{

    [SerializeField()]
    private Transform _reference = null;

    [SerializeField()]
    private float _minDistance = 1.5f;
    [SerializeField()]
    private float _maxDistance = 2.5f;

    [SerializeField()]
    private CanvasGroup _canvasGroup = null;


    private Camera _mainCamera = null;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }


    private void Update()
    {
        float dist = Vector3.Distance(_mainCamera.transform.position, _reference.position);
        float blend = Mathf.Clamp((dist - _minDistance) + (dist / (_maxDistance - _minDistance)), 0, 1);
        blend = 1 - blend;
        _canvasGroup.alpha = blend;
        _canvasGroup.interactable = blend > 0.25f;
    }



}
