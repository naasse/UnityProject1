using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : UnitController
{
    private int healCount =5;
    private int healStrength = 20;
    private GameController gameController;


    public Text statusText;
    public Text inventoryText;

    private float currentMovement;
    private GameObject movementRadius;
    public float maxClimbHeight;
    public float climbHeightResolution;
    public float maxFallDist;

    private UnitInventory inventory;
    public bool inventoryOpen=false;

    private void Start()
    {
        base.Start();
        SetStatusText();
        SetInventoryText();
        gameController = GameObject.Find("EventSystem").GetComponent<GameController>();
        movementRadius = GameObject.Find("MoveRadius");
        inventory = GetComponent<UnitInventory>();
        inventory.setUnit(unit);
    }

    private void SetStatusText()
    {
        statusText.text = "HP: " + unit.healthCur.ToString() + " / " + unit.healthMax.ToString();
    }

    private void SetStatusText(string append)
    {
        statusText.text = "HP: " + unit.healthCur.ToString() + " / " + unit.healthMax.ToString() + " - " + append;
    }

    private void SetInventoryText()
    {
        inventoryText.text = "Heals Remaining: " + healCount;
    }

    private void Update()
    {
        InventoryCheck();
    }
    private void FixedUpdate()
    {
        //Movement
        move();

        if(Input.GetKeyDown(KeyCode.E)){
            print("pressedE");
            GameObject[] temp = GameObject.FindGameObjectsWithTag("Item");
            foreach (GameObject obj in temp)
            {
                if ((obj.transform.position - rb.transform.position).magnitude < 2)
                {
                    PickupClickedItem(obj.transform.GetComponent<ItemScript>());
                    break;
                }
            }
        }


        //MouseListener
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f))
        {

            //If hovering on enemy, click to attack
            if (gameController.IsPlayerTurn())
            {
                //TODO - better way to set player active state on turn than HP > 0 probably. No game over screen yet when programming this, soooo.
                if (hit.collider.gameObject.tag == "Enemy" && Input.GetMouseButtonDown(0) && GetCurrentHP() > 0)
                {
                    EnemyController enemy = hit.collider.gameObject.GetComponent<EnemyController>();
                    enemy.TakeDamage(unit.damage);
                    gameController.UpdateEventLog("Player dealt " + unit.damage + " damage to " + enemy.name);
                    gameController.SetPlayerTurn(false);
                }
                else if (hit.collider.gameObject.tag == "Player" && Input.GetMouseButtonDown(0) && GetCurrentHP() > 0)
                {
                    //Click to Heal
                    //TODO - some kind of active player component, as could be more than one player unit, also the flexibility of using same script to attack player from enemy
                    Heal(healStrength);
                }
            }
        }
    }
    public void TakeDamage(int damageAmount)
    {
        unit.healthCur -= damageAmount;
        SetStatusText();
        if (unit.healthCur <= 0)
        {
            SetGameOver();
        }
    }
    public void SetGameOver()
    {
        //Game over screen
        //TODO, game over screen and actually end combat
        gameController.UpdateEventLog("You lost!");

    }
    public void Heal(int healAmount)
    {
        if (healCount > 0)
        {
            unit.healthCur += healAmount;
            if (unit.healthCur > unit.healthMax)
                unit.healthCur = unit.healthMax;
            SetStatusText("Healed " + healAmount);
            gameController.UpdateEventLog("Player healed " + healAmount);
            healCount--;
            SetInventoryText();
            gameController.SetPlayerTurn(false);
        }
        else
        {
            gameController.UpdateEventLog("No heals left!");
        }
    }
    public int GetMaxHP()
    {
        return unit.healthMax;
    }
    public int GetHealCount()
    {
        return healCount;
    }
    public int GetCurrentHP()
    {
        return unit.healthCur;
    }
    public float GetPercentageHP()
    {
        return (float)unit.healthCur / (float)unit.healthMax;
    }
    public float GetPercentageMoveLeft()
    {
        return (float)currentMovement / (float)unit.movespeed;
    }

    public void move()
    {
        if (canMove)
        {
            if (currentMovement > 0) {
                //Update movement to correct direction
                Vector3 movement = Quaternion.Euler(0, 45, 0) * new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                movement = movement * Time.deltaTime * unit.movespeed;
                float movelength = movement.magnitude;
                float newMovelength = Mathf.Clamp(movelength, 0, currentMovement);
                //check if need to limmit movement
                if (movelength != newMovelength)
                {
                    movement = newMovelength * movement.normalized;
                }
                //checks if current movement leads into a pit greater than maxFallDist;
                if(movement.magnitude > 0)
                {
                    if (!pitCheck(movement))
                    {
                        rb.MovePosition(rb.transform.position + movement);
                        currentMovement -= movement.magnitude;
                        updateMovementRadius();
                    }
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
        currentMovement = unit.movespeed;
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
        }
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

        return inventory.drop(rb.transform.position,item);
    }
    public void InventoryCheck()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.ChangeInventoryState();
        }
    }
    }
