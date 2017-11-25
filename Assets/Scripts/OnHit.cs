using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHit : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
        //Destroy(transform.parent.gameObject);
    }
}
