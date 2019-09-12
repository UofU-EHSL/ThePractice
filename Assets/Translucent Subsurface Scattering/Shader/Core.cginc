#ifndef TSS_INCLUDED
#define TSS_INCLUDED

float _TSSDistort, _TSSPower, _TSSScale;
float _PtLitAttenC, _PtLitAttenL, _PtLitAttenQ;
float3 _TSSLitPos1, _TSSLitPos2, _TSSLitPos3, _TSSLitPos4;
float4 _TSSColor1, _TSSColor2, _TSSColor3, _TSSColor4, _AmbientColor;

float4 TSS_DirLight (float3 L, float3 N, float3 V, float4 C, float thickness)
{
	float3 translit = L + N * _TSSDistort;
	float vdl = saturate(dot(V, -translit));
	float t = pow(vdl, _TSSPower) * _TSSScale * thickness;
	return C * t;
}
float4 TSS_DiffSpec (float3 L, float3 N, float3 V, float4 C)
{
	float diff = max(dot(N, L), 0);
	float3 H = normalize(V + L);
	float spec = clamp(12 * pow(max(dot(N, H), 0), 120), 0, 1);
	return (diff + spec) * C;
}
float TSS_Atten (float3 dist)
{
	float len = length(dist);
	return 1 / (_PtLitAttenC + _PtLitAttenL * len + _PtLitAttenQ * len * len);
}
float4 TSS_PointLight (float3 wldpos, float3 N, float3 V, float thickness)
{
	float3 L1 = _TSSLitPos1 - wldpos;
	float3 L2 = _TSSLitPos2 - wldpos;
	float3 L3 = _TSSLitPos3 - wldpos;
	float3 L4 = _TSSLitPos4 - wldpos;
	
	float atten1 = TSS_Atten(L1);
	float atten2 = TSS_Atten(L2);
	float atten3 = TSS_Atten(L3);
	float atten4 = TSS_Atten(L4);
	
	L1 = normalize(L1);
	L2 = normalize(L2);
	L3 = normalize(L3);
	L4 = normalize(L4);
	
	float4 tss1 = TSS_DirLight(L1, N, V, _TSSColor1, thickness);
	float4 tss2 = TSS_DirLight(L2, N, V, _TSSColor2, thickness);
	float4 tss3 = TSS_DirLight(L3, N, V, _TSSColor3, thickness);
	float4 tss4 = TSS_DirLight(L4, N, V, _TSSColor4, thickness);

	float4 ds1 = TSS_DiffSpec(L1, N, V, _TSSColor1);
	float4 ds2 = TSS_DiffSpec(L2, N, V, _TSSColor2);
	float4 ds3 = TSS_DiffSpec(L3, N, V, _TSSColor3);
	float4 ds4 = TSS_DiffSpec(L4, N, V, _TSSColor4);
	
	float4 lit = float4(0, 0, 0, 1);
#if TSS_POINT_LIGHT_1
	lit += (ds1 * atten1) + (tss1 * atten1);	
#endif
#if TSS_POINT_LIGHT_2
	lit += (ds1 * atten1) + (tss1 * atten1);
	lit += (ds2 * atten2) + (tss2 * atten2);
#endif
#if TSS_POINT_LIGHT_3
	lit += (ds1 * atten1) + (tss1 * atten1);
	lit += (ds2 * atten2) + (tss2 * atten2);
	lit += (ds3 * atten3) + (tss3 * atten3);
#endif
#if TSS_POINT_LIGHT_4
	lit += (ds1 * atten1) + (tss1 * atten1);
	lit += (ds2 * atten2) + (tss2 * atten2);
	lit += (ds3 * atten3) + (tss3 * atten3);
	lit += (ds4 * atten4) + (tss4 * atten4);
#endif
	return lit + _AmbientColor;
}

#endif