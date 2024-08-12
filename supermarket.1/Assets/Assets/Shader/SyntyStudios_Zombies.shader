Shader "SyntyStudios/Zombies" {
	Properties {
		_Texture ("Texture", 2D) = "white" {}
		_Blood ("Blood", 2D) = "white" {}
		_BloodColor ("BloodColor", Vector) = (0.6470588,0.2569204,0.2569204,0)
		_BloodAmount ("BloodAmount", Range(0, 1)) = 0
		_Spec ("Spec", Vector) = (0,0,0,0)
		_Smoothness ("Smoothness", Range(0, 1)) = 0
		_Emissive ("Emissive", 2D) = "white" {}
		[HDR] _EmissiveColor ("Emissive Color", Vector) = (0,0,0,0)
		[HideInInspector] _texcoord2 ("", 2D) = "white" {}
		[HideInInspector] _texcoord ("", 2D) = "white" {}
		[HideInInspector] __dirty ("", Float) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	Fallback "Diffuse"
	//CustomEditor "ASEMaterialInspector"
}