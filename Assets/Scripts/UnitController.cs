using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour {
    protected UnitScript unit;
    protected Rigidbody rb;
    protected bool canMove = false;
    // Use this for initialization
    protected void Start () {
        unit = GetComponent<UnitScript>();
        print(unit);
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }
	
	// Update is called once per frame
	protected void Update () {
		
	}
}
