using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class replica : MonoBehaviour {
    private GameObject start_parent;
    private Vector3 start_location;
    private Quaternion start_rotation;
    private Vector3 start_scale;

    private void Start()
    {
        start_parent = gameObject.transform.parent.gameObject;
        start_location = gameObject.transform.localPosition;
        start_rotation = gameObject.transform.localRotation;
        start_scale = gameObject.transform.localScale;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "abutment")
        {
            gameObject.transform.SetParent(start_parent.transform);
            gameObject.transform.localPosition = start_location;
            gameObject.transform.localRotation = start_rotation;
            gameObject.transform.localScale = start_scale;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "abutment")
        {
            gameObject.transform.SetParent(other.transform);
            gameObject.transform.localScale = new Vector3(1,1,1);
            gameObject.transform.localPosition = new Vector3(0,0,0);
            gameObject.transform.localRotation = new Quaternion(0,0,0,0);
        }
    }
}
