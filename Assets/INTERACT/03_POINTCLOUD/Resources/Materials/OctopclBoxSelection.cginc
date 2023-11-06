// Copyright (C) 2020 CEA LIST, DRT/DIASI/LSI
// Author: Jeremie Le Garrec
// jeremie_legarrec@hotmail.com
#include "UnityCG.cginc"

// Vars
float3 _BoxOrigin;
float3 _BoxTop;
float4x4 _BoxWorld;

bool insideBox(fixed3 pos)
{
  float3 origin_world = mul(_BoxWorld, float4(_BoxOrigin, 1));
  float3 top_world = mul(_BoxWorld, float4(_BoxTop, 1));

  float dot_0 = dot(pos - origin_world, mul(_BoxWorld, float4(-1, 0, 0, 0)));
  float dot_1 = dot(pos - origin_world, mul(_BoxWorld, float4(0, -1, 0, 0)));
  float dot_2 = dot(pos - origin_world, mul(_BoxWorld, float4(0, 0, -1, 0)));
  float dot_3 = dot(pos - top_world, mul(_BoxWorld, float4(1, 0, 0, 0)));
  float dot_4 = dot(pos - top_world, mul(_BoxWorld, float4(0, 1, 0, 0)));
  float dot_5 = dot(pos - top_world, mul(_BoxWorld, float4(0, 0, 1, 0)));
  return dot_0 < 0 && dot_1 < 0 && dot_2 < 0 && dot_3 < 0 && dot_4 < 0 && dot_5 < 0;
}
