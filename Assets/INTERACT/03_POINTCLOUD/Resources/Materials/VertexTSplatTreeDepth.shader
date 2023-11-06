// Copyright (C) 2017 CEA LIST, DRT/DIASI/LSI
// Author: Jeremie Le Garrec
// jeremie_legarrec@hotmail.com

Shader "Custom/VertexTSplatTreeDepth" 
{
  Properties
  {
	  _DiffuseTint("Diffuse Tint", Color) = (1, 1, 1, 1)
    _Extend("Octree Size", float) = 1
    _MinDepthSplatSize("Min Depth Splat Size", int) = 0
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
      #pragma multi_compile_local __ OCTOPCL_CONE
	    #pragma multi_compile_local __ OCTOPCL_COLOR_GRADIENT_Y   
      #pragma multi_compile_local __ OCTOPCL_BOX_SELECTION

      #define OCTOPCL_SHADOW_CASTER 0
    
#ifdef OCTOPCL_BOX_SELECTION
      #include "OctopclBoxSelection.cginc"
#endif

      #include "OctopclTree.cginc"
#ifndef OCTOPCL_CONE 
      #include "OctopclTSplat.cginc"
#else
      #include "OctopclTSplatCone.cginc"
#endif

      ENDCG
    }
    Pass 
    {
      Name "ShadowCaster"
      Tags{ "RenderType" = "Opaque" "Queue" = "Geometry"}
      Tags{ "LightMode" = "ShadowCaster" }

      Fog {Mode Off}
      ZWrite On ZTest LEqual Cull Off
      Offset 1, 1
 
      CGPROGRAM
      #pragma vertex Vertex
      #pragma geometry Geometry
      #pragma fragment Fragment
      #pragma multi_compile_shadowcaster
      #pragma fragmentoption ARB_precision_hint_fastest
      #pragma multi_compile_fwdbase
      #pragma multi_compile_local __ OCTOPCL_BSP
      #pragma multi_compile_local __ OCTOPCL_CONE
      
      #define OCTOPCL_SHADOW_CASTER 1

      #include "OctopclTree.cginc"
#ifndef OCTOPCL_CONE 
      #include "OctopclTSplat.cginc"
#else
      #include "OctopclTSplatCone.cginc"
#endif
      
      ENDCG
    }
  }
}
