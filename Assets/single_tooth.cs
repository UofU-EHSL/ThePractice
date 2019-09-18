using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// Implant code i think is working in here now and just needs to be tested
/// impression code is still not done at all and needs to be thought out and assets found
/// </summary>
public class single_tooth : MonoBehaviour {
    [Header("Basics")]
    public GameObject MRI;
    public int tooth_number;
    public bool missing_tooth = false;
    public Vector3 max_missing_tooth_size;
    public Vector3 min_missing_tooth_size;
    //public GameObject tooth_size_info;
    public Vector3 tooth_info;
    private float smallest_xy;
    public GameObject this_tooth;
    public GameObject gum;
    public GameObject cut_gum;
    public GameObject jaw;
    public GameObject small_hole;
    public GameObject large_hole;
    public GameObject xray;

    [Header("Impressions")]
    public GameObject impression_parent;
    public GameObject impressed_gums;
    public GameObject impressed_abutment;
    public GameObject impressed_tooth;
    public GameObject un_impressed;
    public bool was_impressed;

    [Header("Stats")]
    public bool has_cut;
    public bool has_small_hole;
    public bool has_large_hole;
    public bool has_implant;
    public bool has_abutment;
    public bool has_addon;

    [Header("Once Implanted")]
    public float UpScale;

    [Header("Add-ons")]
    public GameObject abutment_addon;
    public Vector3 init_location;
    public Vector3 location;
    public Vector3 location_limit = new Vector3(1, 1, 1);
    public Quaternion rotation;
    public Vector3 rotation_limit;
    public BoxCollider Boxcollider;

    [Header("Implants")]
    public GameObject implant_gameobject;
    public Vector3 implant_location;
    public Vector3 implant_rotation;

    [Header("Abutments")]
    public GameObject abutment_gameobject;
    public Vector3 abutment_location;
    public Quaternion abutment_rotation;

    public void Awake()
    {
        un_impressed = GameObject.Find("bottom goo/un_impressed");
        GetComponent<single_tooth>().tooth_info = new Vector3(Random.Range(GetComponent<single_tooth>().min_missing_tooth_size.x, GetComponent<single_tooth>().max_missing_tooth_size.x), Random.Range(GetComponent<single_tooth>().min_missing_tooth_size.y, GetComponent<single_tooth>().max_missing_tooth_size.y), Random.Range(GetComponent<single_tooth>().min_missing_tooth_size.z, GetComponent<single_tooth>().max_missing_tooth_size.z));
        Boxcollider = gameObject.GetComponent<BoxCollider>();
        this_tooth = gameObject;
      //tooth_size_info = GameObject.Find(this.gameObject.name + "/gauge");
        gum = GameObject.Find("gums " + tooth_number.ToString());
        cut_gum = GameObject.Find("gums cut " + tooth_number.ToString());
        jaw = GameObject.Find("Mandible " + tooth_number.ToString());
        small_hole = GameObject.Find("Mandible cut " + tooth_number.ToString());
        large_hole = GameObject.Find("Mandible cut2 " + tooth_number.ToString());
        xray = GameObject.Find("xray tooth " + tooth_number.ToString());

        cut_gum.GetComponent<MeshRenderer>().enabled = false;
        small_hole.GetComponent<MeshRenderer>().enabled = false;
        large_hole.GetComponent<MeshRenderer>().enabled = false;
        //tooth_size_info.SetActive(false);
        if (missing_tooth == true)
        {
            this_tooth.GetComponent<MeshRenderer>().enabled = false;
            //Boxcollider.enabled = true;
        }
        else{
            this_tooth.GetComponent<MeshRenderer>().enabled = true;
            //Boxcollider.enabled = false;
        }
        impressed_abutment = GameObject.Find(impression_parent.name + "/Implant " + tooth_number.ToString());
        impressed_gums = GameObject.Find(impression_parent.name + "/Gums " + tooth_number.ToString());
        impressed_tooth = GameObject.Find(impression_parent.name + "/Tooth " + tooth_number.ToString());
    }
    public void reset()
    {
        un_impressed.GetComponent<MeshRenderer>().enabled = true;
        has_cut = false;
        has_small_hole = false;
        has_large_hole = false;
        has_implant = false;
        has_abutment = false;
    }
    
    public void Update()
    {
        if (missing_tooth == true)
        {
            this_tooth.GetComponent<MeshRenderer>().enabled = false;
            Boxcollider.enabled = true;
        }
        else
        {
            this_tooth.GetComponent<MeshRenderer>().enabled = true;
            Boxcollider.enabled = false;
        }
        //implant_gameobject.GetComponent<implant>().set_in(this.gameObject.transform, implant_location, new Quaternion(implant_rotation.x, implant_rotation.y, implant_rotation.z, 1), UpScale);
        if (has_cut)
        {
            gum.GetComponent<MeshRenderer>().enabled = false;
            cut_gum.GetComponent<MeshRenderer>().enabled = true;
        }
        if (has_small_hole)
        {
            jaw.GetComponent<MeshRenderer>().enabled = false;
            small_hole.GetComponent<MeshRenderer>().enabled = true;
        }
        if (has_large_hole)
        {
            large_hole.GetComponent<MeshRenderer>().enabled = true;
            small_hole.GetComponent<MeshRenderer>().enabled = false;
        }
        if (missing_tooth == true)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
    }
    public void impress()
    {
            un_impressed.GetComponent<MeshRenderer>().enabled = false;
            if (missing_tooth == true)
            {
                impressed_abutment.GetComponent<MeshRenderer>().enabled = false;
                impressed_gums.GetComponent<MeshRenderer>().enabled = true;
                if (has_abutment == true)
                {
                    impressed_abutment.GetComponent<MeshRenderer>().enabled = true;
                    impressed_gums.GetComponent<MeshRenderer>().enabled = false;
                    impressed_tooth.GetComponent<MeshRenderer>().enabled = false;
                }
            }
            if (missing_tooth == false)
            {
                Debug.Log("I am in here");
                impressed_abutment.GetComponent<MeshRenderer>().enabled = false;
                impressed_gums.GetComponent<MeshRenderer>().enabled = false;
                impressed_tooth.GetComponent<MeshRenderer>().enabled = true;
            }
    }
    //this is where a blade will work
    public void OnTriggerEnter(Collider other)
    {
        //denture
        if (other.gameObject.GetComponent<denture>() && has_addon == true)
        {
            other.GetComponent<denture>().set_in(this.gameObject.transform, new Vector3(0,0,0), new Quaternion(0, 0, 0, 0));
        }

        //implant code
        if (other.gameObject.GetComponent<implant>() && has_large_hole == true && has_implant == false)
        {
            implant_gameobject = other.gameObject;
            has_implant = true;
            implant_gameobject.GetComponent<implant>().set_in(this.gameObject.transform, implant_location, new Quaternion(implant_rotation.x, implant_rotation.y, implant_rotation.z, 1), UpScale);
        }

        //abutment
        if (other.gameObject.GetComponent<abutment>() && has_implant == true)
        {
            implant_gameobject.GetComponent<CapsuleCollider>().enabled = false;
            abutment_gameobject = other.gameObject;
            has_abutment = true;
            abutment_gameobject.GetComponent<abutment>().set_in(implant_gameobject.transform, abutment_location, new Quaternion(0, 0, 0, 1), 1);
        }

        //abutment addon
        if (other.gameObject.GetComponent<abutment_addon>() && has_abutment == true)
        {
            //implant_gameobject.GetComponent<BoxCollider>().enabled = false;
            abutment_addon = other.gameObject;
            has_addon = true;
            location = new Vector3(0, abutment_gameobject.GetComponent<abutment>().addon_offset, 0);
            abutment_addon.GetComponent<abutment_addon>().set_in(implant_gameobject.transform, abutment_location+location, new Quaternion(0, 0, 0, 1), 1);
        }

        //max implant size calculator
        if (tooth_info.x >= tooth_info.y)
        {
            smallest_xy = tooth_info.y;
        }
        else
        {
            smallest_xy = tooth_info.x;
        }
        //cutting
        if (has_cut == false && other.name == "blade" && missing_tooth == true)
        {
            has_cut = true;
            gum.GetComponent<MeshRenderer>().enabled = false;
            cut_gum.GetComponent<MeshRenderer>().enabled = true; ;
        }

        //perio
        if (other.name == "Perio Probe" && missing_tooth == true)
        {
            //tooth_size_info.SetActive(true);
        }

        //abutment
        if (other.gameObject.tag == "abutment" && has_abutment == false)
        {
            has_abutment = true;
            abutment_gameobject = other.gameObject;
            other.gameObject.transform.SetParent(gameObject.transform);
            other.gameObject.transform.localPosition = abutment_location;
            other.gameObject.transform.localRotation = abutment_rotation;
        }
    }

    //this is where a drill will work
    public void OnTriggerExit(Collider other)
    {
        //implant reset
        if (other.GetComponent<implant>())
        {
            other.GetComponent<implant>().Reset_init();
            has_implant = false;
        }

        
        if (other.gameObject.GetComponent<abutment_addon>())
        {
            
            has_addon = false;
        }

        //abutment reset
        if (other.GetComponent<abutment>() && has_implant == true)
        {
            other.GetComponent<abutment>().Reset_init();
            has_abutment = false;
            implant_gameobject.GetComponent<CapsuleCollider>().enabled = true;
        }
        if (has_cut == true && other.GetComponent<bur>().size < 2)
        {
            jaw.GetComponent<MeshRenderer>().enabled = false;
            small_hole.GetComponent<MeshRenderer>().enabled = true;
            has_small_hole = true;
        }
        if (has_small_hole == true && other.gameObject.GetComponent<bur>().size <= smallest_xy && other.gameObject.GetComponent<bur>().size > 2)
        {
            large_hole.GetComponent<MeshRenderer>().enabled = true;
            small_hole.GetComponent<MeshRenderer>().enabled = false;
            has_large_hole = true;
        }
        if (other.name == "Perio Probe" && missing_tooth == true)
        {
            //tooth_size_info.SetActive(false);
        }
        //impressions
        if (other.tag == "impression") {
            //MRI.GetComponent<random_person>().impress();
        }
    }
}
