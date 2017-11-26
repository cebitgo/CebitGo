using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using System;

public class SpawnDrones : MonoBehaviour
{
    public enum SpawnOrigin
    {
        Left = 0,
        Right = 1
    }

    [SerializeField()]
    private float _spawnDelay = 2.0f;

    [SerializeField()]
    private float _droneSpeedMin = 3.0f;
    [SerializeField()]
    private float _droneSpeedMax = 5.0f;

    [SerializeField()]
    private GameObject dronePrefab;
    [SerializeField()]
    private GameObject giftPrefab;

    [SerializeField()]
    private MeshFilter _spawnOriginLeft;
    [SerializeField()]
    private MeshFilter _spawnOriginRight;

    [SerializeField()]
    private UnityEvent _onFinished = new UnityEvent();



    private GameObject player;
    private float timer;



    // Use this for initialization
    void Start()
    {
        timer = 0.0f;
        player = Camera.main.gameObject;
    }

    private void OnEnable()
    {
        timer = 0.0f;
    }
    // Update is called once per frame
    void Update()
    {

        if (!FlyingDrone.goalReached)
        {
            timer += Time.deltaTime;
            if (timer > _spawnDelay)
            {
                timer -= _spawnDelay;

                SpawnOrigin spawnOrigin = (SpawnOrigin)Random.Range(0, 2);

                Vector3 startPos = _spawnOriginLeft.transform.position;
                Vector3 endPos = _spawnOriginRight.transform.position;
                Vector3 startDir = _spawnOriginRight.transform.position - _spawnOriginLeft.transform.position;

                Bounds bOrigin = new Bounds(Vector3.zero, Vector3.one);
                Transform tOrigin = null;
                Bounds bTarget = new Bounds(Vector3.zero, Vector3.one);
                Transform tTarget = null;


                if (spawnOrigin == SpawnOrigin.Left)
                {
                    bOrigin = _spawnOriginLeft.sharedMesh.bounds;
                    tOrigin = _spawnOriginLeft.transform;
                    bTarget = _spawnOriginRight.sharedMesh.bounds;
                    tTarget = _spawnOriginRight.transform;
                }
                else
                {
                    bOrigin = _spawnOriginRight.sharedMesh.bounds;
                    tOrigin = _spawnOriginRight.transform;
                    bTarget = _spawnOriginLeft.sharedMesh.bounds;
                    tTarget = _spawnOriginLeft.transform;
                }

                Vector2 startUV = new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                Vector2 endUV = new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

                startPos = new Vector3(bOrigin.extents.x * startUV.x - (bOrigin.extents.x / 2), bOrigin.extents.y * startUV.y - (bOrigin.extents.y / 2), 0);
                startPos = tOrigin.transform.position + tOrigin.transform.TransformVector(startPos);

                endPos = new Vector3(bTarget.extents.x * endUV.x - (bTarget.extents.x / 2), bTarget.extents.y * endUV.y - (bTarget.extents.y / 2), 0);
                endPos = tTarget.transform.position + tTarget.transform.TransformVector(endPos);

                startDir = (endPos - startPos).normalized;
                float speed = Random.Range(_droneSpeedMin, _droneSpeedMax);


                GameObject goDrone = Instantiate(dronePrefab);
                goDrone.transform.position = startPos - startDir * 2;

                FlyingDrone drone = goDrone.GetComponent<FlyingDrone>();
                drone.SetMovement(startDir, speed);

                Debug.DrawRay(startPos, startDir * 10.0f, Color.blue, 5.0f);
                Destroy(goDrone, 10.0f);
            }

            /*
            timer += Time.deltaTime;
            int seconds = (int)timer % 60;

            if (seconds > 4.0f)
            {
                GameObject newDrone = Instantiate(dronePrefab);

                float xpos = player.transform.position.x;

                float ypos = player.transform.position.y;
                float randomYPos = Random.Range(0, ypos + 2f);

                float zpos = player.transform.position.z;


                float angle = Random.Range(0, 359);

                float radius = 5f;

                float mineX = xpos + radius * Mathf.Cos(angle);
                float mineZ = zpos + radius * Mathf.Sin(angle);

                newDrone.transform.position = new Vector3(mineX, randomYPos, mineZ);
                timer = 0.0f;

                Destroy(newDrone, 10.0f);
            }
            */
        }
        else
        {
            if (_onFinished != null)
                _onFinished.Invoke();
            //this.enabled = false;
        }

    }
    public void SpawnGift(Transform target)
    {
        GameObject gift = GameObject.Instantiate(giftPrefab, target);
        gift.transform.localPosition = Vector3.zero;
        gift.transform.localRotation = Quaternion.identity;
        GameObject.Destroy(gift, 10.0f);
    }
}
