using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class random_person : MonoBehaviour {
    public int number_of_missing_teeth_mandible;
    private int number_of_missing_man;
    public Slider slider_mandible;
    public int number_of_missing_teeth_maxilary;
    private int number_of_missing_max;
    public Slider slider_maxilary;
    public bool Random_location;
    public bool Random_rotation;
    public bool Random_teeth;
    public GameObject mandibal;
    public GameObject maxilary;
    public Vector3 mandibular_translation;
    public float mandibular_translation_max;
    public float mandibular_translation_min;
    public float init_man_y;
    public GameObject[] teeth;

	// Use this for initialization
	void Start () {
        init_man_y = mandibal.transform.localPosition.y;
        Generate();
        foreach (GameObject tooth in teeth)
        {
            //tooth.GetComponent<single_tooth>().init_location = tooth.GetComponent<single_tooth>().this_tooth.transform.localPosition;
        }

    }
    public void slider()
    {
        number_of_missing_teeth_mandible = Mathf.RoundToInt(slider_mandible.value);
    }
    public void impress()
    {
        foreach (GameObject tooth in teeth)
        {
            tooth.GetComponent<single_tooth>().impress();
        }
    }
    public void Generate () {
        
        foreach (GameObject tooth in teeth)
        {
            tooth.GetComponent<recorded_data>().Generate();
            if (Random_teeth == true) {
                tooth.GetComponent<single_tooth>().missing_tooth = false;
            }
            tooth.GetComponent<single_tooth>().tooth_info = new Vector3(Random.Range(tooth.GetComponent<single_tooth>().min_missing_tooth_size.x, tooth.GetComponent<single_tooth>().max_missing_tooth_size.x), Random.Range(tooth.GetComponent<single_tooth>().min_missing_tooth_size.y, tooth.GetComponent<single_tooth>().max_missing_tooth_size.y), Random.Range(tooth.GetComponent<single_tooth>().min_missing_tooth_size.z, tooth.GetComponent<single_tooth>().max_missing_tooth_size.z));
        }
        if (Random_teeth == true)
        {
            number_of_missing_man = number_of_missing_teeth_mandible;

            while (number_of_missing_man > 0)
            {
                int random = Random.Range(0, 14);
                if (teeth[random].GetComponent<single_tooth>().missing_tooth == false)
                {
                    teeth[random].GetComponent<single_tooth>().missing_tooth = true;
                    number_of_missing_man--;
                }
            }
        }
        mandibular_translation = mandibal.transform.localPosition;
        mandibular_translation.y = init_man_y + Random.Range(mandibular_translation_min, mandibular_translation_max);
        mandibal.transform.localPosition = mandibular_translation;
        foreach (GameObject tooth in teeth)
        {
            //missing tooth size generation
            //tooth.GetComponent<single_tooth>().tooth_info = new Vector3(Random.Range(tooth.GetComponent<single_tooth>().min_missing_tooth_size.x, tooth.GetComponent<single_tooth>().max_missing_tooth_size.x), Random.Range(tooth.GetComponent<single_tooth>().min_missing_tooth_size.y, tooth.GetComponent<single_tooth>().max_missing_tooth_size.y), Random.Range(tooth.GetComponent<single_tooth>().min_missing_tooth_size.z, tooth.GetComponent<single_tooth>().max_missing_tooth_size.z));
            //this is everything setup for missing teeth and how the impression material messes with it all
            tooth.GetComponent<single_tooth>().reset();
            if (tooth.GetComponent<single_tooth>().missing_tooth == true)
            {
                //tooth.GetComponent<single_tooth>().this_tooth.GetComponent<MeshRenderer>().enabled = false;
                tooth.GetComponent<single_tooth>().gum.GetComponent<MeshRenderer>().enabled = true;
                tooth.GetComponent<single_tooth>().cut_gum.GetComponent<MeshRenderer>().enabled = false;
                tooth.GetComponent<single_tooth>().jaw.GetComponent<MeshRenderer>().enabled = true;
                tooth.GetComponent<single_tooth>().small_hole.GetComponent<MeshRenderer>().enabled = false;
                tooth.GetComponent<single_tooth>().large_hole.GetComponent<MeshRenderer>().enabled = false;
                tooth.GetComponent<single_tooth>().Boxcollider.enabled = true;
                //tooth.GetComponent<single_tooth>().tooth_size_info.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                //tooth.GetComponent<single_tooth>().this_tooth.GetComponent<MeshRenderer>().enabled = true;
                tooth.GetComponent<single_tooth>().gum.GetComponent<MeshRenderer>().enabled = true;
                tooth.GetComponent<single_tooth>().cut_gum.GetComponent<MeshRenderer>().enabled = false;
                tooth.GetComponent<single_tooth>().jaw.GetComponent<MeshRenderer>().enabled = true;
                tooth.GetComponent<single_tooth>().small_hole.GetComponent<MeshRenderer>().enabled = false;
                tooth.GetComponent<single_tooth>().large_hole.GetComponent<MeshRenderer>().enabled = false;
                tooth.GetComponent<single_tooth>().Boxcollider.enabled = false;
                //tooth.GetComponent<single_tooth>().tooth_size_info.GetComponent<MeshRenderer>().enabled = false;
            }
            //END this is everything setup for missing teeth and how the impression material messes with it all

            //this makes all of the random locations and rotations
            //tooth.GetComponent<single_tooth>().location = new Vector3(Random.Range(-tooth.GetComponent<single_tooth>().location_limit.x / 2, tooth.GetComponent<single_tooth>().location_limit.x / 2), Random.Range(-tooth.GetComponent<single_tooth>().location_limit.y / 2, tooth.GetComponent<single_tooth>().location_limit.y / 2), Random.Range(-tooth.GetComponent<single_tooth>().location_limit.z / 2, tooth.GetComponent<single_tooth>().location_limit.z / 2));
            //tooth.GetComponent<single_tooth>().this_tooth.transform.localPosition = tooth.GetComponent<single_tooth>().location+tooth.GetComponent<single_tooth>().init_location;
        }
        if (number_of_missing_man == 14)
        {
            foreach(GameObject tooth in teeth){
                tooth.GetComponent<BoxCollider>().enabled = false;
            }
            teeth[4].GetComponent<BoxCollider>().enabled = true;
            teeth[9].GetComponent<BoxCollider>().enabled = true;
        }
    }
}
