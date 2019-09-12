using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DICOMbuilder : MonoBehaviour {
    public GameObject StartingLayer;
    public GameObject[] images;
    public float LayerHeight;
    
    private int layer;
    private GameObject this_layer;
	// Use this for initialization
	void Start () {
        layer = 0;
        foreach (GameObject image in images)
        {
            this_layer = Instantiate(StartingLayer);
            this_layer.transform.position = new Vector3(0, layer * LayerHeight, 0);
            layer++;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
