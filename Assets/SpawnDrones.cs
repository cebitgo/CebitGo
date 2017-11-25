using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDrones : MonoBehaviour {

	
	public GameObject dronePrefab;
	float timer;

	// Use this for initialization
	void Start () {
		timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
    	int seconds = (int) timer % 60;

		if (seconds > 2.0f)
		{
			GameObject newDrone = Instantiate(dronePrefab);
			//newDrone.transform.position = new Vector3(Random.Range(-1.5f, 1.5f), 0.5f, 1.4f);
			newDrone.transform.position = new Vector3(0, 0, 5f);
			timer = 0.0f;

			Destroy(newDrone, 15.0f);
		} 
	}
}
