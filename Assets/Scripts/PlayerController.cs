using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public int attackStrength;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 10.0f;
        rb.freezeRotation = true;
        attackStrength = 1;
    }

    private void FixedUpdate()
    {
        //Movement
        Vector3 movement = Quaternion.Euler(0, 45, 0) * new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        rb.AddForce(speed * movement);

        //MouseListener
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            //If hovering on enemy, click to attack
            if (hit.collider.gameObject.tag == "Enemy" && Input.GetMouseButtonDown(0))
            {
                EnemyController enemy = hit.collider.gameObject.GetComponent<EnemyController>();
                enemy.TakeDamage(attackStrength);
            }
        }
    }
}
