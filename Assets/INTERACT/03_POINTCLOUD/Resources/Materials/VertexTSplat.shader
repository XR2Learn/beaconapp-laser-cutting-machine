// Copyright (C) 2017 CEA LIST, DRT/DIASI/LSI
// Author: Jeremie Le Garrec
// jeremie_legarrec@hotmail.com

Shader "Custom/VertexTSplat" 
{
  Properties
  {
	  _DiffuseTint("Diffuse Tint", Color) = (1, 1, 1, 1)
    _SplatSize("Splat Size", float) = 0.005
    _FocalLength("Focal Length", Float) = 1.
  }
  SubShader
  {
    Tags{ "DisableBatching" = "True" }
    Pass
	  {
      Tags{ "RenderType" = "Opaque" "Queue" = "Geometry" }
      Tags{ "LightMode" = "ForwardBase" }
	    ZWrite On

      LOD 100

      CGPROGRAM
      #pragma target 5.0
      #pragma fragmentoption ARB_precision_hint_fastest

      #pragma vertex Vertex
      #pragma geometry Geometry
      #pragma fragment Fragment
      #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
      #pragma multi_compile_local __ OCTOPCL_BSP
      #pragma multi_compile_local __ OCTOPCL_ADAPTATIVE_SPLAT
      #pragma multi_compile_local __ OCTOPCL_CONE
      #pragma multi_compile_local __ OCTOPCL_COLOR_GRADIENT_Y
      #pragma multi_compile_local __ OCTOPCL_BOX_SELECTION

#ifdef OCTOPCL_BOX_SELECTION
      #include "OctopclBoxSelection.cginc"
#endif

      #include "OctopclSplatSize.cginc"

#ifndef OCTOPCL_CONE 
      #include "OctopclTSplat.cginc"
#else
      #include "OctopclTSplatCone.cginc"
#endif

      ENDCG
    }
  }

  //FallBack "Diffuse"
}
