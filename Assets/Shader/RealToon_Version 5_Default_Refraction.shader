Shader "RealToon/Version 5/Default/Refraction" {
	Properties {
		[Enum(Off,2,On,0)] _DoubleSided ("Double Sided", Float) = 2
		_RefractionIntensity ("Refraction Intensity", Range(0, 1)) = 1
		_MainColor ("Main Color", Vector) = (0.7843137,0.7843137,0.7843137,1)
		[MaterialToggle] _MainColorAffectTexture ("Main Color Affect Texture", Float) = 0
		_TextureIntesnity ("Texture Intesnity", Range(0, 1)) = 0
		_MainTex ("Texture", 2D) = "white" {}
		[MaterialToggle] _TexturePatternStyle ("Texture Pattern Style", Float) = 0
		_NormalMap ("Normal Map", 2D) = "bump" {}
		_NormalMapIntensity ("Normal Map Intensity", Float) = 0
		_Saturation ("Saturation", Range(0, 2)) = 1
		_SelfLitIntensity ("Intensity", Range(0, 1)) = 0
		_SelfLitColor ("Color", Vector) = (1,1,1,1)
		_SelfLitPower ("Power", Float) = 2
		[MaterialToggle] _SelfLitHighContrast ("High Contrast", Float) = 1
		_MaskSelfLit ("Mask Self Lit", 2D) = "white" {}
		_GlossIntensity ("Intensity", Range(0, 1)) = 1
		_Glossiness ("Glossiness", Range(0, 1)) = 0.6
		_GlossSoftness ("Softness", Range(0, 1)) = 0
		_GlossColor ("Color", Vector) = (1,1,1,1)
		_GlossColorPower ("Color Power", Float) = 10
		_MaskGloss ("Mask Gloss", 2D) = "white" {}
		_GlossTexture ("Gloss Texture", 2D) = "black" {}
		_GlossTextureSoftness ("Softness", Float) = 0
		_GlossTextureRotate ("Rotate", Float) = 0
		[MaterialToggle] _GlossTextureFollowObjectRotation ("Follow Object Rotation", Float) = 0
		_GlossTextureFollowLight ("Follow Light", Range(0, 1)) = 0
		_OverallShadowColor ("Overall Shadow Color", Vector) = (0,0,0,1)
		_OverallShadowColorPower ("Overall Shadow Color Power", Float) = 1
		[MaterialToggle] _SelfShadowShadowTAtViewDirection ("Self Shadow & ShadowT At View Direction", Float) = 0
		_HighlightColor ("Highlight Color", Vector) = (1,1,1,1)
		_HighlightColorPower ("Highlight Color Power", Float) = 1
		_SelfShadowIntensity ("Self Shadow Intensity", Range(0, 1)) = 1
		_SelfShadowThreshold ("Threshold", Range(0, 1)) = 0.85
		[MaterialToggle] _VertexColorGreenControlSelfShadowThreshold ("Vertex Color Green Control Self Shadow Threshold", Float) = 0
		_SelfShadowHardness ("Hardness", Range(0, 1)) = 1
		_SelfShadowColor ("Color", Vector) = (1,1,1,1)
		_SelfShadowColorPower ("Color Power", Float) = 1
		[MaterialToggle] _SelfShadowAffectedByLightShadowStrength ("Self Shadow Affected By Light Shadow Strength", Float) = 0
		_SmoothObjectNormal ("Smooth Object Normal", Range(0, 1)) = 0
		[MaterialToggle] _VertexColorBlueControlSmoothObjectNormal ("Vertex Color Blue Control Smooth Object Normal", Float) = 0
		_XYZPosition ("XYZ Position", Vector) = (0,0,0,0)
		_XYZHardness ("XYZ Hardness", Float) = 14
		[MaterialToggle] _ShowNormal ("Show Normal", Float) = 0
		_ShadowColorTexture ("Shadow Color Texture", 2D) = "white" {}
		_ShadowColorTexturePower ("Power", Float) = 0
		_ShadowTIntensity ("ShadowT Intensity", Range(0, 1)) = 1
		_ShadowT ("ShadowT", 2D) = "white" {}
		_ShadowTLightThreshold ("Light Threshold", Float) = 50
		_ShadowTShadowThreshold ("Shadow Threshold", Float) = 0
		_ShadowTColor ("Color", Vector) = (1,1,1,1)
		_ShadowTColorPower ("Color Power", Float) = 1
		_ShadowTHardness ("Hardness", Range(0, 1)) = 1
		[Toggle(N_F_STIS_ON)] _N_F_STIS ("Show In Shadow", Float) = 0
		[Toggle(N_F_STIAL_ON)] _N_F_STIAL ("Show In Ambient Light", Float) = 0
		_ShowInAmbientLightIntensity ("Show In Ambient Light Intensity", Range(0, 1)) = 1
		_ShowInAmbientLightShadowThreshold ("Show In Ambient Light & Shadow Threshold", Float) = 0.4
		[MaterialToggle] _LightFalloffAffectShadowT ("Light Falloff Affect ShadowT", Float) = 0
		_PTexture ("PTexture", 2D) = "white" {}
		_PTexturePower ("Power", Float) = 1
		[MaterialToggle] _GIFlatShade ("GI Flat Shade", Float) = 0
		_GIShadeThreshold ("GI Shade Threshold", Range(0, 1)) = 0
		[MaterialToggle] _LightAffectShadow ("Light Affect Shadow", Float) = 0
		_LightIntensity ("Light Intensity", Float) = -1
		_EnvironmentalLightingIntensity ("Environmental Lighting Intensity", Float) = 1
		_PointSpotlightIntensity ("Point and Spot Light Intensity", Float) = 0.3
		_LightFalloffSoftness ("Light Falloff Softness", Range(0, 1)) = 1
		_CustomLightDirectionIntensity ("Intensity", Range(0, 1)) = 0
		_CustomLightDirection ("Custom Light Direction", Vector) = (0,0,10,0)
		[MaterialToggle] _CustomLightDirectionFollowObjectRotation ("Follow Object Rotation", Float) = 0
		_ReflectionIntensity ("Intensity", Range(0, 1)) = 0
		_ReflectionRoughtness ("Roughtness", Float) = 0
		_MaskReflection ("Mask Reflection", 2D) = "white" {}
		[MaterialToggle] _UseFReflection ("Use FReflection", Float) = 0
		_FReflection ("FReflection", 2D) = "black" {}
		_RimLightUnfill ("Unfill", Float) = 1.5
		_RimLightColor ("Color", Vector) = (1,1,1,1)
		_RimLightColorPower ("Color Power", Float) = 10
		_RimLightSoftness ("Softness", Range(0, 1)) = 1
		[MaterialToggle] _RimLightInLight ("Rim Light In Light", Float) = 1
		[MaterialToggle] _LightAffectRimLightColor ("Light Affect Rim Light Color", Float) = 0
		_Depth ("Depth", Float) = 0.2
		_DepthEdgeHardness ("Edge Hardness", Range(0, 1)) = 0.1
		_DepthColor ("Color", Vector) = (0.5,0.5,0.5,1)
		_DepthColorPower ("Color Power", Float) = 1.8
		_RefVal ("ID", Float) = 0
		[Enum(None,8,A,0,B,2)] _Oper ("Set 1", Float) = 8
		[Enum(None,8,A,6,B,7)] _Compa ("Set 2", Float) = 8
		[Toggle(N_F_CA_ON)] _N_F_CA ("Color Adjustment", Float) = 0
		[Toggle(N_F_SL_ON)] _N_F_SL ("Self Lit", Float) = 0
		[Toggle(N_F_GLO_ON)] _N_F_GLO ("Gloss", Float) = 0
		[Toggle(N_F_GLOT_ON)] _N_F_GLOT ("Gloss Texture", Float) = 0
		[Toggle(N_F_SS_ON)] _N_F_SS ("Self Shadow", Float) = 0
		[Toggle(N_F_SON_ON)] _N_F_SON ("Smooth Object Normal", Float) = 0
		[Toggle(N_F_SCT_ON)] _N_F_SCT ("Shadow Color Texture", Float) = 0
		[Toggle(N_F_ST_ON)] _N_F_ST ("ShadowT", Float) = 0
		[Toggle(N_F_PT_ON)] _N_F_PT ("PTexture", Float) = 0
		[Toggle(N_F_CLD_ON)] _N_F_CLD ("Custom Light Direction", Float) = 0
		[Toggle(N_F_R_ON)] _N_F_R ("Reflection", Float) = 0
		[Toggle(N_F_FR_ON)] _N_F_FR ("FReflection", Float) = 0
		[Toggle(N_F_RL_ON)] _N_F_RL ("Rim Light", Float) = 0
		[Toggle(N_F_D_ON)] _N_F_D ("Depth", Float) = 0
		[Enum(On,1,Off,0)] _ZWrite ("ZWrite", Float) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	//CustomEditor "RealToonShaderGUI"
}