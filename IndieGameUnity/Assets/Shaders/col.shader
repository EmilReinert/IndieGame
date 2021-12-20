Shader "Emil/no shadows"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_ShadowColor("Shadow Color", Color) = (0,0,0,1)
		_Light("Light", Range(0,1)) = 0.5
		_MainTex("Texture", 2D) = "white" {}
		_TexStr("Texture Strength", Range(0,1)) = 0.5
		_SecTex("Texture", 2D) = "white" {}
		_TexStr2("Texture Strength", Range(0,1)) = 0.5
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 100

			Cull off


			Pass
			{
			Tags {"LightMode" = "ForwardBase"}
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
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
			fixed4 _ShadowColor;
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

			float4 col = _Color * (tex2D(_MainTex, ps.uv)*(_TexStr)) + (tex2D(_SecTex, ps.uv2)*(_TexStr2));
			// 
			col = col + _Light * lerp(_ShadowColor,_Color, step(0.2, SHADOW_ATTENUATION(ps)));
			return col;
			}

			ENDCG
		}
			
		}
}