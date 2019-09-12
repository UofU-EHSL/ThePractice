using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[System.Serializable]
public class snapping
{
    public GameObject tool;
    public Vector3 location;
    public Vector3 rotation;
    public GameObject return_parent;
}

public class tool_properties : MonoBehaviour {
    public GameObject Cost_calculator;
    public bool Was_used = false;
    public float Cost;
    public string Manufacturer;
    public string Name;
    public bool Reusable;
    public int Reuse_Cycles;
    public int Cleaning_Cycles;
    public float Real_cost;
    public bool Disposible;
    public bool Autoclava_Required;
    public bool AutoClave_Optional;
    public bool Sharps;
    public bool Bio_Hazard;
    public bool Refrigerate;
    public bool Sterile;
    public float Capacity;
    public bool Controlled_substance;
    public float Radioactivity;
    public bool Broken_sterile_field;
    public GameObject[] NonCompatibleObjects;
    public snapping[] snapping;

    // Use this for initialization
    void Start () {
        Was_used = false;
        Real_cost = Cost / Reuse_Cycles;
        foreach (snapping tool in snapping)
        {
            tool.return_parent = tool.tool.transform.parent.gameObject;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        foreach (GameObject NonCompatibleObject in NonCompatibleObjects)
        {
            if (collision.gameObject.name == NonCompatibleObject.gameObject.name)
            {
                Debug.Log("not compatible");
            }
        }
       // if (collision.gameObject.GetComponent<tool_properties>().Sterile == false)
       // {
       //     Broken_sterile_field = true;
       // }
        
    }
    public void OnTriggerEnter(Collider other)
    {
        foreach (snapping tool in snapping)
        {
            if (other.gameObject == tool.tool.gameObject)
            {
                tool.tool.transform.SetParent(gameObject.transform);
                tool.tool.transform.localPosition = tool.location;
                tool.tool.transform.localRotation = Quaternion.Euler(tool.rotation);
                tool.tool.GetComponent<Rigidbody>().isKinematic = true;
                tool.tool.GetComponent<Rigidbody>().useGravity = false;
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        foreach (snapping tool in snapping)
        {
            if (other.gameObject == tool.tool.gameObject)
            {
                tool.tool.transform.SetParent(gameObject.transform);
                tool.tool.GetComponent<Rigidbody>().isKinematic = false;
                tool.tool.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
    public void On_pickup()
    {
        Debug.Log("triggered pickup");
        Cost_calculator.gameObject.GetComponent<Rolling_cost_list>().Add_to_list(Name,Real_cost);
    }
}
