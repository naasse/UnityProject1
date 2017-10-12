using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour {
    protected UnitScript unit;
    protected Rigidbody rb;
    protected bool canMove = true;
    protected bool combatPhase = false;
    public bool isTurn = false;
    // Use this for initialization
    protected void Start () {
        unit = GetComponent<UnitScript>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;


    }
	
	// Update is called once per frame
	protected void Update () {
		
	}

    public void setCombat(bool combat)
    {
        combatPhase = combat;
    }


}
