using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drop_reset : MonoBehaviour {
    private GameObject init_parent;
    private Vector3 init_location;
    private Quaternion init_rotation;
    public GameObject ground;
	// Use this for initialization
	void Start () {
        init_parent = gameObject.transform.parent.gameObject;
        init_location = gameObject.transform.localPosition;
        init_rotation = gameObject.transform.localRotation;
	}
    public void reset()
    {
        gameObject.transform.SetParent(init_parent.transform);
        gameObject.transform.localPosition = init_location;
        gameObject.transform.localRotation = init_rotation;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == ground)
        {
            reset();
        }
    }
}
