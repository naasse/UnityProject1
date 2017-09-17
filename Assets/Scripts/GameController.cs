using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Image enemyHealth;

    private void FixedUpdate()
    {
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
                //TODO - some kind of active player component, as could be more than one player unit, also the flexibility of using same script to attack player from enemy
                //Currently handling click event in PlayerController, and would like to migrate it here
                if (Input.GetMouseButtonDown(0))
                {
                    //enemy.TakeDamage(attackStrength);
                }

                //Get enemy remaining HP (0..1)
                float enemyRemainingHP = enemy.GetPercentageHP();

                //Load the fill percentage, hide if enemy defeated
                //TODO - seems to be an issue with the prefab having an Image from saved Canvas. Can't seem to get healthBar to work on the prefab
                enemyHealth.gameObject.transform.Find("Health").GetComponent<Image>().fillAmount = enemyRemainingHP;
                if (enemyRemainingHP <= 0 || hit.collider.gameObject.tag != "Enemy")
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