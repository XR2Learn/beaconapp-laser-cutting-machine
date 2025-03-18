// Copyright (C) 2017 CEA LIST, DRT/DIASI/LSI
// Author: Jeremie Le Garrec
// jeremie_legarrec@hotmail.com
#include "UnityCG.cginc"

// Vars
uniform float _SplatSize;
uniform float _FocalLength;

float SplatSize(float4 pos)
{
#if OCTOPCL_ADAPTATIVE_SPLAT
  float dist = distance(_WorldSpaceCameraPos, mul(unity_ObjectToWorld, pos));
  return max(_SplatSize, 0.05*_SplatSize*dist/_FocalLength);
#else
  return _SplatSize;
#endif
}