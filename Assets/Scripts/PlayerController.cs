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

    public int maxHP;
    private int currentHP;
    public Text statusText;

    private bool canMove=false;
    public float maxMovement;
    private float currentMovement;
    private GameObject movementRadius;

    private void Start()
    {
        currentHP = maxHP;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        SetStatusText();
        regenMovement();
        movementRadius = GameObject.Find("MoveRadius");
    }

    private void SetStatusText()
    {
        statusText.text = "HP: " + currentHP.ToString() + " / " + maxHP.ToString();
    }

    private void FixedUpdate()
    {
        //Movement
        move();
        print(currentMovement);

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
    public float GetPercentageMoveLeft()
    {
        return (float)currentMovement / (float)maxMovement;
    }

    public void move()
    {
        if (canMove)
        {
            if (currentMovement > 0) { 
                //Update movement to correct direction
                Vector3 movement = Quaternion.Euler(0, 45, 0) * new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                movement = movement * Time.deltaTime*speed;
                float movelength = movement.magnitude;
                float newMovelength = Mathf.Clamp(movelength, 0, currentMovement);
                //check if need to limmit movement
                if (movelength != newMovelength)
                {
                    movement = newMovelength * movement.normalized;
                    currentMovement -= movelength;
                }
                else currentMovement -= movelength;
                rb.MovePosition(rb.transform.position+movement);

                updateMovementRadius();
            }
        }
    }
    public void setCanMove(bool newMove)
    {
        this.canMove = newMove;
    }
    public void regenMovement()
    {
        currentMovement = maxMovement;
    }
    public void startTurn()
    {
        regenMovement();
        setCanMove(true);
    }
    public void endTurn()
    {
        setCanMove(false);
    }
    public void updateMovementRadius()
    {
        //scales radius to movement limmit
        movementRadius.transform.position = rb.transform.position+ new Vector3(0,-.6f,0);
        movementRadius.transform.localScale = new Vector3(currentMovement, .01f, currentMovement);
    }
    }
