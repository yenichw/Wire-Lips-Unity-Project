Shader "Dissolve/Dissolve_TexturCoords" {
	Properties {
		_Color ("Main Color", Vector) = (1,1,1,1)
		_SpecColor ("Specular Color", Vector) = (0.5,0.5,0.5,1)
		_Shininess ("Shininess", Range(0.03, 1)) = 0.078125
		_Amount ("Amount", Range(0, 1)) = 0.5
		_StartAmount ("StartAmount", Float) = 0.1
		_Illuminate ("Illuminate", Range(0, 1)) = 0.5
		_Tile ("Tile", Float) = 1
		_DissColor ("DissColor", Vector) = (1,1,1,1)
		_ColorAnimate ("ColorAnimate", Vector) = (1,1,1,1)
		_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
		_BumpMap ("Normalmap", 2D) = "bump" {}
		_DissolveSrc ("DissolveSrc", 2D) = "white" {}
		_DissolveSrcBump ("DissolveSrcBump", 2D) = "white" {}
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Specular"
}