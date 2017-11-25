using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDrone : MonoBehaviour {

	public static bool goalReached;
	int pointCount;
	GameObject player;
	private Vector3 direction;
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Main Camera");
		direction = player.transform.position - gameObject.transform.position;
		goalReached = false;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPosition = transform.position + direction * 0.2f * Time.deltaTime;
		transform.position = newPosition;		
	}

	void OnCollisionEnter(Collision col)
	{
		++pointCount;
		if (pointCount == 3)
		{
			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        	cube.transform.position = this.gameObject.transform.position;
			goalReached = true;
			pointCount = 0;
		}
		Destroy(this.gameObject);
		Destroy(col.gameObject);
	}
}
