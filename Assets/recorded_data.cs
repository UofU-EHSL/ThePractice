using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode]
public class recorded_data : MonoBehaviour {

    [Header("Basic stats")]
    public GameObject uid_gameobject;
    public string UID;
    public string form_id_uid;

    public string tooth_number;
    public string tooth_number_formid;

    public string tooth_info;
    public string tooth_info_formid;

    public float cut_time;
    public string cut_formid;

    public float small_time;
    public string small_hole_formid;

    public float large_time;
    public string large_hole_formid;

    private string implant_size = "null";
    public float implant_time;
    public string has_implant_formid;

    private string abutment_type;
    public float abutment_time;
    public string has_abutment_formid;

    public float impression_time;
    public string impression_made_formid;

    public float submit_time;
    public string submit_formid;

    private float current_timer = 0.0f;
    private float rolling_timer = 0.0f;

    [Header("Summary stats")]
    public string summary;
    public string summery_form_id;

    public void Start()
    {
        tooth_number = gameObject.name;
        Generate();
    }
    public void Generate()
    {
        tooth_info = GetComponent<single_tooth>().tooth_info.ToString();
        cut_time = 0.0f;
        small_time = 0.0f;
        large_time = 0.0f;
        implant_time = 0.0f;
        abutment_time = 0.0f;
        impression_time = 0.0f;
        submit_time = 0.0f;
        current_timer = 0.0f;
        rolling_timer = 0.0f;
    }
    public void Update()
    {
        current_timer += Time.deltaTime;
        rolling_timer += Time.deltaTime;
        
        
        if (GetComponent<single_tooth>().has_cut)
        {
            cut_time = current_timer;
        }
        if (GetComponent<single_tooth>().has_small_hole)
        {
            small_time = current_timer;
        }
        if (GetComponent<single_tooth>().has_large_hole)
        {
            large_time = current_timer;
        }
        if (GetComponent<single_tooth>().has_implant)
        {
            implant_size = GetComponent<single_tooth>().implant_gameobject.GetComponent<implant>().size.ToString();
            implant_time = current_timer;
        }
        if (GetComponent<single_tooth>().has_abutment)
        {
            abutment_type = GetComponent<single_tooth>().abutment_gameobject.GetComponent<abutment>().abutment_name;
            abutment_time = current_timer;
        }
        if (GetComponent<single_tooth>().was_impressed)
        {
            impression_time = current_timer;
        }
    }

    public void submit()
    {
        if (GetComponent<single_tooth>().missing_tooth && cut_time > 0)
        {
            UID = uid_gameobject.GetComponent<Text>().text;
            submit_time = current_timer;
            StartCoroutine(Post(UID, tooth_number, tooth_info, cut_time.ToString(), small_time.ToString(), large_time.ToString(), implant_time.ToString()+" "+implant_size, abutment_time.ToString()+" "+abutment_type, impression_time.ToString(), submit_time.ToString()));
        }
    }

    [SerializeField]
    public string BASE_URL;
    IEnumerator Post(string uid, string number, string info, string cut, string small, string large, string implant, string abutment, string impression, string submit)
    {
        WWWForm form = new WWWForm();

        form.AddField(form_id_uid, uid);
        form.AddField(tooth_number_formid, number);
        form.AddField(cut_formid, cut);
        form.AddField(tooth_info_formid, info);
        form.AddField(small_hole_formid, small);
        form.AddField(large_hole_formid, large);
        form.AddField(has_implant_formid, implant);
        form.AddField(has_abutment_formid, abutment);
        form.AddField(impression_made_formid, impression);
        form.AddField(submit_formid, submit);

        byte[] rawData = form.data;
        WWW www = new WWW(BASE_URL + "/formResponse", rawData);
        yield return www;
    }
}
