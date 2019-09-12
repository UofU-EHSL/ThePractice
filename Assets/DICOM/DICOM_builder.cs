using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

namespace Valve.VR.InteractionSystem
{
    public class DICOM_builder : MonoBehaviour
    {
        // public GameObject Dir_location;
        private int starting_frames = 3;
        private bool Dir_loaded = false;
        public string pathPrefix = @"file://";
        public string pathImageAssets = @"C:\dicom\";
        public string pathSmall = @"small\";
        public string filename = @"IM-0001-";
        public string fileSuffix = @".jpg";
        public ArrayList imageBuffer;
        public GameObject Proxy;
        public Vector3 proxy_translate_offset;
        public Quaternion proxy_rotation_offset;
        public GameObject StartingLayer;
        public Texture2D[] images;
        private GameObject[] layers;

        public float SkipLayersSlider;
        public float CutOffTopSlider;
        public float alpha;
        public float contrast;

        public int skip_layers;
        public int CutOffTop;
        public int CutOffBottom;
        public float LayerHeight;
        public string location;
        public GameObject parent;
        private int layer;
        private GameObject this_layer;
        private GameObject[] refresh;
        public float slice_multiplier;
        public float contrast_float;
        public float alpha_float;
        

        // Use this for initialization

        public void Start()
        {
            images = Resources.LoadAll(location, typeof(Texture2D)).Cast<Texture2D>().ToArray();
            refresh = new GameObject[images.Length];
            BuildDICOM();
        }





        public void BuildDICOM()
        {
            Debug.Log("BuildDICOM");
            contrast_float = contrast;
            alpha_float = alpha;

            StartingLayer.GetComponent<Renderer>().material.SetFloat("_lighten", contrast_float);
            StartingLayer.GetComponent<Renderer>().material.SetFloat("_cutoff", alpha_float);
            layer = 0;
            CutOffBottom = images.Length;
            CutOffBottom = 0;

                foreach (GameObject layer in refresh)
                {
                    Destroy(layer);
                }

            StartingLayer.SetActive(true);
            foreach (Texture image in images)
            {
                if ((layer % skip_layers) == 0)
                {
                    this_layer = Instantiate(StartingLayer);
                    refresh[layer] = this_layer;
                    this_layer.GetComponent<Renderer>().material.SetTexture("_MainTex", image);
                    this_layer.transform.position = new Vector3(StartingLayer.transform.position.x, ((layer * LayerHeight) * StartingLayer.transform.localScale.y) + StartingLayer.transform.position.y, StartingLayer.transform.position.z);
                    this_layer.transform.localScale = StartingLayer.transform.localScale;
                    this_layer.transform.SetParent(parent.transform);
                    this_layer.transform.localRotation = StartingLayer.transform.localRotation;
                    if (layer >= CutOffTop || layer <= CutOffBottom)
                    {
                        Destroy(this_layer);
                    }
                    //testing line
                    //this_layer.transform.rotation = new Quaternion(0,0,0,0); // w component was StartingLayer.transform.rotation.w
                    //this_layer.transform.rotation.SetFromToRotation(this_layer.transform.position, StartingLayer.transform.position);
                //testing line end
                }
                layer++;
            }
            parent.transform.rotation = StartingLayer.transform.rotation;
            
            StartingLayer.SetActive(false);
        }




        // Update is called once per frame
        void Update()
        {
            skip_layers = (int)Math.Round(((1 - SkipLayersSlider) * images.Length / slice_multiplier) + 1, 0);
            //skip_layers = (int)Math.Round(((SkipLayersSlider.GetComponent<LinearMapping>().value) * images.Length / slice_multiplier) + 1, 0);
            CutOffTop = (int)Math.Round(((CutOffTopSlider) * images.Length) + 1, 0);
            parent.transform.position = Proxy.transform.position + proxy_translate_offset;

            parent.transform.rotation = Proxy.transform.rotation;

            if (starting_frames <= 0) {
                BuildDICOM();
                starting_frames--;
            }
        }
    }
}