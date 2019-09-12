using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawer : MonoBehaviour {
    private GameObject triggerItema;
    public GameObject DrawerGameobject;
    public Transform ActiveTools;
    public GameObject tool;

    public void OnTriggerEnter(Collider other)
    {
        tool = other.gameObject;
        other.gameObject.GetComponent<tool>().DrawerGameobject = ActiveTools.gameObject;

    }

    public void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<tool>().DrawerGameobject = other.gameObject.GetComponent<tool>().initGameobject;
    }
}