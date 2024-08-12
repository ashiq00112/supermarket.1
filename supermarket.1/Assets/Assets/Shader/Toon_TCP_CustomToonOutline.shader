Shader "Toon/TCP_CustomToonOutline" {
	Properties {
		_TextureSample3 ("Texture Sample", 2D) = "white" {}
		_OutlineWidth2 ("Outline  Width", Range(0.0001, 0.5)) = 0.0065
		_TextureRamp ("Texture Ramp", 2D) = "white" {}
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
}