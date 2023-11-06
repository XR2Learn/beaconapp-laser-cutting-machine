// Adapted from the EDL shader code in potree which is modified from Christian Boucheny in cloud compare:
// https://github.com/cloudcompare/trunk/tree/master/plugins/qEDL/shaders/EDL
// https://github.com/SFraissTU/BA_PointCloud/blob/master/PointCloudRenderer/Assets/Resources/Shaders/EDL.shader
// Modify version from Code from Craig James/Kazys Stepanas 3rdEyeScene EDL shader
// https://github.com/data61/3rdEyeScene/blob/master/3rdEyeScene/Assets/Shaders/EDL.shader

Shader "Custom/EDL"
{
  Properties
  {
    // modify/comment out the MainTex to turn this into a grayscale effect, showing the EDL contribution 
    _MainTex("Base (RGB)", 2D) = "white" {}
    _DepthTexture("Depth Texture", 2D) = "black" {}
    _Color("Color", Color) = (1, 1, 1, 1)
    _Radius("Radius", float) = 3.0
    _ExpScale("Exp Scale", float) = 100.0
    _EdlScale("Edl Scale", float) = 1.0
  }

  SubShader
  {
    Pass
    {
      Tags{ "Queue" = "Overlay" }
      ZTest Always
      Cull Off
      ZWrite Off
    //ZWrite On

    CGPROGRAM
#pragma vertex vert_img
#pragma fragment frag
#include "UnityCG.cginc"

    sampler2D _MainTex;
    sampler2D _CameraDepthTexture;
    half4 _CameraDepthTexture_ST;
    float _Radius;
    float _ExpScale;
    float _EdlScale;

#define NEIGHBOUR_COUNT 8
    uniform float2 _NeighbourAddress[NEIGHBOUR_COUNT];

    ///////////////////////////////////////////////////////////////////////////
    // Support methods for EDL in fragment shader

    // Look at neighbour's depth and return lower factor if neighbours are significantly different.
    float computeObscurance(float depth, sampler2D depthTex, float2 uv)
    {
      //from https://github.com/SFraissTU/BA_PointCloud/blob/master/PointCloudRenderer/Assets/Resources/Shaders/EDL.shader
      // Simulated light direction. I noted that I got much better edge outlining by
      // making it (0, 0, -1). Using (0, 0, 1) tends to eat away the colour of lines and
      // points, making them black. Using (0, 0, -1) tends to outline lines and points.
      // I should really come back to investigate more and see if firstly I can understand why,
      // then see if I can remove the outline altogether where we have such edge transition.
      float3 lightDir = float3(0, 0, -1);
      float4 lightPlane = float4(lightDir, -dot(lightDir, float3(0, 0, depth)));
      float2 uvRadius = _Radius / float2(_ScreenParams.x, _ScreenParams.y);
      float2 neighbourRelativeUv, neighbourUv;
      float neighbourDepth, inPlaneNeighbourDepth;
      float sum = 0.0;

      for (int c = 0; c < NEIGHBOUR_COUNT; c++)
      {
        neighbourRelativeUv = uvRadius * _NeighbourAddress[c];
        neighbourUv = uv + neighbourRelativeUv;

        neighbourDepth = LinearEyeDepth(tex2D(depthTex, neighbourUv));
        // Try not to darken around transitions from surface to background.
        neighbourDepth = (neighbourDepth > 0.01) ? neighbourDepth : depth;

        inPlaneNeighbourDepth = dot(float4(neighbourRelativeUv, neighbourDepth, 1.0), lightPlane);
        sum += max(0.0, inPlaneNeighbourDepth) / _EdlScale;
      }
      return sum;
    }

    ///////////////////////////////////////////////////////////////////////////
    // Fragment shader
    // Depth from http://williamchyr.com/2013/11/unity-shaders-depth-and-normal-textures/
    // http://beta.unity3d.com/talks/Siggraph2011_SpecialEffectsWithDepth_WithNotes.pdf
    fixed4 frag(v2f_img i) : SV_Target
    {
      float2 uv = i.uv;
      sampler2D depthTex = _CameraDepthTexture;
      //Replaced _CameraDepthTexture with _DepthTexture to be explicitly set when calling.
      //This supports rendering the whole scene to a texture in a single pass, then extracting
      //the depth from that render into _DepthTexture. Otherwise a seperate depth pass is required.

      //float depth = LinearEyeDepth(tex2D(depthTex, uv));
      float depth = LinearEyeDepth(tex2D(depthTex, UnityStereoScreenSpaceUVAdjust(uv, _CameraDepthTexture_ST)));

      float edlFactor = computeObscurance(depth, depthTex, uv);
      edlFactor = exp(-_ExpScale * edlFactor);

      // For _MainTex, always use i.uv, not uv to cater for the potential flip.
      fixed4 color = tex2D(_MainTex, i.uv);

      if (depth <= 0.01)
      {
        edlFactor = 1.0f;
      }

      return fixed4(color.rgb * edlFactor, 1.0);

      // Show depth buffer
      //return fixed4(depth, depth, depth, 1.0f);
    }

    ENDCG
  }
  }
}
