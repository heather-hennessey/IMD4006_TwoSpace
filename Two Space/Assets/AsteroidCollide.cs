using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollide : MonoBehaviour {

    public GameObject Rock;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void onTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("YUS");
        Instantiate(Rock, new Vector3(75, -15, 0), Quaternion.identity);
        Instantiate(Rock, new Vector3(87, -10, 0), Quaternion.identity);
        Instantiate(Rock, new Vector3(90, -23, 0), Quaternion.identity);
    }
}
