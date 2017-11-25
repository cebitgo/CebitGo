using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDrone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f * Time.deltaTime);
		transform.position = newPosition;
	}

	void OnCollisionEnter()
	{
		Destroy(this.gameObject);
	}


}
