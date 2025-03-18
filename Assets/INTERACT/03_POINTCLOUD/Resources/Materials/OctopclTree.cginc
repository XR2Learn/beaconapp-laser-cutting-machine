// Copyright (C) 2017 CEA LIST, DRT/DIASI/LSI
// Author: Jeremie Le Garrec
// jeremie_legarrec@hotmail.com

struct node
{
	//float children;
	//float offset;
  uint children;
	uint offset;
};

// Uniforms
uniform float _Extend;
uniform StructuredBuffer<node> _Buffer;
uniform int _MinDepthSplatSize;

/**
* checks whether the bit at index is 1
* number is treated as if it were an integer in the range 0-255
*/
inline bool isBitSet(float number, float index)
{
	return fmod(floor(number / pow(2.0, index)), 2.0) != 0.0;
}

float getDepth(float4 pos)
{
  float3 offset = float3(0.0, 0.0, 0.0);
  float iOffset = 0.0;
  float depth = 0.0;
  UNITY_LOOP for(float i = 0.0; i <= 1000.0; i++)
  {
    float nodeSizeAtLevel = _Extend  / pow(2.0, i);
    float3 index3d = (pos.xyz - offset) / nodeSizeAtLevel;
    index3d = max(0, index3d);
    index3d = floor(index3d + 0.5);
    float index = 4.0*index3d.x + 2.0*index3d.y + index3d.z;
          
    float children = _Buffer[iOffset].children;
    if(isBitSet(children,index))
    {
      // there are more visible child nodes at this position
      iOffset = iOffset + _Buffer[iOffset].offset + index;
      depth++;
    }
    else
    {
      return depth;
    }
    offset = offset + (nodeSizeAtLevel * 0.5)*index3d;
  }
  return depth;
}

float SplatSize(float4 pos)
{
  return _Extend / ( pow(2.0, max(_MinDepthSplatSize, getDepth(pos))) * 128.0 * 2.);
}
