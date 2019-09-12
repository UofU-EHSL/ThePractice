using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hand_piece : MonoBehaviour {

    public Vector3 bit_location;
    public Quaternion bit_rotation;
    public GameObject hit_child;
    private Vector3 location;
    private Quaternion rotation;

    public void Start()
    {
        location = gameObject.transform.localPosition;
        rotation = gameObject.transform.localRotation;
    }
    public void reset()
    {
        gameObject.transform.localRotation = rotation;
        gameObject.transform.localPosition = location;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bur")
        {
            other.transform.SetParent(gameObject.transform);
            other.transform.localPosition = bit_location;
            other.transform.localRotation = bit_rotation;
        }
        if (other.tag == "not_sterile")
        {
            reset();
        }
    }
}
