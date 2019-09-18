using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.Events;

    public class xray : MonoBehaviour
    {

        //public UnityEvent takeXray;
        public MaterialControl[] m_BeLightOfs;
        [Header("Thickness")]
        public Camera m_MainCam;
        [Range(-10.00f, 10f)] public float m_ThicknessRange = 0.03f;
        [Header("Internal")]
        RenderTexture m_RTThicknessMapMesh;
        RenderTexture m_RTThicknessMap;
        int m_LightSourceType = 0;  // 0 - point, 1 - directional
        int m_NumOfPointLight = 1;
        GameObject m_RTCamObj;
        Camera m_RTCam;
        Shader m_SdrThicknessMesh;
        Material m_MatThicknessMap;

        private void Start()
        {

            m_BeLightOfs = GameObject.FindObjectsOfType<MaterialControl>();
            for (int i = 0; i < m_BeLightOfs.Length; i++)
                m_BeLightOfs[i].Initialize();

            ApplyLightType();
            m_RTThicknessMapMesh = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGBFloat);
            m_RTThicknessMapMesh.name = "Proc1";
            m_RTThicknessMap = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGBFloat);
            m_RTThicknessMap.name = "Proc2";
            m_SdrThicknessMesh = Shader.Find("Translucent Subsurface Scattering/Thickness Mesh");
            Shader tm = Shader.Find("Translucent Subsurface Scattering/Thickness Map");
            m_MatThicknessMap = new Material(tm);
        }
        void OnDisable()
        {
            DestroyImmediate(m_MatThicknessMap);
        }
        private void Update()
        {
            // build thickness map
            if (m_RTCamObj == null)
            {
                m_RTCamObj = new GameObject("RTCamera");
                m_RTCamObj.hideFlags = HideFlags.DontSave;
                m_RTCamObj.transform.parent = m_MainCam.gameObject.transform;
                m_RTCam = m_RTCamObj.AddComponent<Camera>();
            }
            m_RTCam.enabled = false;
            m_RTCam.CopyFrom(m_MainCam);
            m_RTCam.targetTexture = m_RTThicknessMapMesh;
            m_RTCam.backgroundColor = Color.black;
            m_RTCam.clearFlags = CameraClearFlags.SolidColor;
            m_RTCam.RenderWithShader(m_SdrThicknessMesh, "");
            m_MatThicknessMap.SetTexture ("_ThicknessTex", m_RTThicknessMapMesh);
            m_MatThicknessMap.SetFloat("_Sigma", m_ThicknessRange);
            Graphics.Blit(m_RTThicknessMapMesh, m_RTThicknessMap, m_MatThicknessMap);

            foreach (MaterialControl item in m_BeLightOfs)
            {
                item.SetMaterialsTexture("_ThicknessTex", m_RTThicknessMap);
            }
        }
        void ApplyLightType()
        {
            foreach (MaterialControl item in m_BeLightOfs)
            {
                item.SetShader(Shader.Find("Translucent Subsurface Scattering/Directional Light"));
            }
        }
    }