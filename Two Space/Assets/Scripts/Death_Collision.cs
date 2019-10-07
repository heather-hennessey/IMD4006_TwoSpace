using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Collision : MonoBehaviour {

    
	void OnTriggerEnter2D(Collider2D otherObj)
    {
        Debug.Log("Object entered");
        GameObject collider = otherObj.gameObject;
        
        if(collider.tag == "Player")
        {
            Debug.Log("Knight entered");
            // Ded
            Player playerScript = collider.transform.parent.GetComponent<Player>();
            playerScript.alive = false;

        }
    }

}

