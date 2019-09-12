using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class perio_probe : MonoBehaviour {

    public Vector3 dimentions;

    public GameObject canvas;
    public Text x;
    public Text y;
    public Text z;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<single_tooth>() && other.gameObject.GetComponent<single_tooth>().missing_tooth == true)
        {
            dimentions = other.gameObject.GetComponent<single_tooth>().tooth_info;
            canvas.SetActive(true);
            x.text = dimentions.x.ToString();
            y.text = dimentions.y.ToString();
            z.text = dimentions.z.ToString();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<single_tooth>() && other.gameObject.GetComponent<single_tooth>().missing_tooth == true)
        {
            canvas.SetActive(false);
            x.text = dimentions.x.ToString();
            y.text = dimentions.y.ToString();
            z.text = dimentions.z.ToString();
        }
    }
}
