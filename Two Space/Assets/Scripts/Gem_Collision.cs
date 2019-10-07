using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem_Collision : MonoBehaviour {

	public Sprite newSprite;

    void OnTriggerEnter2D(Collider2D otherObj)
    {
        Debug.Log("Object entered");
        GameObject collider = otherObj.gameObject;
        
        if(collider.tag == "Player")
        {
            Debug.Log("Knight entered");
            // Turn into Gem
            gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
        }
        if(collider.tag == "Beam" && gameObject.GetComponent<SpriteRenderer>().sprite == newSprite)
        {
            Debug.Log("Beam entered");
            // Get beamed up
            //BeamUp();
            
        }
    }
    void BeamUp()
    {
        var pos = gameObject.transform.position;
        while(pos.y < 0.0)
        {
            pos.y += 1;
            gameObject.transform.position = pos;
        }   
    }

}
