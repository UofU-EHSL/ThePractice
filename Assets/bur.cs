using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bur : MonoBehaviour {

    public float size;
    public Vector3 init_location;
    public Quaternion init_rotation;
    public GameObject init_parent;

    [Header("Handpiece settings")]
    public Vector3 hand_piece_location;
    public Quaternion hand_piece_rotation;

    // Use this for initialization
    void Start () {
        init_parent = transform.parent.gameObject;
        init_location = transform.position;
        init_rotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        //////////////debug
        if (gameObject.transform.parent.gameObject != init_parent)
        {
            gameObject.transform.localPosition = hand_piece_location;
            gameObject.transform.localRotation = hand_piece_rotation;
        }
	}
}