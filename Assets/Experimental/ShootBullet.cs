using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour {

	public GameObject bulletPrefab;
	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space"))
		{
			GameObject bullet = Instantiate(bulletPrefab, player.transform.position, player.transform.rotation);
			bullet.GetComponent<Rigidbody>().velocity = player.transform.forward * 6;
			Destroy(bullet, 10.0f);
		}
            
	}
}
