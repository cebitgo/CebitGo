using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDrone : MonoBehaviour
{

    public static bool goalReached;
    private static int _count;
    private GameObject player;
    private Vector3 direction;

    [SerializeField()]
    private GameObject _fxExplosion = null;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Main Camera");
        direction = player.transform.position - gameObject.transform.position;
        goalReached = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position + direction * 0.2f * Time.deltaTime;
        transform.position = newPosition;

        if (Input.GetKeyDown(KeyCode.K))
        {
            Kill(null);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        Kill(col.gameObject);
    }

    private void Kill(GameObject other)
    {
        _count++;
        if (_count == 3)
        {
            //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //cube.transform.position = this.gameObject.transform.position;
            goalReached = true;
            _count = 0;
        }

        GameObject.Instantiate(_fxExplosion, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
        if (other != null)
            Destroy(other);
    }
}
