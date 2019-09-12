using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Image_Loader : MonoBehaviour {

    public GameObject Dir_location;
    private bool Dir_loaded = false;
    public string pathPrefix = @"file://";
    public string pathImageAssets = @"C:\dicom\";
    public string pathSmall = @"small\";
    public string filename = @"IM-0001-";
    public string fileSuffix = @".jpg";
    public ArrayList imageBuffer;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
