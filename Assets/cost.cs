using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cost : MonoBehaviour {
    public GameObject[] items;
    public float total_cost;
	// Use this for initialization
	void calculate()
    {
        foreach (GameObject item in items)
        {
           total_cost = item.GetComponent<tool_properties>().Real_cost;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
