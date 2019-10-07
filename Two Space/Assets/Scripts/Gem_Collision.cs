using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem_Collision : MonoBehaviour {

	public Sprite newSprite;

    void OnTriggerEnter2D(Collider2D otherObj)
    {
        
        GameObject collider = otherObj.gameObject;
        
        if(collider.tag == "Player")
        {
            
            // Turn into Gem
            gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
        }
       
    }
    public void BeamUp()
    {
        if (gameObject.GetComponent<SpriteRenderer>().sprite == newSprite)
        {
            Debug.Log("Beam entered");
            var pos = gameObject.transform.position;
            pos.y += 10.0f;
            gameObject.transform.position = pos;
        }
    }

}
