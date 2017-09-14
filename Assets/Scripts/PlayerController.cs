using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private float speed;
    private float jumpPower;
    public String jumpButton;

    private bool isTouchingMap;
    private Rigidbody rb;
    private int count;
    private int jumpPowerCount;
    private int speedPowerCount;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 10.0f;
        jumpPower = 0.0f;
        count = 0;
        jumpPowerCount = 0;
        speedPowerCount = 1;
    }

    private void FixedUpdate()
    {

        float jumpNow = 0.0f;
        //What if not 0 plane? Seems delayed, too - can't jump again for a few seconds after landing.
        if (Input.GetKeyDown(jumpButton) && GetComponent<Rigidbody>().transform.position.y <= 0.55f)
        {
            jumpNow = jumpPower;
        }
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), jumpNow, Input.GetAxis("Vertical"));
        rb.AddForce(speed * movement);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TAG NAME"))
        {
            //LOGIC
        }
        else  if (other.gameObject.CompareTag("Enemy"))
        {
            //LOGIC
        }
    }
}
