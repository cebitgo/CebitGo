using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using System;

public class SpawnDrones : MonoBehaviour
{
    [SerializeField()]
    private GameObject dronePrefab;
    [SerializeField()]
    private GameObject giftPrefab;

    private GameObject player;
    private float timer;

    [SerializeField()]
    private UnityEvent _onFinished = new UnityEvent();

    // Use this for initialization
    void Start()
    {
        timer = 0.0f;
        player = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        if (!FlyingDrone.goalReached)
        {
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

                Destroy(newDrone, 15.0f);
            }
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
        GameObject.Destroy(gift, 25.0f);
    }
}
