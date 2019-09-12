Shader "Translucent Subsurface Scattering/Thickness Map" {
	Properties {
		_MainTex("Thickness Texture", 2D) = "white" {}
		_Sigma("Sigma", Float) = 1.0
	}
	SubShader {
		Cull Off ZWrite Off ZTest Always
		Pass {
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float _Sigma;

			float4 frag (v2f_img i) : SV_Target
			{
				float4 rtc = tex2D(_MainTex, i.uv);
				float intensity = exp(-_Sigma * abs(rtc.r));
				float4 c = float4(intensity.xxx, 1.0);
				c = lerp(0.0, 1.0 - c, rtc.a);
				return float4(saturate(1.0 - c.rrr), 1.0);
			}
			ENDCG
		}
	}
	FallBack Off
}