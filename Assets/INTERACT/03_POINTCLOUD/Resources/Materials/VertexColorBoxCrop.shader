// Copyright (C) 2017 CEA LIST, DRT/DIASI/LSI
// Author: Jeremie Le Garrec
// jeremie_legarrec@hotmail.com

Shader "Custom/VertexColorBoxCrop" 
{
  Properties
  {
    _Alpha("Alpha", Range(0.0, 1.0)) = 0.0
  }
  SubShader
  {
    Pass
    {
      LOD 200

      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag

      #include "UnityCG.cginc"
      float _Alpha;
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
        half3 col : COLOR;
        float2 uv : TEXCOORD1;
      };

      VertexOutput vert(VertexInput v)
      {
        VertexOutput o;
        o.pos = UnityObjectToClipPos(v.v);
#ifdef UNITY_COLORSPACE_GAMMA
        o.col = v.color;
#else
        o.col = GammaToLinearSpace(v.color);
#endif

        if (!inside(mul(unity_ObjectToWorld, v.v)))
          o.uv = float2(-1.0f, -1.0f);
        else
          o.uv = float2(1.0f, 1.0f);
        return o;
      }

      half4 frag(VertexOutput o) : COLOR
      {
        if (_Alpha == 0.0f) clip(o.uv);
        if (o.uv[0] < 0.0f) return half4(1.0-_Alpha, 1.0-_Alpha, 1.0-_Alpha, 1.0);
        return half4(o.col, 1.0f);
      }

      ENDCG
    }
  }
}
