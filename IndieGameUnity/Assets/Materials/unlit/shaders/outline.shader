// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Emil/outline" {

	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo", 2D) = "white" {}

		_Cutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5

		_Glossiness("Smoothness", Range(0.0, 1.0)) = 0.5
		[Gamma] _Metallic("Metallic", Range(0.0, 1.0)) = 0.0
		_MetallicGlossMap("Metallic", 2D) = "white" {}

		_BumpScale("Scale", Float) = 1.0
		_BumpMap("Normal Map", 2D) = "bump" {}

		_Parallax("Height Scale", Range(0.005, 0.08)) = 0.02
		_ParallaxMap("Height Map", 2D) = "black" {}

		_OcclusionStrength("Strength", Range(0.0, 1.0)) = 1.0
		_OcclusionMap("Occlusion", 2D) = "white" {}

		_EmissionColor("Color", Color) = (0,0,0)
		_EmissionMap("Emission", 2D) = "white" {}

		_DetailMask("Detail Mask", 2D) = "white" {}

		_DetailAlbedoMap("Detail Albedo x2", 2D) = "grey" {}
		_DetailNormalMapScale("Scale", Float) = 1.0
		_DetailNormalMap("Normal Map", 2D) = "bump" {}

		[Enum(UV0,0,UV1,1)] _UVSec("UV Set for secondary textures", Float) = 0

			// UI-only data
			[HideInInspector] _EmissionScaleUI("Scale", Float) = 0.0
			[HideInInspector] _EmissionColorUI("Color", Color) = (1,1,1)

			// Blending state
			[HideInInspector] _Mode("__mode", Float) = 0.0
			[HideInInspector] _SrcBlend("__src", Float) = 1.0
			[HideInInspector] _DstBlend("__dst", Float) = 0.0
			[HideInInspector] _ZWrite("__zw", Float) = 1.0

			// -------------------------
			// Added Outline properties
			_OutlineColor("Outline Color", Color) = (0,0,0,1)
			_Outline("Outline width", Range(.002, 0.3)) = .005
			// -------------------------
	}

		CGINCLUDE
#define UNITY_SETUP_BRDF_INPUT MetallicSetup
			ENDCG

			/////////////////////////////////////////////////////////////////////////////////////////////

			SubShader
		{
			Tags { "RenderType" = "Opaque" "PerformanceChecks" = "False" }
			LOD 300

			// ----------------------
			// Start of Outline adding

			CGINCLUDE
			#include "UnityCG.cginc"

			struct appdata {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				UNITY_FOG_COORDS(0)
				fixed4 color : COLOR;
			};

			uniform float _Outline;
			uniform float4 _OutlineColor;

			v2f vert(appdata v) {
				// just make a copy of incoming vertex data but scaled according to normal direction
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);

				float3 norm = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, v.normal));
				float2 offset = TransformViewToProjection(norm.xy);

				o.pos.xy += offset * o.pos.z * _Outline;
				o.color = _OutlineColor;
				UNITY_TRANSFER_FOG(o,o.pos);
				return o;
			}
			ENDCG

			Pass {
				Name "OUTLINE"
				Tags { "LightMode" = "Always" }
				Cull Front
				ZWrite On
				ColorMask RGB
				Blend SrcAlpha OneMinusSrcAlpha

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				fixed4 frag(v2f i) : SV_Target
				{
					UNITY_APPLY_FOG(i.fogCoord, i.color);
					return i.color;
				}
				ENDCG
			}

				// End of Outline adding
				// ----------------------


				}

					FallBack "Standard"
					CustomEditor "CustomStandardShaderGUI"
}
