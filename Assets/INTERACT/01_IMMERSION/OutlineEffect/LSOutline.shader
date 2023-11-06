Shader "LS/Outline" {

  SubShader {
    Tags 
	{
      "Queue" = "Geometry"
    }

	//fill stencil pass
	 Pass {
	  Name "Mask"
	  Cull Back
	  ZTest Always
	  ZWrite Off
	  ColorMask 0

	  Stencil {
		Ref 1
		Pass Replace
	  }
	}

	//outline pass
    Pass {
      Name "Outline"
      Cull Back
      ZTest Always
      ZWrite Off

      Stencil {
        Ref 1
        Comp NotEqual
      }

      CGPROGRAM
      #include "UnityCG.cginc"

      #pragma vertex vert
      #pragma fragment frag

      struct appdata {
        float4 vertex : POSITION;
        float3 normal : NORMAL;
        UNITY_VERTEX_INPUT_INSTANCE_ID
      };

      struct v2f {
        float4 position : SV_POSITION;
		float4 screenPos : TEXCOORD1;
        UNITY_VERTEX_OUTPUT_STEREO
      };

      float _OutlineWidth = 4.0;

      v2f vert(appdata input) {
        v2f output;

        UNITY_SETUP_INSTANCE_ID(input);
        UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
		
		//2D offset in screen space
		float4 clipPosition = UnityObjectToClipPos(input.vertex);
		half3 wNormal = UnityObjectToWorldNormal(input.normal);
		float3 clipNormal = mul((float3x3) UNITY_MATRIX_VP, wNormal);
		float2 offset = normalize(clipNormal.xy) * _OutlineWidth * clipPosition.w * 0.001; // clipPosition.w for constant screen space size
		clipPosition.xy +=  offset;
		output.position = clipPosition;
		
		//uv from vertex in screen space
		output.screenPos = ComputeScreenPos(output.position);

        return output;
      }

	  sampler2D _HatchTx;
	  float _HatchSize = 1.0;
	  float _HatchSpeed = 1.0f;
	  half4 _OutlineColor = fixed4(1, 0, 0, 1);

      fixed4 frag(v2f input) : SV_Target
	  { 
			return _OutlineColor *tex2D(_HatchTx, input.screenPos.xy * _HatchSize * float2(_ScreenParams.x/ _ScreenParams.y,1.0) / input.screenPos.w + float2(_Time.x * _HatchSpeed,0));
      }
      ENDCG
    }
	  
	//fill pass
	Pass {
	Name "Fill"
	Cull Back
	ZTest Always
	ZWrite Off
	ColorMask RGB
	Blend DstColor Zero

		CGPROGRAM
		#include "UnityCG.cginc"

		#pragma vertex vert
		#pragma fragment frag

		struct appdata {
		float4 vertex : POSITION;
		float3 normal : NORMAL;
		UNITY_VERTEX_INPUT_INSTANCE_ID
		};

		struct v2f {
		float4 position : SV_POSITION;
		fixed4 color : COLOR;
		UNITY_VERTEX_OUTPUT_STEREO
		};

		half4 _FillColor = fixed4(0,1,0,1);

		v2f vert(appdata input)
		{
		v2f output;

		UNITY_SETUP_INSTANCE_ID(input);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

		output.position = UnityObjectToClipPos(input.vertex);
		output.color = _FillColor;

		return output;
		}

		fixed4 frag(v2f input) : SV_Target
		{
			return input.color;
		}

		ENDCG
	}
  }
}
