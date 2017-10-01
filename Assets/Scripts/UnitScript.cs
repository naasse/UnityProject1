using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour {
    protected UnitInventory inv;

    public float movespeed;
    public int healthMax;
    public int healthCur;

    public int damage;
    public int attackSpeed;

    public int Strength=10;
    public int Dexterity=10;
    public int Constituion=10;
    public int Wisdom=10;
    public int Intelligence=10;
    public int Charisma=10;

    public int Armour;


    // Use this for initialization
     void Start () {
        healthCur = healthMax;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void UpdateStats()
    {

    }
}
