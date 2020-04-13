using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class AttributeManager : MonoBehaviour
{
    static public int MAGIC = 16;
    public Text attributeDisplay;

    int attirbutes = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Magic")
        attirbutes |= MAGIC;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);
        attributeDisplay.transform.position = screenPoint + new Vector3(0,-50,0); //keeps text in center of screen facing player
        attributeDisplay.text = Convert.ToString(attirbutes, 2).PadLeft(8, '0');
    }
       
}
