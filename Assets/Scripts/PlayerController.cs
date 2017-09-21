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
    public float maxClimbHeight;
    public float climbHeightResolution;
    public float maxFallDist;

    public Inventory inventory;

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


        if(Input.GetKeyDown(KeyCode.E)){
            print("pressedE");
            ItemScript temp = GameObject.FindGameObjectsWithTag("Item")[0].GetComponent<ItemScript>();
            if((temp.transform.position - rb.transform.position).magnitude < 2)
            {
                PickupClickedItem(temp);
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            print("pressedQ");
            for(int i = 0; i < inventory.items.Length; i++)
            {
                if (inventory.items[i] != null)
                {
                    DropClickedItem(inventory.items[i]);
                }
            }

        }

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
                movement = movement * Time.deltaTime * speed;
                float movelength = movement.magnitude;
                float newMovelength = Mathf.Clamp(movelength, 0, currentMovement);
                //check if need to limmit movement
                if (movelength != newMovelength)
                {
                    movement = newMovelength * movement.normalized;
                }
                //checks if current movement leads into a pit greater than maxFallDist;
                if (!pitCheck(movement)) {
                    rb.MovePosition(rb.transform.position + movement);
                    currentMovement -= movement.magnitude;
                    updateMovementRadius();
                }
                
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

    private bool pitCheck(Vector3 nextStep)
    {
        bool tooFar = false;
        if(!Physics.Raycast(rb.transform.position+nextStep,new Vector3(0,-1,0), maxFallDist)){
            tooFar = true;
            print("Found nothing");
        }
        print("Found nothing");
        return tooFar;
    }
    public void updateMovementRadius()
    {
        //scales radius to movement limmit
        movementRadius.transform.position = rb.transform.position+ new Vector3(0,-.6f,0);
        movementRadius.transform.localScale = new Vector3(currentMovement, .01f, currentMovement);
    }

    public void PickupClickedItem(ItemScript item)
    {
        if (item.GetComponent<ItemScript>().pickupable)
        {
            inventory.pickup(item);
        }
    }
    public ItemScript DropClickedItem(ItemScript item)
    {
        return inventory.drop(item,rb.transform.position);
    }
    }
