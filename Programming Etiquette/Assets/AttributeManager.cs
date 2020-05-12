using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//This Class shows examples of usinf bitflags to represent player stats
public class AttributeManager : MonoBehaviour
{
    static public int MAGIC = 16;
    public Text attributeDisplay;
    static public int FLY = 8;
    static public int INTELLIGENCE = 4;
    public int attributes = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Magic")
        {
            attributes |= MAGIC;
        }
        else if (other.gameObject.tag == "Fly")
        {
            attributes |= FLY;
        }
       else if (other.gameObject.tag == "Intelligence")
        {
            attributes |= INTELLIGENCE;
        }
        //Note: To remove an attribute
        /*
         *      else if (other.gameObject.tag == "UNDO")
        {
            attirbutes &= ~INTELLIGENCE;
        }
        */


    }
    // Update is called once per frame
    void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);
        attributeDisplay.transform.position = screenPoint + new Vector3(0,-50,0); //keeps text in center of screen facing player
        attributeDisplay.text = Convert.ToString(attributes, 2).PadLeft(8, '0');
    }
       
}
