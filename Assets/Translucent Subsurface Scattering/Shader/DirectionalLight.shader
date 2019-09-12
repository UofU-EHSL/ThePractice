// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Translucent Subsurface Scattering/Directional Light" {
	Properties {
		_MainTex ("Base Texture", 2D) = "white" {}
		_TSSLitPos ("Light Position", Vector) = (0, 0, 0, 0)
		_TSSDistort ("Light Distort", Float) = 0.23
		_TSSPower ("Light Power", Float) = 4.8
		_TSSScale ("Light Scale", Float) = 2.2
		_TSSColor("Surface Color", Color) = (1, 1, 1, 1)
		_Offset("Offset", 2D) = "white" {}
		_AmbientColor ("Ambient", Color) = (0.1, 0.1, 0.1, 1)
	}
	SubShader {
		Tags { "RenderType" = "Opaque" "LightMode" = "ForwardBase" }
		Pass {
			CGPROGRAM
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"
			#include "Core.cginc"
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#pragma multi_compile _ TSS_BUMP
			#pragma multi_compile _ TSS_THICKNESS_MAP

			sampler2D _MainTex, _ThicknessTex;
			float4 _MainTex_ST, _BumpTex_ST;

			struct v2f {
				float4 pos : SV_POSITION;
				float3 lit : TEXCOORD0;
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
				o.lit = WorldSpaceLightDir(v.vertex);
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
				float thickness = 1.0;
				float2 scrpos = i.scrpos.xy / i.scrpos.w;
				thickness = tex2D(_ThicknessTex, scrpos).r;
				float3 L = normalize(i.lit);
				float3 V = normalize(i.view);
				float4 tss = TSS_DirLight(L, N, V, _LightColor0, thickness);
				tss += TSS_DiffSpec(L, N, V, _LightColor0);
				float4 albedo = tex2D(_MainTex, i.tex.xy);
				
				return albedo * tss;
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
