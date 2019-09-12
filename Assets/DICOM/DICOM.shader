
Shader "Unlit/dicom"
{
    Properties
    {
        _MainTex ("Albedo Texture", 2D) = "white" {}
		//_MainTex("Base (RGB) Transparency (A)", 2D) = "" {}
		_cutoff ("Alpha cutoff", Range(0,1)) = .1
		_lighten ("lighten", float) = 0
		//Display test
		_min ("min", float) = 0
		_max ("max", float) = 0

		_min2 ("min", float) = 0
		_max2 ("max", float) = 0
    }

    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float _cutoff;
			float _lighten;

			float _min;
			float _max;
			
			float _min2;
			float _max2;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
				float cutoff = _cutoff;
				float lighten = _lighten;

				float min = _min;
				float max = _max;
				
				float min2 = _min2;
				float max2 = _max2;

				col.a = col.r+lighten;

				if(col.r >= min && col.r <= max){
					col.g = col.g-0.5;
				}

				if(col.r >= min2 && col.r <= max2){
					col.b = col.b-0.5;
				}

				if(col.a <= _cutoff){
					col.a = 0.0;
				}
				//AlphaTest Greater [_cutoff]
				//SetTexture [_MainTex] {combine texture }
                return col;
            }
            ENDCG
        }
    }
}

/*
Shader "Unlit/dicom" {
	Properties{
		_MainTex("Base (RGB) Transparency (A)", 2D) = "" {}
		_Cutoff("Alpha cutoff", Range(0,1)) = 0.5
		//_Lighten("lighten", float) = 0
		//Display test
		//_Min_Rendered("min_rendered", float) = 0
		//_Max_Rendered("max_rendered", float) = 0
	}
		SubShader{
		
			//LOD 100

			//ZWrite Off
			//Blend SrcAlpha OneMinusSrcAlpha
			//Cull Off

			Pass{

		// Use the Cutoff parameter defined above to determine
		// what to render.
		AlphaTest Greater[_Cutoff]
		Material{
		Diffuse(1,1,1,1)
		Ambient(1,1,1,1)


	}
		Lighting On
		SetTexture[_MainTex]{ combine texture * primary }
	}
	}
}
*/