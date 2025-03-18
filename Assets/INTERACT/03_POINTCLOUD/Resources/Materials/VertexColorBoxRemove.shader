// Copyright (C) 2017 CEA LIST, DRT/DIASI/LSI
// Author: Jeremie Le Garrec
// jeremie_legarrec@hotmail.com

Shader "Custom/VertexColorBoxRemove" 
{
  Properties
  {
    _SelectColor("SelectColor", Color) = (1,0,0,1)
  }
  SubShader
  {
    Pass
    {
      LOD 200

      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #pragma fragmentoption ARB_precision_hint_fastest

      #include "UnityCG.cginc"

      half4 _SelectColor;
      float3 _Origin;
      float3 _Top;
      float4x4 _BoxWorld;
      
      bool inside(fixed3 pos)
      {
        float3 origin_world = mul(_BoxWorld, float4(_Origin, 1));
        float3 top_world = mul(_BoxWorld, float4(_Top, 1));

        float dot_0 = dot(pos - origin_world, mul(_BoxWorld, float4(-1, 0, 0, 0)));
        float dot_1 = dot(pos - origin_world, mul(_BoxWorld, float4(0, -1, 0, 0)));
        float dot_2 = dot(pos - origin_world, mul(_BoxWorld, float4(0, 0, -1, 0)));
        float dot_3 = dot(pos - top_world, mul(_BoxWorld, float4(1, 0, 0, 0)));
        float dot_4 = dot(pos - top_world, mul(_BoxWorld, float4(0, 1, 0, 0)));
        float dot_5 = dot(pos - top_world, mul(_BoxWorld, float4(0, 0, 1, 0)));
        return dot_0 < 0 && dot_1 < 0 && dot_2 < 0 && dot_3 < 0 && dot_4 < 0 && dot_5 < 0;
      }

      struct VertexInput
      {
        float4 v : POSITION;
        half3 color: COLOR;
      };

      struct VertexOutput
      {
        float4 pos : SV_POSITION;
        half4 col : COLOR;
      };

      VertexOutput vert(VertexInput v)
      {
        VertexOutput o;
        o.pos = UnityObjectToClipPos(v.v);
#ifdef UNITY_COLORSPACE_GAMMA
        o.col = half4(v.color,1.);
#else
        o.col = half4(GammaToLinearSpace(v.color), 1.);
#endif

        if (inside(mul(unity_ObjectToWorld, v.v)))
          o.col = _SelectColor;
        return o;
      }

      half4 frag(VertexOutput o) : COLOR
      {
        return o.col;
      }

      ENDCG
    }
  }
}
