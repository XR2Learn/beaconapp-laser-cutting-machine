
Shader "Beam/SPline"
{
  Properties {
    _Color ("Color", Color) = (1,1,1,1)
    _MainTex ("Albedo (RGB)", 2D) = "white" {}
    _Radius ("Radius", Range(0,1)) = 0.05
    _Wrap ("Wrap", Range(1,10)) = 2.0
  }
  SubShader 
  {
    Pass
    {
      Tags { "RenderType"="Opaque" "LightMode" = "ForwardBase"}
      Cull Off

      LOD 200	
      CGPROGRAM
#pragma target 5.0
#pragma vertex VS_Main
#pragma fragment FS_Main
#pragma geometry GS_Main
#pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight

#include "UnityCG.cginc" 
#include "UnityLightingCommon.cginc" // for _LightColor0
#include "AutoLight.cginc" //shadows

        // **************************************************************
        // Data structures												*
        // **************************************************************
    struct appdata_b
    {
      float4 vertex : POSITION;
      uint id : SV_VertexID;
      uint inst : SV_InstanceID;
    };       
    struct BeamPoint //compute buffer
    {
        float3 pos;
        float3 coY;
        float3 coZ;
    };
    struct GS_INPUT //input of geom shader
    {
    float4	pos		: POSITION;
    float3	normal	: NORMAL; //encodes direction Y
    float3  tangent : TANGENT; //encodes direction Z
    //will encode the direction...
    float2  tex0	: TEXCOORD0;

    };
	struct  VS_INPUT
	{
		float4 vertex : POSITION;
	};

    struct FS_INPUT
    {
    float4	pos		: POSITION;
    float4 diffuse : COLOR;
    float2  tex0	: TEXCOORD0;
    SHADOW_COORDS(1)
    };


      // **************************************************************
      // Vars															*
      // **************************************************************

      uniform float _Radius;
      uniform float _Wrap;
      float4x4 _VP;
      uniform float4 _Color;
      sampler2D _MainTex;
  
    #ifdef SHADER_API_D3D11
    StructuredBuffer<BeamPoint> _BeamBuffer; //contains beam information
    #endif
    

      // **************************************************************
      // Light											*
      // **************************************************************

      float4 Diffuse(float3 N)
      {
        half nl = max(0, dot(normalize(N), _WorldSpaceLightPos0.xyz));
        return  nl * _LightColor0;
      }
      
    // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
    // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
    // #pragma instancing_options assumeuniformscaling
    //UNITY_INSTANCING_BUFFER_START(Props)
    // put more per-instance properties here
    //UNITY_INSTANCING_BUFFER_END(Props)

      // **************************************************************
      // Shader Programs												*
      // **************************************************************

      // Vertex Shader ------------------------------------------------
      GS_INPUT VS_Main(appdata_b v)
      {
        GS_INPUT output = (GS_INPUT)0;
        BeamPoint bp = _BeamBuffer[v.id];
        
        output.pos = mul(unity_ObjectToWorld, float4(bp.pos, 1.0f));
        output.normal = mul(unity_ObjectToWorld, float4(bp.coY, 0.0f));
        output.tangent = mul(unity_ObjectToWorld, float4(bp.coZ, 0.0f));
        output.tex0 = float2(0,0);

        return output;
      }

      // Geometry Shader -----------------------------------------------------
      [maxvertexcount(18)]
      void GS_Main(line GS_INPUT p[2], inout TriangleStream<FS_INPUT> triStream)
      {
    
        //we consider the sequence P --- 0 ---- 1 ---- E
        float3 P0 = p[0].pos;
        float3 P1 = p[1].pos;
        float3 V10 = P1-P0;
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
          angles[a] = float2(cos(PI4 * a), sin (PI4 *a ));
        }
        angles[8] = angles[0];


        float4x4 ump = UNITY_MATRIX_MVP;
        float4x4 uwo = unity_WorldToObject;
        float4x4 vp = mul(ump, uwo);
		VS_INPUT v;
        FS_INPUT pIn;

        for (int i = 0; i< 9; i++)
        {
          float3 N0 = angles[i].x * Yf0 * Y0 + angles[i].y * Zf0 * Z0;
          float3 N1 = angles[i].x * Yf1 * Y1 + angles[i].y * Zf1 * Z1;

		  v.vertex = float4(P0 + _Radius * N0, 1.0f);
          pIn.tex0 = float2(p[0].tex0.x, i * deltaV);
          pIn.diffuse = Diffuse(N0);
          pIn.pos = mul(vp, v.vertex);
          TRANSFER_SHADOW(pIn)
          triStream.Append(pIn);

		  v.vertex = float4(P1 + _Radius * N1, 1.0f);
          pIn.tex0 = float2(p[1].tex0.x, i * deltaV);
          pIn.diffuse = Diffuse(N1);
          pIn.pos = mul(vp, v.vertex);
          TRANSFER_SHADOW(pIn)
          triStream.Append(pIn);
        }
      }
      // Fragment Shader -----------------------------------------------
      float4 FS_Main(FS_INPUT input) : COLOR
      {
        float4 diffuse = input.diffuse;
        fixed4 tcol = tex2D(_MainTex, input.tex0);
        fixed shadow = SHADOW_ATTENUATION(input);
        return _Color * tcol * (shadow * diffuse +unity_AmbientSky);
      }
      ENDCG
    }

    Pass
    {
      Tags { "RenderType"="Opaque" "LightMode" = "ShadowCaster"}
      Cull Off

      LOD 200	
      CGPROGRAM
#pragma target 5.0
#pragma vertex VS_Main
#pragma fragment FS_Main
#pragma geometry GS_Main
#include "UnityCG.cginc" 

        // **************************************************************
        // Data structures												*
        // **************************************************************
        
    struct appdata_b
    {
      float4 vertex : POSITION;
      uint id : SV_VertexID;
      uint inst : SV_InstanceID;
    };       
    struct BeamPoint //compute buffer
    {
        float3 pos;
        float3 coY;
        float3 coZ;
    };
      struct GS_INPUT
      {
        float4	pos		: POSITION;
        float3	normal	: NORMAL; //encodes direction Y
        float3  tangent : TANGENT; //encodes direction Z
        
      };

      struct FS_INPUT
      {
        float4	pos		: POSITION;
      };
      // **************************************************************
      // Vars															*
      // **************************************************************

      uniform float _Radius;
      uniform float _Wrap;
      float4x4 _VP;
#ifdef SHADER_API_D3D11
      StructuredBuffer<BeamPoint> _BeamBuffer; //contains actual 
#endif

      // **************************************************************
      // Shader Programs												*
      // **************************************************************

      // Vertex Shader ------------------------------------------------
      GS_INPUT VS_Main(appdata_b v)
      {       
        GS_INPUT output = (GS_INPUT)0;
        BeamPoint bp = _BeamBuffer[v.id];
        
        output.pos = mul(unity_ObjectToWorld, float4(bp.pos, 1.0f));
        output.normal = mul(unity_ObjectToWorld, float4(bp.coY, 0.0f));
        output.tangent = mul(unity_ObjectToWorld, float4(bp.coZ, 0.0f));

        return output;        
      }


      [maxvertexcount(18)]
      void GS_Main(line GS_INPUT p[2], inout TriangleStream<FS_INPUT> triStream)
      {

        //we consider the sequence P --- 0 ---- 1 ---- E
        float3 P0 = p[0].pos;
        float3 P1 = p[1].pos;
        float3 V10 = P1-P0;
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
          angles[a] = float2(cos(PI4 * a), sin (PI4 *a ));
        }
        angles[8] = angles[0];


        float4x4 ump = UNITY_MATRIX_MVP;
        float4x4 uwo = unity_WorldToObject;
        float4x4 vp = mul(ump, uwo);
        float4 v;
        FS_INPUT pIn;

        for (int n = 0; n< 9; n++)
        {
          float3 N0 = angles[n].x * Yf0 * Y0 + angles[n].y * Zf0 * Z0;
          float3 N1 = angles[n].x * Yf1 * Y1 + angles[n].y * Zf1 * Z1;

          v = float4(P0 + _Radius * N0, 1.0f);
          pIn.pos = mul(vp, v);
          triStream.Append(pIn);

          v = float4(P1 + _Radius * N1, 1.0f);
          pIn.pos = mul(vp, v);
          triStream.Append(pIn);
        }
      }
      // Fragment Shader -----------------------------------------------
      float4 FS_Main(FS_INPUT input) : COLOR
      {
        return float4(0,0,0,1);
      }
      ENDCG
    }
  }
}
