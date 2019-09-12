using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bur_home : MonoBehaviour {

    private bool snapback;
	// Use this for initialization
	void Start () {
        snapback = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (snapback == true) {
            if (other.GetComponent<bur>())
            {
                other.transform.SetParent(other.GetComponent<bur>().init_parent.transform);
                other.transform.position = other.GetComponent<bur>().init_location;
                other.transform.rotation = other.GetComponent<bur>().init_rotation;
            }
            snapback = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<bur>())
        {
            snapback = true;
        }
    }
}
