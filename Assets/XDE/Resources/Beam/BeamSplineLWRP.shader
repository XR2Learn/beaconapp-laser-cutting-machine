

Shader "Lightweight Render Pipeline/Beam/Spline"
{

  Properties
  {
    [MainColor] _BaseColor("Color", Color) = (0.5,0.5,0.5,1)
    [MainTexture] _BaseMap("Albedo", 2D) = "white" {}

  _Smoothness("Smoothness", Range(0.0, 1.0)) = 0.5
    _GlossMapScale("Smoothness Scale", Range(0.0, 1.0)) = 1.0
    _SpecColor("Specular", Color) = (0.2, 0.2, 0.2)

  }
    SubShader
  {
    Tags{ "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }
    LOD 200
    // ------------------------------------------------------------------
    // Forward pass. Shades GI, emission, fog and all lights in a single pass.
    // Compared to Builtin pipeline forward renderer, LWRP forward renderer will
    // render a scene with multiple lights with less drawcalls and less overdraw.
    Pass
  {
    // "Lightmode" tag must be "LightweightForward" or not be defined in order for
    // to render objects.
    Name "StandardLit"
    Tags{ "LightMode" = "UniversalForward" }
    Cull Front

    HLSLPROGRAM

#pragma prefer_hlslcc gles
#pragma require geometry
#pragma exclude_renderers d3d11_9x
    // Use shader model 3.0 target, to get nicer looking lighting
#pragma target 3.0

    // -------------------------------------
    // Material Keywords
    // unused shader_feature variants are stripped from build automatically

    // -------------------------------------
    // Lightweight Render Pipeline keywords
    // When doing custom shaders you most often want to copy and past these #pragmas
    // These multi_compile variants are stripped from the build depending on:
    // 1) Settings in the LWRP Asset assigned in the GraphicsSettings at build time
    // e.g If you disable AdditionalLights in the asset then all _ADDITIONA_LIGHTS variants
    // will be stripped from build
    // 2) Invalid combinations are stripped. e.g variants with _MAIN_LIGHT_SHADOWS_CASCADE
    // but not _MAIN_LIGHT_SHADOWS are invalid and therefore stripped.
#pragma multi_compile _ _MAIN_LIGHT_SHADOWS
#pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
#pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
#pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
#pragma multi_compile _ _SHADOWS_SOFT
#pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE


    // -------------------------------------
    // Unity defined keywords
#pragma multi_compile _ DIRLIGHTMAP_COMBINED
#pragma multi_compile _ LIGHTMAP_ON


#pragma vertex LitPassVertex
#pragma geometry LitPassGeometry
#pragma fragment LitPassFragment

    // Including the following two function is enought for shading with Lightweight Pipeline. Everything is included in them.
    // Core.hlsl will include SRP shader library, all constant buffers not related to materials (perobject, percamera, perframe).
    // It also includes matrix/space conversion functions and fog.
    // Lighting.hlsl will include the light functions/data to abstract light constants. You should use GetMainLight and GetLight functions
    // that initialize Light struct. Lighting.hlsl also include GI, Light BDRF functions. It also includes Shadows.

    // Required by all Lightweight Render Pipeline shaders.
    // It will include Unity built-in shader variables (except the lighting variables)
    // (https://docs.unity3d.com/Manual/SL-UnityShaderVariables.html
    // It will also include many utilitary functions. 

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

    // Include this if you are doing a lit shader. This includes lighting shader variables,
    // lighting and shadow functions
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

    // Include this if you are doing a lit shader. This includes lighting shader variables,
    // lighting and shadow functions
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
    // Material shader variables are not defined in SRP or LWRP shader library.
    // This means _BaseColor, _BaseMap, _BaseMap_ST, and all variables in the Properties section of a shader
    // must be defined by the shader itself. If you define all those properties in CBUFFER named
    // UnityPerMaterial, SRP can cache the material properties between frames and reduce significantly the cost
    // of each drawcall.
    // In this case, for sinmplicity LitInput.hlsl is included. This contains the CBUFFER for the material
    // properties defined above. As one can see this is not part of the ShaderLibrary, it specific to the
    // LWRP Lit shader.
#include "Packages/com.unity.render-pipelines.universal/Shaders/LitInput.hlsl"

    uniform float _Radius = 0.01f;
  uniform float _Wrap = 1.0f;
  struct Attributes
  {
    float4 positionOS   : POSITION;
    float3 normalOS     : NORMAL;
    float2 uv           : TEXCOORD0;
    uint id             : SV_VertexID;
  };
  struct GS_INPUT //input of geom shader
  {
    float4	pos		: POSITION;
    float3	normal	: NORMAL; //encodes direction Y
    float3  tangent : TANGENT; //encodes direction Z
                               //will encode the direction...
    float2  tex0	: TEXCOORD0;

  };
  struct FS_INPUT
  {
    float4 positionCS               : SV_POSITION;
    float2 uv                       : TEXCOORD0;
    float3 positionWS               : TEXCOORD1; // xyz: positionWS, w: vertex fog factor
    half3  normalWS                 : TEXCOORD2;

#ifdef _MAIN_LIGHT_SHADOWS
    float4 shadowCoord              : TEXCOORD4; // compute shadow coord per-vertex for the main light
#endif

  };
  struct BeamPoint //compute buffer
  {
    float3 pos;
    float3 coY;
    float3 coZ;
  };
#ifdef SHADER_API_D3D11
  StructuredBuffer<BeamPoint> _BeamBuffer; //contains beam information
#endif
                                           //this only multiply values into world space prior geometry step
  GS_INPUT LitPassVertex(Attributes input)
  {
    GS_INPUT output = (GS_INPUT)0;
    BeamPoint bp = _BeamBuffer[input.id];

    output.pos = mul(unity_ObjectToWorld, float4(bp.pos, 1.0f));
    output.normal = mul(unity_ObjectToWorld, float4(bp.coY, 0.0f)).xyz;
    output.tangent = mul(unity_ObjectToWorld, float4(bp.coZ, 0.0f)).xyz;
    output.tex0 = float2(0, 0);

    return output;
  }
  // Geometry Shader -----------------------------------------------------
  [maxvertexcount(18)]
  void LitPassGeometry(line GS_INPUT p[2], inout TriangleStream<FS_INPUT> triStream)
  {

    //we consider the sequence P --- 0 ---- 1 ---- E
    float3 P0 = p[0].pos.xyz;
    float3 P1 = p[1].pos.xyz;
    float3 V10 = P1 - P0;
    float3 T = normalize(V10);

    float3 Y0 = p[0].normal;
    float3 Z0 = p[0].tangent.xyz;

    float3 Y1 = p[1].normal;
    float3 Z1 = p[1].tangent.xyz;

    float aY0 = abs(dot(Y0, T));
    float Yf0 = pow(1.0 + aY0*aY0, 0.5);
    float aZ0 = abs(dot(Z0, T));
    float Zf0 = pow(1.0 + aZ0*aZ0, 0.5);

    float aY1 = abs(dot(Y1, T));
    float Yf1 = pow(1.0 + aY0*aY0, 0.5);
    float aZ1 = abs(dot(Z1, T));
    float Zf1 = pow(1.0 + aZ1*aZ1, 0.5);

    //registering angles
    //float PI3 = 1.0471975512;
    const float PI4 = 0.78539816339;
    const float deltaV = _Wrap * 0.125f;

    float2 angles[9];
    for (float a = 0; a< 8; a++)
    {
      angles[a] = float2(cos(PI4 * a), sin(PI4 *a));
    }
    angles[8] = angles[0];


    //float4x4 ump = UNITY_MATRIX_MVP;
    //float4x4 uwo = unity_WorldToObject;
    //float4x4 vp = mul(ump, uwo);
    FS_INPUT pIn;

    for (int i = 0; i< 9; i++)
    {
      float3 N0 = angles[i].x * Yf0 * Y0 + angles[i].y * Zf0 * Z0;
      float3 N1 = angles[i].x * Yf1 * Y1 + angles[i].y * Zf1 * Z1;

      float4 positionOS = float4(P0 + _Radius * N0, 1.0f);
      VertexPositionInputs vertexInput = GetVertexPositionInputs(positionOS.xyz);
      pIn.uv = float2(p[0].tex0.x, i * deltaV);
      pIn.normalWS = N0;
      pIn.positionCS = vertexInput.positionCS;
      pIn.positionWS = vertexInput.positionWS;
#ifdef _MAIN_LIGHT_SHADOWS
      pIn.shadowCoord = GetShadowCoord(vertexInput);
#endif
      triStream.Append(pIn);

      positionOS = float4(P1 + _Radius * N1, 1.0f);
      vertexInput = GetVertexPositionInputs(positionOS.xyz);
      pIn.uv = float2(p[1].tex0.x, i * deltaV);
      pIn.normalWS = N1;
      pIn.positionCS = vertexInput.positionCS;
      pIn.positionWS = vertexInput.positionWS;
#ifdef _MAIN_LIGHT_SHADOWS
      pIn.shadowCoord = GetShadowCoord(vertexInput);
#endif
      triStream.Append(pIn);
    }
  }
  half4 LitPassFragment(FS_INPUT input) : SV_Target
  {
    // Surface data contains albedo, metallic, specular, smoothness, occlusion, emission and alpha
    // InitializeStandarLitSurfaceData initializes based on the rules for standard shader.
    // You can write your own function to initialize the surface data of your shader.
    SurfaceData surfaceData;
  InitializeStandardLitSurfaceData(input.uv, surfaceData);

  half3 normalWS = input.normalWS;
  normalWS = normalize(normalWS);

  // Samples SH fully per-pixel. SampleSHVertex and SampleSHPixel functions
  // are also defined in case you want to sample some terms per-vertex.
  half3 bakedGI = SampleSH(normalWS);

  float3 positionWS = input.positionWS;
  half3 viewDirectionWS = SafeNormalize(GetCameraPositionWS() - positionWS);

  // BRDFData holds energy conserving diffuse and specular material reflections and its roughness.
  // It's easy to plugin your own shading fuction. You just need replace LightingPhysicallyBased function
  // below with your own.
  BRDFData brdfData;
  InitializeBRDFData(surfaceData.albedo, surfaceData.metallic, surfaceData.specular, surfaceData.smoothness, surfaceData.alpha, brdfData);

  // Light struct is provide by LWRP to abstract light shader variables.
  // It contains light direction, color, distanceAttenuation and shadowAttenuation.
  // LWRP take different shading approaches depending on light and platform.
  // You should never reference light shader variables in your shader, instead use the GetLight
  // funcitons to fill this Light struct.
#ifdef _MAIN_LIGHT_SHADOWS
  // Main light is the brightest directional light.
  // It is shaded outside the light loop and it has a specific set of variables and shading path
  // so we can be as fast as possible in the case when there's only a single directional light
  // You can pass optionally a shadowCoord (computed per-vertex). If so, shadowAttenuation will be
  // computed.
  Light mainLight = GetMainLight(input.shadowCoord);
#else
  Light mainLight = GetMainLight();
#endif

  // Mix diffuse GI with environment reflections.
  half3 color = GlobalIllumination(brdfData, bakedGI, surfaceData.occlusion, normalWS, viewDirectionWS);

  // LightingPhysicallyBased computes direct light contribution.
  color += LightingPhysicallyBased(brdfData, mainLight, normalWS, viewDirectionWS);

  // Additional lights loop
#ifdef _ADDITIONAL_LIGHTS

  // Returns the amount of lights affecting the object being renderer.
  // These lights are culled per-object in the forward renderer
  int additionalLightsCount = GetAdditionalLightsCount();
  for (int i = 0; i < additionalLightsCount; ++i)
  {
    // Similar to GetMainLight, but it takes a for-loop index. This figures out the
    // per-object light index and samples the light buffer accordingly to initialized the
    // Light struct. If _ADDITIONAL_LIGHT_SHADOWS is defined it will also compute shadows.
    Light light = GetAdditionalLight(i, positionWS);

    // Same functions used to shade the main light.
    color += LightingPhysicallyBased(brdfData, light, normalWS, viewDirectionWS);
  }
#endif

  return half4(color,1.0);
  }
    ENDHLSL
  }

    // Used for rendering shadowmaps
    UsePass "Universal Render Pipeline/Lit/ShadowCaster"

    // Used for depth prepass
    // If shadows cascade are enabled we need to perform a depth prepass. 
    // We also need to use a depth prepass in some cases camera require depth texture
    // (e.g, MSAA is enabled and we can't resolve with Texture2DMS
    UsePass "Universal Render Pipeline/Lit/DepthOnly"

    // Used for Baking GI. This pass is stripped from build.
    UsePass "Universal Render Pipeline/Lit/Meta"
  }

    FallBack "Hidden/InternalErrorShader"

}