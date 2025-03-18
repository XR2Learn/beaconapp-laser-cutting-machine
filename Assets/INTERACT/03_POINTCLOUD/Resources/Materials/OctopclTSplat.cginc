#include "UnityCG.cginc"
//#include "AutoLight.cginc"

// vert to geo
struct v2g
{
  float4 pos : SV_POSITION;
#if !OCTOPCL_SHADOW_CASTER
  half3  color : COLOR;
#endif
  float splat : TEXCOORD0;
};

// geo to frag
struct g2f
{
  float4 pos : SV_POSITION;
#if !OCTOPCL_SHADOW_CASTER
  half3  color : COLOR;
#endif
  float2 uv : TEXCOORD0;
#if OCTOPCL_BSP || OCTOPCL_BOX_SELECTION
  float4 worldpos : TEXCOORD1;
#endif
};

// Vars
uniform float4 _DiffuseTint;

#if OCTOPCL_COLOR_GRADIENT_Y
sampler2D _GradientTx;
uniform float _Height;
#endif

#if OCTOPCL_BSP
uniform float4 _planeEquation;
uniform int _cuttingOn;
#endif

uniform int _orthographic;

#if OCTOPCL_BOX_SELECTION
uniform half4 _selectionColorIn;
uniform half4 _selectionColorOut;
#endif

// Vertex modifier function
v2g Vertex(float4 vertex : POSITION, // vertex position input
           half3 color : COLOR)
{
  v2g o = (v2g)0;
  o.pos = vertex;
#if !OCTOPCL_SHADOW_CASTER
#ifdef UNITY_COLORSPACE_GAMMA
  o.color = color;
#else
  o.color = GammaToLinearSpace(color);
#endif
#endif

  o.splat = SplatSize(vertex);
  return o;
}

[maxvertexcount(3)]
void Geometry(point v2g p[1], inout TriangleStream<g2f> triStream)
{
  float3 upVector = mul(UNITY_MATRIX_T_MV,float4(0,1,0,0));
  float3 eyeVector;
  float3 rightVector;
  if(!_orthographic)
  {
	eyeVector = ObjSpaceViewDir(p[0].pos);
	rightVector = normalize(cross(eyeVector, upVector));
  }
  else
  {
	eyeVector = mul(UNITY_MATRIX_T_MV,float4(0,0,1,0));
	rightVector = mul(UNITY_MATRIX_T_MV,float4(1,0,0,0));
  }

  //float extend = p[0].splat * 1.73205080757f;
  //add extend to cover splat
  float extend = 1.5f * p[0].splat * 1.73205080757f;
  float4 v[3];
  v[0] = float4(p[0].pos - extend * rightVector - extend * 0.57735026919f * upVector, 1.0f);
  v[1] = float4(p[0].pos + extend * 1.15470053838f * upVector, 1.0f);
  v[2] = float4(p[0].pos + extend * rightVector - extend * 0.57735026919f * upVector, 1.0f);

  g2f pIn;
  UNITY_INITIALIZE_OUTPUT(g2f, pIn);
  pIn.pos = UnityObjectToClipPos(v[0]);
#if !OCTOPCL_SHADOW_CASTER
  pIn.color = p[0].color;
#endif
  pIn.uv = float2(-1.73205080757f, -1.0f);
#if OCTOPCL_BSP
  pIn.worldpos = mul(unity_ObjectToWorld, v[0]);
#endif
  triStream.Append(pIn);

  pIn.pos = UnityObjectToClipPos(v[1]);
#if !OCTOPCL_SHADOW_CASTER
  pIn.color = p[0].color;
#endif
  pIn.uv = float2(0.0f, 2.0f);
#if OCTOPCL_BSP
  pIn.worldpos = mul(unity_ObjectToWorld, v[1]);
#endif
  triStream.Append(pIn);

  pIn.pos = UnityObjectToClipPos(v[2]);
#if !OCTOPCL_SHADOW_CASTER
  pIn.color = p[0].color;
#endif
  pIn.uv = float2(1.73205080757f, -1.0f);
#if OCTOPCL_BSP
  pIn.worldpos = mul(unity_ObjectToWorld, v[2]);
#endif
  triStream.Append(pIn);
}

float4 Fragment(g2f i) : COLOR
{

#if !OCTOPCL_SHADOW_CASTER
#if OCTOPCL_COLOR_GRADIENT_Y && (OCTOPCL_BSP || OCTOPCL_BOX_SELECTION)
  float4 color = tex2D(_GradientTx, float2(i.worldpos.y / _Height, 0));
#else
  float4 color = float4(i.color, 1);
#endif
#endif

#if OCTOPCL_BSP
  if (_cuttingOn)
  {
    //clip(dot(i.worldpos, _planeEquation.xyz) - _planeEquation.w);
    float plane_dot = dot(i.worldpos, _planeEquation.xyz) - _planeEquation.w;
    clip(plane_dot);
#if !OCTOPCL_SHADOW_CASTER
    if (plane_dot < 0.004)
      color = float4(1,0,0,1);
#endif
  }
#endif

  clip(dot(i.uv,i.uv) > 1.0f ? -1 : 1);

#if OCTOPCL_BOX_SELECTION && !OCTOPCL_SHADOW_CASTER
  if (insideBox(i.worldpos))
    color *= _selectionColorIn;
  else 
    color *= _selectionColorOut;
#endif

#if !OCTOPCL_SHADOW_CASTER
  //float4 color = float4(i.color,1);
  return _DiffuseTint * color;
#else
  SHADOW_CASTER_FRAGMENT(i)
#endif

}