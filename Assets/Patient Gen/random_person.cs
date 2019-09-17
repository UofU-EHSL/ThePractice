using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class tooth{
    public GameObject this_tooth;
    public GameObject gum;
    public Vector3 init_location;
    public Vector3 location;
    public Vector3 location_limit = new Vector3(1,1,1);
    public Quaternion rotation;
    public Vector3 rotation_limit;
}

public class random_person : MonoBehaviour {
    public bool Random_location;
    public bool Random_rotation;
    public GameObject mandibal;
    public GameObject maxilary;
    public Vector3 mandibular_translation;
    public float mandibular_translation_max;
    public float mandibular_translation_min;
    private float init_man_y;
    public tooth[] teeth;
	// Use this for initialization
	void Start () {
        foreach (tooth tooth in teeth)
        {
            tooth.init_location = tooth.this_tooth.transform.localPosition;
        }
        init_man_y = mandibular_translation.y;
        Generate();
    }
	
	// Update is called once per frame
	public void Generate () {
        mandibular_translation = mandibal.transform.localPosition;
        mandibular_translation.y = init_man_y + Random.Range(mandibular_translation_min, mandibular_translation_max);
        mandibal.transform.localPosition = mandibular_translation;
        foreach (tooth tooth in teeth)
        {
            tooth.location = new Vector3(Random.Range(-tooth.location_limit.x / 2, tooth.location_limit.x / 2), Random.Range(-tooth.location_limit.y / 2, tooth.location_limit.y / 2), Random.Range(-tooth.location_limit.z / 2, tooth.location_limit.z / 2));
            tooth.this_tooth.transform.localPosition = tooth.location+tooth.init_location;
        }
    }
}
