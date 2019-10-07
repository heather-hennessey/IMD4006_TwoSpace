using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollision : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D otherObj)
    {
        Debug.Log("Object Collided");
        GameObject collider = otherObj.gameObject;
        SpaceCraft scScript = gameObject.transform.parent.GetComponent<SpaceCraft>();

        if(collider.tag == "Grass")
        {
            Debug.Log("Hit Ground");
            // Ded
            
            scScript.alive = false;

        }
        if(collider.tag == "Asteroid")
        {
            Debug.Log("Asteroid hit");
            // die
            scScript.alive = false;
        }
    }
}
