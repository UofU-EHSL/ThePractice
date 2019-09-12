using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class denture_placement : MonoBehaviour {
    public GameObject[] implant_location;
    public GameObject denture;

    /// <summary>
    /// if you have 2 implants and abutments in then snap this in
    /// </summary>

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == denture)
        {
            if (implant_location[0].GetComponent<single_tooth>().has_abutment && implant_location[1].GetComponent<single_tooth>().has_abutment)
            {
                
            }
        }
    }
}
