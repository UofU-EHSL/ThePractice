using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class resetables
{
    public GameObject Object;
    public GameObject init_parent;
    public Vector3 init_location;
    public Quaternion init_rotation;
}
public class reset : MonoBehaviour {

    public resetables[] resetables;
    // Use this for initialization
    void Start() {
        foreach (resetables reset in resetables)
        {
            reset.init_parent = reset.Object.transform.parent.gameObject;
            reset.init_location = reset.Object.transform.localPosition;
            reset.init_rotation = reset.Object.transform.localRotation;
        }
    }
}
