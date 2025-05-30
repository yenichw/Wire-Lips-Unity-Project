Shader "RealToon/Version 5/Default/Default" {
	Properties {
		[Enum(Off,2,On,0)] _DoubleSided ("Double Sided", Float) = 2
		_MainTex ("Texture", 2D) = "white" {}
		[MaterialToggle] _TexturePatternStyle ("Texture Pattern Style", Float) = 0
		_MainColor ("Main Color", Vector) = (0.7843137,0.7843137,0.7843137,1)
		[MaterialToggle] _EnableTextureTransparent ("Enable Texture Transparent", Float) = 0
		_Cutout ("Cutout", Range(0, 1)) = 0
		[MaterialToggle] _AlphaBaseCutout ("Alpha Base Cutout", Float) = 1
		[MaterialToggle] _UseSecondaryCutout ("Use Secondary Cutout", Float) = 0
		_SecondaryCutout ("Secondary Cutout", 2D) = "white" {}
		_NormalMap ("Normal Map", 2D) = "bump" {}
		_NormalMapIntensity ("Normal Map Intensity", Float) = 0
		_Saturation ("Saturation", Range(0, 2)) = 1
		_OutlineWidth ("Width", Float) = 0.5
		_OutlineWidthControl ("Width Control", 2D) = "white" {}
		[Enum(Normal,0,Origin,1)] _OutlineExtrudeMethod ("Outline Extrude Method", Float) = 0
		_OutlineOffset ("Outline Offset", Vector) = (0,0,0,1)
		_OutlineZPostionInCamera ("Outline Z Position In Camera", Float) = 0
		[Enum(Off,1,On,0)] _DoubleSidedOutline ("Double Sided Outline", Float) = 1
		_OutlineColor ("Color", Vector) = (0,0,0,1)
		_NoisyOutlineIntensity ("Noisy Outline Intensity", Range(0, 1)) = 0
		[MaterialToggle] _DynamicNoisyOutline ("Dynamic Noisy Outline", Float) = 0
		[MaterialToggle] _LightAffectOutlineColor ("Light Affect Outline Color", Float) = 0
		[MaterialToggle] _OutlineWidthAffectedByViewDistance ("Outline Width Affected By View Distance", Float) = 0
		[MaterialToggle] _VertexColorRedAffectOutlineWitdh ("Vertex Color Red Affect Outline Witdh", Float) = 0
		_SelfLitIntensity ("Intensity", Range(0, 1)) = 0
		_SelfLitColor ("Color", Vector) = (1,1,1,1)
		_SelfLitPower ("Power", Float) = 2
		[MaterialToggle] _SelfLitHighContrast ("High Contrast", Float) = 1
		_MaskSelfLit ("Mask Self Lit", 2D) = "white" {}
		_GlossIntensity ("Gloss Intensity", Range(0, 1)) = 1
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
		_ReduceShadowPointLight ("Reduce Shadow (Point Light)", Float) = 0
		_ReduceShadowSpotDirectionalLight ("Reduce Shadow (Spot & Directional Light)", Float) = 10
		_ShadowHardness ("Shadow Hardness", Range(0, 1)) = 0
		_HighlightColor ("Highlight Color", Vector) = (1,1,1,1)
		_HighlightColorPower ("Highlight Color Power", Float) = 1
		_SelfShadowRealtimeShadowIntensity ("Self Shadow & Realtime Shadow Intensity", Range(0, 1)) = 1
		_SelfShadowThreshold ("Threshold", Range(0, 1)) = 0.85
		[MaterialToggle] _VertexColorGreenControlSelfShadowThreshold ("Vertex Color Green Control Self Shadow Threshold", Float) = 0
		_SelfShadowHardness ("Hardness", Range(0, 1)) = 1
		_SelfShadowRealTimeShadowColor ("Self Shadow & Real Time Shadow Color", Vector) = (1,1,1,1)
		_SelfShadowRealTimeShadowColorPower ("Self Shadow & Real Time Shadow Color Power", Float) = 1
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
		[Toggle(N_F_STIAL_ON )] _N_F_STIAL ("Show In Ambient Light", Float) = 0
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
		[MaterialToggle] _CustomLightDirectionFollowObjectRotation ("Follow Object Rotation", Float) = 0
		_CustomLightDirection ("Custom Light Direction", Vector) = (0,0,10,0)
		_ReflectionIntensity ("Intensity", Range(0, 1)) = 0
		_ReflectionRoughtness ("Roughtness", Float) = 0
		_RefMetallic ("Metallic", Range(0, 1)) = 0
		_MaskReflection ("Mask Reflection", 2D) = "white" {}
		[MaterialToggle] _UseFReflection ("Use FReflection", Float) = 0
		_FReflection ("FReflection", 2D) = "black" {}
		_RimLightUnfill ("Unfill", Float) = 1.5
		_RimLightColor ("Color", Vector) = (1,1,1,1)
		_RimLightColorPower ("Color Power", Float) = 10
		_RimLightSoftness ("Softness", Range(0, 1)) = 1
		[MaterialToggle] _RimLightInLight ("Rim Light In Light", Float) = 1
		[MaterialToggle] _LightAffectRimLightColor ("Light Affect Rim Light Color", Float) = 0
		_RefVal ("ID", Float) = 0
		[Enum(None,8,A,0,B,2)] _Oper ("Set 1", Float) = 8
		[Enum(None,8,A,6,B,7)] _Compa ("Set 2", Float) = 8
		[Toggle(N_F_CO_ON)] _N_F_CO ("Cutout", Float) = 0
		[Toggle(N_F_O_ON)] _N_F_O ("Outline", Float) = 1
		[Toggle(N_F_CA_ON)] _N_F_CA ("Color Adjustment", Float) = 0
		[Toggle(N_F_SL_ON)] _N_F_SL ("Self Lit", Float) = 0
		[Toggle(N_F_GLO_ON)] _N_F_GLO ("Gloss", Float) = 0
		[Toggle(N_F_GLOT_ON)] _N_F_GLOT ("Gloss Texture", Float) = 0
		[Toggle(N_F_SS_ON)] _N_F_SS ("Self Shadow", Float) = 1
		[Toggle(N_F_SON_ON)] _N_F_SON ("Smooth Object Normal", Float) = 0
		[Toggle(N_F_SCT_ON)] _N_F_SCT ("Shadow Color Texture", Float) = 0
		[Toggle(N_F_ST_ON)] _N_F_ST ("ShadowT", Float) = 0
		[Toggle(N_F_PT_ON)] _N_F_PT ("PTexture", Float) = 0
		[Toggle(N_F_CLD_ON)] _N_F_CLD ("Custom Light Direction", Float) = 0
		[Toggle(N_F_R_ON)] _N_F_R ("Relfection", Float) = 0
		[Toggle(N_F_FR_ON)] _N_F_FR ("FRelfection", Float) = 0
		[Toggle(N_F_RL_ON)] _N_F_RL ("Rim Light", Float) = 0
		[Toggle(N_F_HDLS_ON)] _N_F_HDLS ("Hide Directional Light Shadow", Float) = 0
		[Toggle(N_F_HPSS_ON)] _N_F_HPSS ("Hide Point & Spot Light Shadow", Float) = 0
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