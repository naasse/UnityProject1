using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour {
    protected UnitInventory inv;

    public float movespeed=5;
    public int healthMax;
    public int healthCur;

    public float damage=1;
    public int attackSpeed;

    public int Strength=10;
    public int Dexterity=10;
    public int Constituion=10;
    public int Wisdom=10;
    public int Intelligence=10;
    public int Charisma=10;

    public float armor;
    public bool inCombat =false;


    // Use this for initialization
     void Start () {
        healthCur = healthMax;
        inv =gameObject.transform.GetComponent<UnitInventory>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void UpdateStats()
    {
        damage = inv.calculateDamage();
        armor = inv.calculateResistance()[0];
    }
}
