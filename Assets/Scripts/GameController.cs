using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public Image enemyHealth;
    public Text turnText;
    public Text eventText;

    private bool playerTurn;
    private float timeWait = 1.0f;

    private int roundNumber;


    private List<string> eventLog = new List<string>();

    PlayerController player;

    private void Start()
    {
        roundNumber = 1;
        turnText.text = "Turn: Player";
        eventText.text = "";
        UpdateEventLog("Combat starting!");
        UpdateEventLog("Round: " + roundNumber);
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        SetPlayerTurn(true);
    }

    public bool IsPlayerTurn()
    {
        return playerTurn;
    }
    public void SetPlayerTurn(bool newPlayerTurn)
    {
        playerTurn = newPlayerTurn;
        if (playerTurn)
        {
            turnText.text = "Players Turn";
            roundNumber++;
            UpdateEventLog("Round: " + roundNumber);
            player.startTurn();
        }
        else
        {
            player.endTurn();
            turnText.text = "Enemies Turn";

        }
            
 
        UpdateEventLog(turnText.text);
    }

    private void DoEnemyTurn()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemyObject in enemyList)
        {
            EnemyController enemy = enemyObject.GetComponent<EnemyController>();
            player.TakeDamage(enemy.GetAttackStrength());            
            //TODO - these should be handled in the respective element controller, I think
            UpdateEventLog("Player took 1 damage from " + enemy.name);
        }

        SetPlayerTurn(true);
    }

    private void FixedUpdate()
    {
        if (timeWait <= 0)
            timeWait = 1.0f;
        if (!playerTurn)
        {
            timeWait -= Time.deltaTime;
            if (timeWait < 0)
            {
                DoEnemyTurn();
            }
        }
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

    //TODO - scrollable, or only display subset
    //Maybe have all available somewhere (or at least the last x)
    public void UpdateEventLog(string message)
    {
        eventLog.Add(message);
        eventText.text += "\n" + System.DateTime.Now + ": " + message;

    }
}