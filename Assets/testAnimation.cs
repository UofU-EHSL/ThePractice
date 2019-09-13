using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class flat_down : MonoBehaviour {

    public int AnimationLayer;
    public float AnimationTime;
    public string AnimationName;
    public bool test = true;

    public void Update()
    {
        if (test == true)
        {
            animate();
            test = false;
        }
    }

    public void animate()
    {
        GetComponent<Animator>().Play(AnimationName, AnimationLayer, AnimationTime);
    }
}
