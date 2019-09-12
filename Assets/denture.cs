using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class denture_drill_locations
{
     
}
public class denture : MonoBehaviour {

    public GameObject hole;
    public GameObject full_denture;
    public GameObject MRI;
    private Transform init_parent;
    private Vector3 init_location;
    private Quaternion init_rotation;
    private Vector3 init_scale;
    private bool drilled = false;

    private void Start()
    {
        init_parent = full_denture.transform.parent;
        init_location = full_denture.transform.localPosition;
        init_rotation = full_denture.transform.localRotation;
        init_scale = full_denture.transform.localScale;
    }

    public void set_in(Transform parent, Vector3 location, Quaternion rotation)
    {
        if (drilled == true)
        {
            full_denture.GetComponent<Rigidbody>().useGravity = false;
            full_denture.GetComponent<Rigidbody>().isKinematic = true;
            full_denture.transform.SetParent(MRI.gameObject.transform);
            full_denture.transform.localPosition = location;
            full_denture.transform.localRotation = rotation;
            //full_denture.transform.localScale = new Vector3(scale,scale,scale);
            Debug.Log(full_denture.name + "was here");

        }
    }

    public void Reset_init()
    {
        full_denture.transform.SetParent(init_parent.transform);
        full_denture.transform.position = init_location;
        full_denture.transform.rotation = init_rotation;
        full_denture.transform.localScale = init_scale;
        drilled = false;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bur")
        {
            hole.GetComponent<MeshRenderer>().enabled = false;
            drilled = true;
        }
    }


    
    public void Generate()
    {
        hole.GetComponent<MeshRenderer>().enabled = true;
        full_denture.transform.SetParent(init_parent);
        full_denture.transform.localPosition = init_location;
        full_denture.transform.localRotation = init_rotation;
        drilled = false;
    }
}
