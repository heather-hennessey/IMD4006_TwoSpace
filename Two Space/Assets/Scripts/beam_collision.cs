using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beam_collision : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D otherObj)
    {
        GameObject collider = otherObj.gameObject;
        //Debug.Log("Hit Object" + collider.tag);
        if (collider.tag == "Player")
        {
           // Debug.Log("Player Enter");
            // Move up 
            Player pScript = collider.transform.parent.GetComponent<Player>();
            pScript.IsInsideTractorBeam = true;
        }
        else if(collider.tag == "Item")
        {
            FindObjectOfType<ScoreController>().incrementScore();
            Destroy(collider);
        }
    }

    void OnTriggerExit2D(Collider2D otherObj)
    {
        GameObject collider = otherObj.gameObject;
        if (collider.tag == "Player")
        {
            //Debug.Log("Player Exit");
            // Move up 
            Player pScript = collider.transform.parent.GetComponent<Player>();
            pScript.IsInsideTractorBeam = false;
        }
    }
}