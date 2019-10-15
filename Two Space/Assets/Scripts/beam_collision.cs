using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beam_collision : MonoBehaviour
{
    public bool DestroyItem = false;

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
            FindObjectOfType<Gem_Collision>().BeamUp();

            if(DestroyItem == true)
            {
                Destroy(collider);
                DestroyItem = false;
            }
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

    public void DestroyCollider()
    {
        DestroyItem = true;
    }

    public void HideBeam()
    {
        Debug.Log("WORKING");
        gameObject.GetComponent<Renderer>().enabled = false;
    }
}