// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Copyright (C) 2018 CEA LIST, DRT/DIASI/LSI
// Author: Jeremie Le Garrec & Gilles Rougeron

Shader "Custom/VertexNormalColor" {
	Properties
	{
		_Color ("Main Color", Color) = (1,1,1,1)
	}
    SubShader 
	{
	Pass {
	    Tags{ "LightMode" = "ForwardBase" }

        LOD 200
         
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag

		#include "UnityCG.cginc"
		#include "AutoLight.cginc"
		uniform float4 _LightColor0;
 
        struct VertexInput {
            float4 v : POSITION;
			//float3 n : NORMAL;
            float4 color: COLOR;
        };
         
        struct VertexOutput {
            float4 pos : SV_POSITION;
            float4 col : COLOR;
        };
         
        VertexOutput vert(VertexInput v) {
         
            VertexOutput o;
            o.pos = UnityObjectToClipPos(v.v);
			o.col = float4(0.5,0.5,0.5, 1.0);
            return o;
        }
         
        float4 frag(VertexOutput o) : COLOR {
            return saturate(o.col);
        }
 
        ENDCG
    } 
    Pass {
	    Tags{ "LightMode" = "ForwardAdd" }

        LOD 200
         
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag

		#include "UnityCG.cginc"
		#include "AutoLight.cginc"
		uniform float4 _LightColor0;
		// User-specified properties
        uniform float4 _Color; 
 
        struct VertexInput {
            float4 v : POSITION;
			float3 n : NORMAL;
            float4 color: COLOR;
        };
         
        struct VertexOutput {
            float4 pos : SV_POSITION;
            float4 color : COLOR;
        };
         
        VertexOutput vert(VertexInput v) {
         
            VertexOutput o;
			o.pos = UnityObjectToClipPos(v.v);
			float NdotL;
			//if(_WorldSpaceLightPos0.w == 0)
			//{
			//	NdotL = 0.0;
			//}
			//else
			//{
				float3 l_dir = normalize(ObjSpaceLightDir(v.v));
				NdotL = abs(dot(v.n, l_dir));
			//}
 
			float3 d = NdotL * _LightColor0 * _Color;
			o.color = float4(d, 1.0);
            return o;
        }
         
        float4 frag(VertexOutput o) : COLOR {
            return saturate(o.color);
        }
 
        ENDCG
        } 
    }
 
}