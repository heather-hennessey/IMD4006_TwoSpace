using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {
    public GameObject astronaut;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Ground")
        {
            astronaut.GetComponent<Player>()._canJump = true;
            astronaut.GetComponent<Player>()._canWalk = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.tag == "Ground")
        {
            astronaut.GetComponent<Player>()._canJump = false;
            astronaut.GetComponent<Player>()._canWalk = false;
        }
    }
}
