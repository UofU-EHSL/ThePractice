using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OP_controller : MonoBehaviour {
    public GameObject small;
    public GameObject large;

    public void Start()
    {
        large.SetActive(false);
    }
}
