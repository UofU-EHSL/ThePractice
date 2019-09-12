using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turn_on_all_the_lights : MonoBehaviour {

    public GameObject[] lights;
    public bool onOff;
    public void light()
    {
        foreach (GameObject light in lights)
        {
            light.gameObject.SetActive(onOff);
        }
    }
}
