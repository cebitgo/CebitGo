using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class SpawnDrones : MonoBehaviour {

	public GameObject dronePrefab;
	GameObject player;
	float timer;
	FlyingDrone droneScript;

	// Use this for initialization
	void Start () {
		timer = 0.0f;
		player = GameObject.Find("Main Camera"); 
		GameObject drone = GameObject.Find("Drohne");
        droneScript = drone.GetComponent<FlyingDrone>();	
	}
	
	// Update is called once per frame
	void Update () {

		if (!FlyingDrone.goalReached)
		{
			timer += Time.deltaTime;
			int seconds = (int) timer % 60;

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
		
	}
}
