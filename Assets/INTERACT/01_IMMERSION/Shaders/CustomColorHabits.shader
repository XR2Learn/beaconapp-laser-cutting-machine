Shader "Custom/CustomColorHabits"
{
    Properties
    {
        [NoScaleOffset] _MainTex ("Albedo (RGB)", 2D) = "white" {}
        [NoScaleOffset] _MaskTex("Mask", 2D) = "black" {}
        _MaskColor("Mask Color", Color) = (1.0,0.0,0.0,0.0)
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        [NoScaleOffset] _BumpMap("Normal Map", 2D) = "bump"{}
        [NoScaleOffset] _LogoTex("Logo", 2D) = "red" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0


        struct Input
        {
            float2 uv_MainTex : TEXCOORD0;
            float2 uv2_LogoTex : TEXCOORD1; 
        };
        

        sampler2D _MainTex;
        sampler2D _BumpMap;
        sampler2D _MaskTex;
        sampler2D _LogoTex;
        half _Glossiness;
        half _Metallic;
        fixed4 _MaskColor;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void Blend_Overlay_fixed4(fixed4 Base, fixed4 Blend, float Opacity, out fixed4 Out)
        {
            fixed4 result1 = 1.0 - 2.0 * (1.0 - Base) * (1.0 - Blend);
            fixed4 result2 = 2.0 * Base * Blend;
            fixed4 zeroOrOne = step(Base, 0.5);
            Out = result2 * zeroOrOne + (1 - zeroOrOne) * result1;
            Out = lerp(Base, Out, Opacity);
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {

            fixed4 basecolor = tex2D (_MainTex, IN.uv_MainTex);
            fixed4 opacity = tex2D(_MaskTex, IN.uv_MainTex);
            
            //Overlay blend

            fixed4 result1 = 1.0 - 2.0 * (1.0 - basecolor) * (1.0 - _MaskColor);
            fixed4 result2 = 2.0 * basecolor * _MaskColor;
            fixed4 zeroOrOne = step(basecolor, 0.5);

            fixed4 blendedcolor = result2 * zeroOrOne + (1 - zeroOrOne) * result1;
            blendedcolor = lerp(basecolor, blendedcolor, opacity);

            //add logo

            fixed4 finalcolor = lerp(blendedcolor, tex2D(_LogoTex, IN.uv2_LogoTex), tex2D(_LogoTex, IN.uv2_LogoTex).a);

            o.Albedo = finalcolor.rgb;
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
