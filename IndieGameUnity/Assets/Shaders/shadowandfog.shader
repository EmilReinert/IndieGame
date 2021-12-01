Shader "Emil/shadow and fog"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Texture", 2D) = "white" {}
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
		// make fog work
		#pragma multi_compile_fog
	//make shadows work
	#pragma multi_compile_fwdbase

	#include "AutoLight.cginc"
#include "UnityCG.cginc"


		struct SHADERDATA
		{
		float2 uv : TEXCOORD0;
			float4 _ShadowCoord : TEXCOORD1;
			float4 position : SV_POSITION;
		};


		sampler2D _MainTex;
		float4 _MainTex_ST;
		fixed4 _Color;
		float4 _Alpha;
		SHADERDATA vert(float4 vertex:POSITION, float2 uv : TEXCOORD0)
		{
			SHADERDATA vs;
			vs.position = UnityObjectToClipPos(vertex);
			vs.uv = TRANSFORM_TEX(uv, _MainTex);
			vs._ShadowCoord = ComputeScreenPos(vs.position);
			return vs;
		}

		float4 frag(SHADERDATA ps) : SV_TARGET
		{

		float4 col = (tex2D(_MainTex, ps.uv)*(_Color.a))* _Color + _Color;
		// 
		col = col + 0.5* lerp(float4(0, 0, 0, 1),_Color, step(0.2, SHADOW_ATTENUATION(ps)));
		return col + (col * 0.5);
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

			struct v2f {
				V2F_SHADOW_CASTER;
			};

			v2f vert(appdata_base v)
			{
				v2f o;
				TRANSFER_SHADOW_CASTER(o)
				return o;
			}

			float4 frag(v2f i) : COLOR
			{
				SHADOW_CASTER_FRAGMENT(i)
			}
			ENDCG
		
	}
	}
}