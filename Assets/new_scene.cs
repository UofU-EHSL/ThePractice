using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class new_scene : MonoBehaviour {
    private GameObject portal;
    public Vector3 location;
    public GameObject next_portal;
    public GameObject player;

	void Start () {
        portal = this.gameObject;
	}
	
    void OnTriggerEnter(Collider other)
    {
        player.transform.position = location;
        portal.SetActive(false);
        next_portal.SetActive(true);
    }
}