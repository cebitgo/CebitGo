using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideRotator : MonoBehaviour
{

    [SerializeField()]
    private Transform _target = null;

    public void RotateY(float angle)
    {
        _target.eulerAngles = new Vector3(0, angle, 0);
    }

}