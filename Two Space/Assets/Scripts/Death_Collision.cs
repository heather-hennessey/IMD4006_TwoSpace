using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Collision : MonoBehaviour {

    
	void OnTriggerEnter2D(Collider2D otherObj)
    {
        
        GameObject collider = otherObj.gameObject;
        
        if(collider.tag == "Player")
        {
            
            // Ded
            Player pScript = collider.transform.parent.GetComponent<Player>();
            pScript.alive = false;

        }
    }

}

