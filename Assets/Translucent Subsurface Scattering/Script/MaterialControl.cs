using UnityEngine;

public class MaterialControl : MonoBehaviour
{
	[Range(0f, 0.5f)] public float m_Distort = 0.23f;
	[Range(1f, 15f)] public float m_Power = 4.8f;
	[Range(0f, 10f)] public float m_Scale = 2.2f;
	public bool m_UseBumpMap = false;
	public bool m_UseThicknessMap = false;
	public Texture2D m_CustomThicknessMap;
	Renderer m_Rd;
	Material[] m_Mats;
	int m_ID_TSSDistort = 0;
	int m_ID_TSSPower = 0;
	int m_ID_TSSScale = 0;
	int m_ID_TSSThicknessMap = 0;
	[Header("Internal Material ID")]
	public int m_ID_TSSLitPos1 = 0;
	public int m_ID_TSSLitPos2 = 0;
	public int m_ID_TSSLitPos3 = 0;
	public int m_ID_TSSLitPos4 = 0;
	public int m_ID_TSSLitColor1 = 0;
	public int m_ID_TSSLitColor2 = 0;
	public int m_ID_TSSLitColor3 = 0;
	public int m_ID_TSSLitColor4 = 0;
	
	public void Initialize ()
	{
		m_Rd = GetComponent<Renderer> ();
		m_Mats = m_Rd.materials;
		m_ID_TSSDistort = Shader.PropertyToID ("_TSSDistort");
		m_ID_TSSPower = Shader.PropertyToID ("_TSSPower");
		m_ID_TSSScale = Shader.PropertyToID ("_TSSScale");
		m_ID_TSSThicknessMap = Shader.PropertyToID ("_ThicknessTex");
		m_ID_TSSLitPos1 = Shader.PropertyToID ("_TSSLitPos1");
		m_ID_TSSLitPos2 = Shader.PropertyToID ("_TSSLitPos2");
		m_ID_TSSLitPos3 = Shader.PropertyToID ("_TSSLitPos3");
		m_ID_TSSLitPos4 = Shader.PropertyToID ("_TSSLitPos4");
		m_ID_TSSLitColor1 = Shader.PropertyToID ("_TSSColor1");
		m_ID_TSSLitColor2 = Shader.PropertyToID ("_TSSColor2");
		m_ID_TSSLitColor3 = Shader.PropertyToID ("_TSSColor3");
		m_ID_TSSLitColor4 = Shader.PropertyToID ("_TSSColor4");
	}
	public void UpdateSelfParameters ()
	{
		for (int i = 0; i < m_Mats.Length; i++)
		{
			m_Mats[i].SetFloat (m_ID_TSSDistort, m_Distort);
			m_Mats[i].SetFloat (m_ID_TSSPower, m_Power);
			m_Mats[i].SetFloat (m_ID_TSSScale, m_Scale);
			if (m_UseThicknessMap) m_Mats[i].EnableKeyword ("TSS_THICKNESS_MAP");
			else                   m_Mats[i].DisableKeyword ("TSS_THICKNESS_MAP");
			if (m_UseBumpMap)      m_Mats[i].EnableKeyword ("TSS_BUMP");
			else                   m_Mats[i].DisableKeyword ("TSS_BUMP");
			
			if (m_CustomThicknessMap)
				m_Mats[i].SetTexture (m_ID_TSSThicknessMap, m_CustomThicknessMap);
		}
	}
	public void SetMaterialsVector (int name, Vector4 v)
	{
		for (int i = 0; i < m_Mats.Length; i++)
			m_Mats[i].SetVector (name, v);
	}
	public void SetMaterialsColor (int name, Color c)
	{
		for (int i = 0; i < m_Mats.Length; i++)
			m_Mats[i].SetColor (name, c);
	}
	public void SetMaterialsTexture (string name, Texture t)
	{
		// if we want to use custom thickness map, ignore outside set up.
		if (name == "_ThicknessTex" && m_CustomThicknessMap != null)
			return;
	
		for (int i = 0; i < m_Mats.Length; i++)
			m_Mats[i].SetTexture (name, t);
	}
	public void ApplyKeyword (string keyword, bool enable)
	{
		for (int i = 0; i < m_Mats.Length; i++)
		{
			if (enable)
				m_Mats[i].EnableKeyword (keyword);
			else
				m_Mats[i].DisableKeyword (keyword);
		}
	}
	public void SetShader (Shader sdr)
	{
		for (int i = 0; i < m_Mats.Length; i++)
			m_Mats[i].shader = sdr;
    }
}