using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBulletEmitter : MonoBehaviour
{
    [SerializeField()]
    private GameObject _bulletPrefab = null;


    private Camera _mainCamera = null;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                GameObject bullet = Instantiate(_bulletPrefab);
                bullet.transform.parent = _mainCamera.transform;
                bullet.transform.localPosition = Vector3.zero;
                bullet.transform.localRotation = Quaternion.identity;
                //bullet.transform.rotation = Quaternion.LookRotation(_mainCamera.transform.forward, _mainCamera.transform.up);
                bullet.transform.parent = null;
                bullet.GetComponent<Rigidbody>().AddForce(_mainCamera.transform.forward * 6, ForceMode.VelocityChange);
                GameObject.Destroy(bullet, 5.0f);
            }
        }

#if UNITY_EDITOR

        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(_bulletPrefab);//, _mainCamera.transform.position, _mainCamera.transform.rotation);
            bullet.transform.parent = _mainCamera.transform;
            bullet.transform.localPosition = Vector3.zero;
            bullet.transform.localRotation = Quaternion.identity;
            //bullet.transform.rotation = Quaternion.LookRotation(_mainCamera.transform.forward, _mainCamera.transform.up);
            bullet.transform.parent = null;
            bullet.GetComponent<Rigidbody>().AddForce(_mainCamera.transform.forward * 6, ForceMode.VelocityChange);
            GameObject.Destroy(bullet, 5.0f);
        }

#endif

    }
}