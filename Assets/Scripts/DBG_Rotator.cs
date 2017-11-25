using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBG_Rotator : MonoBehaviour
{

    public void RotLeft()
    {
        this.transform.Rotate(Vector3.up, -15.0f, Space.Self);
    }

    public void RotRight()
    {
        this.transform.Rotate(Vector3.up, 15.0f, Space.Self);
    }

}