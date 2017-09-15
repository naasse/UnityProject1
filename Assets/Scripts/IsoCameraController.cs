using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IsoCameraController: MonoBehaviour
{
    public Camera camera;
    public Vector3 offset;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        moveCamera();
    }

    private void moveCamera()
    {
        camera.transform.position = gameObject.transform.position + offset;
    }

}
