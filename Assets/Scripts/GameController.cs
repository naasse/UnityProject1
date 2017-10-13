using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public Image enemyHealth;
    public Text turnText;
    public Text eventText;
    public Text enemyCountText;

    private int enemiesLeft=-1;

    private bool playerTurn;
    private float timeWait = 1.0f;
    private int roundNumber;
    private GameObject[] enemyList;

    public bool combatStarted = false;
    private List<string> eventLog = new List<string>();

    PlayerController player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void startCombat()
    {
        combatStarted = true;
        roundNumber = 0;
        UpdateEventLog("Combat starting!");
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        SetPlayerTurn(true);
        player.setCombat(true);
        GetRemainingEnemies();
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

        foreach (GameObject enemyObject in enemyList)
        {
            //If player dies during enemy turns, should skip the rest of enemies - would change when more than 1 player
            if (player.GetCurrentHP() <= 0)
            {
                break;
            }
            enemyObject.GetComponent<EnemyController>().TakeTurn();
        }

        SetPlayerTurn(true);
    }

    private void FixedUpdate()
    {

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
        if (combatStarted) { 
        if (timeWait <= 0)
            timeWait = 1.0f;
        if (!playerTurn)
        {

            timeWait -= Time.deltaTime;
            if (timeWait < 0 && enemiesLeft>0)
            {
                DoEnemyTurn();
            }
        }
        else GetRemainingEnemies();
    }
    }

    //TODO - scrollable, or only display subset
    //inefficient, rebuilding last 10 every time? Quick fix to keep from flowing off the page. If stick with this, definitely want to pop early records off list as this grows - 
    //although destroying after combat would probably be sufficient. Cleanest would be when adding 10th record, pop 0 index off queue.
    //Maybe have all available somewhere (or at least the last x)
    public void UpdateEventLog(string message)
    {
        string msgAndDate = "\n" + System.DateTime.Now + ": " + message;
        eventLog.Add(msgAndDate);

        eventText.text = "";
        int msgNum = 0;
        foreach (string msgText in eventLog)
        {
            //Only displaying last 10 messages
            if (msgNum >= eventLog.Count - 10)
            {
                eventText.text += msgText;
            }
            msgNum++;
        }
    }

    public void GetRemainingEnemies()
    {
        //TODO - don't refetch, pop them from list when they die and rely on gamecontroller Start() original list
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesLeft = enemyList.Length;
        enemyCountText.text = "Enemies Remaining: " + enemiesLeft;
        if (enemiesLeft <= 0)
        {
            UpdateEventLog("You won!");
            player.setCombat(false);
            combatStarted=false;
        }
    }
}