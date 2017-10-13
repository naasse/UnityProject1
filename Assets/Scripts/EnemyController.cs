using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : UnitController {

    private int maxHP;
    private int currentHP;
    private int attackStrength;
    private GameController gameController;
    private PlayerController player;
    // Use this for initialization
    void Start ()
    {
        base.Start();
        maxHP = 10;
        currentHP = 10;
        attackStrength = 1;
        gameController = GameObject.Find("EventSystem").GetComponent<GameController>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    public void TakeDamage(int damageAmount)
    {
        currentHP -= damageAmount;
        if (currentHP <= 0)
        {
            gameObject.SetActive(false);
            //gameController.GetRemainingEnemies();
        }
    }

    public void Heal(int healAmount)
    {
        currentHP += healAmount;
        if (currentHP > maxHP)
            currentHP = maxHP;
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
        return (float)currentHP/(float)maxHP;
    }
    public int GetAttackStrength()
    {
        return attackStrength;
    }

    public void TakeTurn()
    {
        rb.isKinematic = false;
        player.TakeDamage(attackStrength);
        gameController.UpdateEventLog(player.name + " took " + attackStrength + " damage from " + name);
        rb.isKinematic = true;
    }
}
