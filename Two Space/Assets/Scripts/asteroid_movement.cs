using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid_movement : MonoBehaviour {
    public float MoveSpeed;
    
    private Vector2 _inputAxis;
    private Rigidbody2D rig;

	// Use this for initialization
	void Start () {
		rig = gameObject.GetComponent<Rigidbody2D>();		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        var pos = gameObject.transform.position;
        pos.x -= MoveSpeed * Time.deltaTime;
		gameObject.transform.position = pos;
           
	}
    
    void OnCollisionEnter2D(Collision2D otherObj)
    {
        GameObject collider = otherObj.gameObject;
        //Debug.Log("Hit Object" + collider.tag);

        if(collider.tag == "Player")
        {
            // die
            FindObjectOfType<Player>().alive = false;
            //Player pScript = collider.transform.parent.GetComponent<Player>();
            //print(pScript);
            //pScript.alive = false;
        }
        else if (collider.tag == "Ship"){
            SpaceCraft sScript = collider.transform.parent.GetComponent<SpaceCraft>();
            sScript.alive = false;
        }
        else{
            Destroy(gameObject);
        }
    }
}
