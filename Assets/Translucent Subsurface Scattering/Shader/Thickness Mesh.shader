// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Translucent Subsurface Scattering/Thickness Mesh" {
	Properties {}
	SubShader {
		Pass {
			Cull Back Lighting Off Fog { Mode Off }
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 pos : SV_POSITION;
				half dist : TEXCOORD0;
			};
			v2f vert (appdata_base v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				COMPUTE_EYEDEPTH(o.dist);
				return o;
			}
			float4 frag (v2f i) : SV_TARGET
			{
				float depth = i.dist;
				return float4(depth, depth, depth, 1);
			}
			ENDCG
		}
		Pass {
			Lighting Off Cull Front Blend One One ZTest Always Fog { Mode Off }
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 pos : SV_POSITION;
				half dist : TEXCOORD0;
			};
			v2f vert (appdata_base v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				COMPUTE_EYEDEPTH(o.dist);
				return o;
			}
			float4 frag(v2f i) : COLOR
			{
				float depth = -i.dist;
				return float4(depth, depth, depth, 1);
			}
			ENDCG
		}
	}
	FallBack Off
}
