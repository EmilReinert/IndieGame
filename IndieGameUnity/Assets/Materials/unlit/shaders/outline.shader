Shader "Emil/outline"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_Light("Light", Range(0,1)) = 0.5
		_MainTex("Texture", 2D) = "white" {}
		_TexStr("Texture Strength", Range(0,1)) = 0.5
		_SecTex("Texture", 2D) = "white" {}
		_TexStr2("Texture Strength", Range(0,1)) = 0.5
			// -------------------------
			// Added Outline properties
			_OutlineColor("Outline Color", Color) = (0,0,0,1)
			_Outline("Outline width", Range(.2, 0.3)) = 0.2
			// -------------------------

	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 100

			Cull off
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

			Pass
			{
			Tags {"LightMode" = "ForwardBase"}
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				// make fog work
				#pragma multi_compile_fog
			//make shadows work
			#pragma multi_compile_fwdbase

			#include "AutoLight.cginc"
	#include "UnityCG.cginc"


			struct SHADERDATA
			{
			float2 uv : TEXCOORD0;
			float2 uv2 : TEXCOORD2;
				float4 _ShadowCoord : TEXCOORD1;
				float4 position : SV_POSITION;
			};


			sampler2D _MainTex;
			sampler2D _SecTex;
			float4 _MainTex_ST;
			float4 _SecTex_ST;
			fixed4 _Color;
			float4 _Alpha;
			half _Light;
			half _TexStr;
			half _TexStr2;

			SHADERDATA vert(float4 vertex:POSITION, float2 uv : TEXCOORD0, float2 uv2 : TEXCOORD0)
			{
				SHADERDATA vs;
				vs.position = UnityObjectToClipPos(vertex);
				vs.uv = TRANSFORM_TEX(uv, _MainTex);
				vs.uv2 = TRANSFORM_TEX(uv2, _SecTex);
				vs._ShadowCoord = ComputeScreenPos(vs.position);
				return vs;
			}

			float4 frag(SHADERDATA ps) : SV_TARGET
			{

			float4 col = _Color + (tex2D(_MainTex, ps.uv)*(_TexStr))*(tex2D(_SecTex, ps.uv2)*(_TexStr2));
			// 
			col = col + _Light * lerp(float4(0, 0, 0, 1),_Color, step(0.2, SHADOW_ATTENUATION(ps)));
			return col;
			}

			ENDCG
		}
			Pass
			{
				Name "ShadowCaster"
				Tags { "LightMode" = "ShadowCaster" }

				ZWrite On ZTest LEqual Cull Off
				Offset 1, 1

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_shadowcaster
				#pragma fragmentoption ARB_precision_hint_fastest
				#include "UnityCG.cginc"

				struct v2ff {
					V2F_SHADOW_CASTER;
				};

				v2ff vert(appdata_base v)
				{
					v2ff o;
					TRANSFER_SHADOW_CASTER(o)
					return o;
				}

				float4 frag(v2f i) : COLOR
				{
					SHADOW_CASTER_FRAGMENT(i)
				}
				ENDCG
			}Pass {
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
		}
}