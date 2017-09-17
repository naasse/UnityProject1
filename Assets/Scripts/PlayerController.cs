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

    private int maxHP;
    private int currentHP;
    public Text statusText;

    private void Start()
    {
        maxHP = 20;
        currentHP = 20;
        rb = GetComponent<Rigidbody>();
        speed = 10.0f;
        rb.freezeRotation = true;
        attackStrength = 1;
        SetStatusText();
    }

    private void SetStatusText()
    {
        statusText.text = "HP: " + currentHP.ToString() + " / " + maxHP.ToString();
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
            GameController gameController = GameObject.Find("EventSystem").GetComponent<GameController>();
            //If hovering on enemy, click to attack
            if (gameController.IsPlayerTurn()) 
            {
                if (hit.collider.gameObject.tag == "Enemy" && Input.GetMouseButtonDown(0))
                {
                    EnemyController enemy = hit.collider.gameObject.GetComponent<EnemyController>();
                    enemy.TakeDamage(attackStrength);
                    gameController.UpdateEventLog("Player dealt " + attackStrength + " damage to " + enemy.name);
                    gameController.SetPlayerTurn(false);
                }
            }
        }
    }
    public void TakeDamage(int damageAmount)
    {
        currentHP -= damageAmount;
        SetStatusText();
        //Game over screen
    }
    public void Heal(int healAmount)
    {
        currentHP += healAmount;
        if (currentHP > maxHP)
            currentHP = maxHP;
        SetStatusText();
    }
    public int GetMaxHP()
    {
        return maxHP;
    }
    public int GetCurrentHP()
    {
        return currentHP;
    }
    public float GetPercentageHP()
    {
        return (float)currentHP / (float)maxHP;
    }
}
