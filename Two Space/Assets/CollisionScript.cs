using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour {

	public GameObject obj;
	public int numAsteroids;
	private float counter = 5.0f;


    void OnTriggerEnter2D(Collider2D otherObj)
    {
		if(otherObj.gameObject.tag == "Player" || otherObj.gameObject.tag == "Ship")
        {
            
			Vector3 colliderPos = gameObject.transform.position;
			Vector3 camHeight = Camera.current.transform.position;
			Debug.Log("insideeeee" + camHeight);
	
			// Instantiate asteroids
			for (int i = 0; i < numAsteroids; i++)
			{
				float x = colliderPos.x + Random.Range(30.0f, 120.0f);
				float y = camHeight.y + Random.Range(40.0f, 100.0f);
				Vector3 spawnPos = new Vector3(x, y, 0.0f);
				Instantiate(obj, spawnPos, Quaternion.identity);
			}
        }  
    }
}
