using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tool : MonoBehaviour {
    public GameObject DrawerGameobject;
    public GameObject initGameobject;

    private void Start()
    {
        initGameobject = gameObject.transform.parent.gameObject;
        DrawerGameobject = initGameobject;
    }
    public void release()
    {
        Debug.Log("release of tool");
        gameObject.transform.SetParent(DrawerGameobject.transform);
    }
}
