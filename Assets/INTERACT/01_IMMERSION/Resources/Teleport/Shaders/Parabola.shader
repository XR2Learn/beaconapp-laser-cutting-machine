// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Parabola Shader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Color) = (1, 1, 1, 1)
		_BackColor ("Color Behind Objects", Color) = (0.5,0.5,0.5,1)
		_Glow("Intensity", Range(0, 3)) = 1
		_Alpha ("Total Alpha (Transparency)", Float) = 1.0
	}
	SubShader
	{
		Tags{ "Queue" = "Overlay" "IgnoreProjector" = "True" "RenderType" = "Transparent" }

		CGINCLUDE
		#pragma vertex vert
		#pragma fragment frag
		#pragma multi_compile_instancing

		#include "UnityCG.cginc"

		struct appdata
		{
			float4 vertex : POSITION;
			float2 uv : TEXCOORD0;
			UNITY_VERTEX_INPUT_INSTANCE_ID
		};

		struct v2f
		{
			float2 uv : TEXCOORD0;
			float4 vertex : SV_POSITION;
			UNITY_VERTEX_OUTPUT_STEREO
		};

		sampler2D _MainTex;
		fixed4 _Color;
		fixed4 _BackColor;
		fixed _Alpha;
		half _Glow;

		v2f vert(appdata v)
		{
			v2f o;

			UNITY_SETUP_INSTANCE_ID(v);
			UNITY_INITIALIZE_OUTPUT(v2f, o);
			UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

			o.vertex = UnityObjectToClipPos(v.vertex);
			o.uv = v.uv;
			return o;
		}
		ENDCG

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			ZTest Greater
			ZWrite Off

			CGPROGRAM
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv) * _BackColor;
				col.a *= _Alpha;
				return col;
			}
			ENDCG
		}

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			ZTest LEqual
			ZWrite Off

			CGPROGRAM
			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv) * _Color;
				col *= _Glow;
				col.a *= _Alpha;
				return col;
			}
			ENDCG
		}
	}
}
