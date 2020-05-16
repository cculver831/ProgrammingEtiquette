using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script displays bit masking to grant access to a dorr with specific requirements.
public class DoorManager : MonoBehaviour
{
    int doorType = AttributeManager.MAGIC;
    private void OnCollisionEnter(Collision collision)
    {
        if((collision.gameObject.GetComponent<AttributeManager>().attributes & doorType) != 0) //if the bits match, we have the attribute we're looking for
        {
            GetComponent<BoxCollider>().isTrigger = true;
            //Make door solid again
        }
        else
        {
            Debug.Log("You don't have access to this door yet");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        GetComponent<BoxCollider>().isTrigger = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
