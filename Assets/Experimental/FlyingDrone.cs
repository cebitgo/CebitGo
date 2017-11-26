using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDrone : MonoBehaviour
{

    public static bool goalReached;
    private static int _count;
    //private GameObject player;
    //private Vector3 direction;

    [SerializeField()]
    private GameObject _fxExplosion = null;

    private Vector3 _direction = Vector3.zero;
    private float _speed = 1.0f;

    // Use this for initialization
    void Start()
    {
        //player = GameObject.Find("Main Camera");
        //direction = player.transform.position - gameObject.transform.position;
        goalReached = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Vector3 newPosition = transform.position + direction * 0.2f * Time.deltaTime;
        //transform.position = newPosition;
        transform.position += _direction * _speed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.K))
        {
            Kill(null);
        }
    }

    public void SetMovement(Vector3 direction, float speed)
    {
        _direction = direction;
        _speed = speed;
    }

    void OnTriggerEnter(Collider col)
    {
        Bullet bullet = col.GetComponent<Bullet>();
        if (bullet != null)
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

        if (_fxExplosion != null)
            GameObject.Instantiate(_fxExplosion, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
        if (other != null)
            Destroy(other);
    }
}