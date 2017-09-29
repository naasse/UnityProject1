using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public float speed;
    public int attackStrength;
    public int healStrength;
    public int healCount;
    private Rigidbody rb;
    private GameController gameController;

    public int maxHP;
    private int currentHP;
    public Text statusText;
    public Text inventoryText;

    private bool canMove=false;
    public float maxMovement;
    private float currentMovement;
    private GameObject movementRadius;
    public float maxClimbHeight;
    public float climbHeightResolution;
    public float maxFallDist;

    private PlayerInventory inventory;
    public bool inventoryOpen=false;

    private void Start()
    {
        currentHP = maxHP;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        SetStatusText();
        regenMovement();
        SetInventoryText();
        gameController = GameObject.Find("EventSystem").GetComponent<GameController>();
        movementRadius = GameObject.Find("MoveRadius");
        inventory = transform.GetComponent<PlayerInventory>();
    }

    private void SetStatusText()
    {
        statusText.text = "HP: " + currentHP.ToString() + " / " + maxHP.ToString();
    }

    private void SetStatusText(string append)
    {
        statusText.text = "HP: " + currentHP.ToString() + " / " + maxHP.ToString() + " - " + append;
    }

    private void SetInventoryText()
    {
        inventoryText.text = "Heals Remaining: " + healCount;
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
                    enemy.TakeDamage(attackStrength);
                    gameController.UpdateEventLog("Player dealt " + attackStrength + " damage to " + enemy.name);
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
        currentHP -= damageAmount;
        SetStatusText();
        if (currentHP <= 0)
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
            currentHP += healAmount;
            if (currentHP > maxHP)
                currentHP = maxHP;
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
        return maxHP;
    }
    public int GetHealCount()
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

        return inventory.drop(rb.transform.position,item);
    }
    }
