// Copyright (C) 2017 CEA LIST, DRT/DIASI/LSI
// Author: Jeremie Le Garrec
// jeremie_legarrec@hotmail.com


Shader "Custom/VertexColorAlpha" {
    SubShader {
    
    Tags { "Queue"="Transparent" "RenderType"="Opaque"}
		Blend SrcAlpha OneMinusSrcAlpha
    
    Pass {
        LOD 200
         
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
 
        struct VertexInput {
            float4 v : POSITION;
            float4 color: COLOR;
        };
         
        struct VertexOutput {
            float4 pos : SV_POSITION;
            float4 col : COLOR;
        };
         
        VertexOutput vert(VertexInput v) {
         
            VertexOutput o;
            o.pos = UnityObjectToClipPos(v.v);
            o.col = v.color;
             
            return o;
        }
         
        float4 frag(VertexOutput o) : COLOR {
            if(o.col[3]==0)
              return half4(1.0, 1.0, 1.0, 1.0f); // white
            else
              return o.col;
        }
 
        ENDCG
        } 
    }
 
}