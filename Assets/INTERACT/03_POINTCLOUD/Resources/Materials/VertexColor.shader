// Copyright (C) 2017 CEA LIST, DRT/DIASI/LSI
// Author: Jeremie Le Garrec
// jeremie_legarrec@hotmail.com


Shader "Custom/VertexColor" 
{
  SubShader 
  {
    Pass 
    {
      LOD 200
         
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #pragma multi_compile_local __ OCTOPCL_BSP
      #pragma multi_compile_local __ OCTOPCL_COLOR_GRADIENT_Y  
      #pragma multi_compile_local __ OCTOPCL_BOX_SELECTION

      #include "UnityCG.cginc"

#ifdef OCTOPCL_BOX_SELECTION
      #include "OctopclBoxSelection.cginc"
#endif

#if OCTOPCL_BSP
      uniform float4 _planeEquation;
      uniform int _cuttingOn;
#endif
#if OCTOPCL_COLOR_GRADIENT_Y
      sampler2D _GradientTx;
      uniform float _Height;
#endif

#if OCTOPCL_BOX_SELECTION
      uniform half4 _selectionColorIn;
      uniform half4 _selectionColorOut;
#endif

      struct VertexInput 
      {
        float4 v : POSITION;
        half3 color: COLOR;
      };
         
      struct VertexOutput 
      {
        float4 pos : SV_POSITION;
        half3 col : COLOR;
#if OCTOPCL_BSP || OCTOPCL_BOX_SELECTION
        float4 worldpos : TEXCOORD1;
#endif
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

#if OCTOPCL_BSP || OCTOPCL_BOX_SELECTION
        o.worldpos = mul(unity_ObjectToWorld, v.v);
#endif
        return o;
      }
         
      half4 frag(VertexOutput o) : COLOR
      {

#if OCTOPCL_COLOR_GRADIENT_Y && (OCTOPCL_BSP || OCTOPCL_BOX_SELECTION)
        half4 color = tex2D(_GradientTx, float2(o.worldpos.y / _Height, 0));
#else
        half4 color = half4(o.col, 1.0f);
#endif

#if OCTOPCL_BSP
        if (_cuttingOn)
          clip(dot(o.worldpos, _planeEquation.xyz) - _planeEquation.w);
#endif
#if OCTOPCL_BOX_SELECTION && !OCTOPCL_SHADOW_CASTER
        if (insideBox(o.worldpos))
          color *= _selectionColorIn;
        else
          color *= _selectionColorOut;
#endif
        return color;
      }
      ENDCG
    } 
  }
}