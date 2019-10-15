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
        if (collider.tag == "Player")
        {
            // Move up 
            Player pScript = collider.transform.parent.GetComponent<Player>();
            pScript.IsInsideTractorBeam = true;
        }
        else if(collider.tag == "Item")
        {
            collider.GetComponent<Gem_Collision>().beamUp = true;
        }
    }

    void OnTriggerExit2D(Collider2D otherObj)
    {
        GameObject collider = otherObj.gameObject;
        if (collider.tag == "Player")
        {
            // Move up 
            Player pScript = collider.transform.parent.GetComponent<Player>();
            pScript.IsInsideTractorBeam = false;
        }
    }

    public void DestroyCollider()
    {
        DestroyItem = true;
    }
}