using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class slider_value_text : MonoBehaviour {

    public GameObject slider;
	
	// Update is called once per frame
	void Update () {
        this.gameObject.GetComponent<Text>().text = slider.GetComponent<Slider>().value.ToString();
	}
}
