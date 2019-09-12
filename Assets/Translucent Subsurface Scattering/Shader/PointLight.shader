// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Translucent Subsurface Scattering/Point Light" {
	Properties {
		_MainTex ("Base Texture", 2D) = "white" {}
		_BumpTex ("Bump Texture", 2D) = "black" {}
		_TSSLitPos1 ("Light Position 1", Vector) = (0, 0, 0, 0)
		_TSSLitPos2 ("Light Position 2", Vector) = (0, 0, 0, 0)
		_TSSLitPos3 ("Light Position 3", Vector) = (0, 0, 0, 0)
		_TSSLitPos4 ("Light Position 4", Vector) = (0, 0, 0, 0)
		_TSSDistort ("Light Distort", Float) = 0.23
		_TSSPower ("Light Power", Float) = 4.8
		_TSSScale ("Light Scale", Float) = 2.2
		_TSSColor1 ("Surface Color 1", Color) = (1, 1, 1, 1)
		_TSSColor2 ("Surface Color 2", Color) = (1, 1, 1, 1)
		_TSSColor3 ("Surface Color 3", Color) = (1, 1, 1, 1)
		_TSSColor4 ("Surface Color 4", Color) = (1, 1, 1, 1)
		_PtLitAttenC ("Const Attenuation", Range (0, 1)) = 0.1
		_PtLitAttenL ("Linear Attenuation", Range (0, 0.1)) = 0.1
		_PtLitAttenQ ("Quadratic Attenuation", Range (0, 0.1)) = 0.1
		_AmbientColor ("Ambient", Color) = (0.3, 0.3, 0.3, 1)
	}
	SubShader {
		Tags { "RenderType" = "Opaque" }
		Pass {
			CGPROGRAM
			#include "UnityCG.cginc"
			#include "Core.cginc"
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile TSS_POINT_LIGHT_1 TSS_POINT_LIGHT_2 TSS_POINT_LIGHT_3 TSS_POINT_LIGHT_4
			#pragma multi_compile _ TSS_BUMP
			#pragma multi_compile _ TSS_THICKNESS_MAP

			sampler2D _MainTex, _BumpTex, _ThicknessTex;
			float4 _MainTex_ST, _BumpTex_ST;

			struct v2f {
				float4 pos : SV_POSITION;
				float3 wldpos : TEXCOORD0;
				float4 tex : TEXCOORD1;
				float3 nor : TEXCOORD2;
				float3 view : TEXCOORD3;
				float3 tan : TEXCOORD4;
				float3 bin : TEXCOORD5;
				float4 scrpos : TEXCOORD6;
			};
			v2f vert (appdata_tan v)
			{
				TANGENT_SPACE_ROTATION;
				
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.tex.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.tex.zw = TRANSFORM_TEX(v.texcoord, _BumpTex);
				o.wldpos = mul(unity_ObjectToWorld, v.vertex);
				o.nor = mul((float3x3)unity_ObjectToWorld, v.normal);
				o.view = WorldSpaceViewDir(v.vertex);
				o.tan = mul((float3x3)unity_ObjectToWorld, v.tangent.xyz);
				o.bin = mul((float3x3)unity_ObjectToWorld, binormal);
				o.scrpos = ComputeScreenPos(o.pos);
				return o;
			}
			float4 frag (v2f i) : SV_TARGET
			{
				float3 N = normalize(i.nor);
#if TSS_BUMP
				float3 bump = tex2D(_BumpTex, i.tex.zw).rgb;
				bump = normalize(bump * 2.0 - 1.0);
				float3 T = normalize(i.tan);
				float3 B = normalize(i.bin);
				N = normalize(N + T * bump.x - B * bump.y);
#endif
				float thickness = 1.0;
#if TSS_THICKNESS_MAP
				float2 scrpos = i.scrpos.xy / i.scrpos.w;
				thickness = tex2D(_ThicknessTex, scrpos).r;
				//return thickness.rrrr;
#endif
				float3 V = normalize(i.view);
				float4 tss = TSS_PointLight(i.wldpos, N, V, thickness);
				float4 albedo = tex2D(_MainTex, i.tex.xy);
				return albedo * tss;
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
