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
    public Image enemyHealth;

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
            //If hovering on enemy
            if (hit.collider.gameObject.tag == "Enemy")
            {
                //Show healthbar
                enemyHealth.gameObject.SetActive(true);
                EnemyController enemy = hit.collider.gameObject.GetComponent<EnemyController>();

                //Click to Attack
                if (Input.GetMouseButtonDown(0))
                {
                    enemy.TakeDamage(attackStrength);
                }

                //Get enemy remaining HP (0..1)
                float enemyRemainingHP = enemy.GetPercentageHP();

                //Load the fill percentage, hide if enemy defeated
                //TODO - seems to be an issue with the prefab having an Image from saved Canvas. Can't seem to get healthBar to work on the prefab
                enemyHealth.gameObject.transform.Find("Health").GetComponent<Image>().fillAmount = enemyRemainingHP;
                if (enemyRemainingHP <= 0)
                    enemyHealth.gameObject.SetActive(false);
            }
            else
            {
                //Hide healthbar if not hovering on an enemy
                //TODO - may not be the best way to handle this - only fires on hovering on another game object
                enemyHealth.gameObject.SetActive(false);
            }
        }
    }
}
