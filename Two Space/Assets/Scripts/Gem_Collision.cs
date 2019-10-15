using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem_Collision : MonoBehaviour {

	public Sprite newSprite;
    private bool keepGoing;
    public bool beamUp;

    void Start(){
        beamUp = false;
    }

    void OnTriggerEnter2D(Collider2D otherObj)
    {
        GameObject collider = otherObj.gameObject;
        
        if(collider.tag == "Player")
        {
            // Turn into Gem
            gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
        }  
    }
    
    void FixedUpdate(){
        if(beamUp == true && gameObject.GetComponent<SpriteRenderer>().sprite == newSprite)
        {
            SpaceCraft sc = FindObjectOfType<SpaceCraft>();
            float x = 0f;
            if(gameObject.transform.position.x < sc.transform.position.x)
            {
                x = 0.1f;
            }
            else if(gameObject.transform.position.x > sc.transform.position.x)
            {
                x = -0.1f;
            }

            gameObject.transform.Translate(x, 0.1f, 0f, Space.World);

            if(gameObject.transform.position.y > sc.transform.position.y)
            {
                FindObjectOfType<ScoreController>().incrementScore();
                FindObjectOfType<beam_collision>().DestroyCollider();
                Destroy(gameObject);
            }
        }
    }
}
