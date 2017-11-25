using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour {

	public GameObject bulletPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space"))
		{
			GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

			// Add velocity to the bullet
			bullet.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 2) * 6;
			Destroy(bullet, 10.0f);
		}
            
	}
}
