Shader "Custom/HsShader1"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            Varyings vert(Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS.xyz);
                o.uv = v.uv;
                return o;
            }

            half4 frag(Varyings i) : SV_Target
            {
                float2 I = i.uv * _ScreenParams.xy;
                float4 O = 0;
                float z = 0, d = 0, iStep = 0, t = _Time.y;

                for (; iStep < 40.0; iStep++)
                {
                    float3 P = z * normalize(float3(I + I, 0) - _ScreenParams.xyx) + 0.1;
                    float3 p = float3(atan2(P.z + 9.0, P.x + 0.1) * 2.0 - 0.3 * P.y,
                                  0.6 * P.y - t,
                                  length(P.xz) - 4.0);

                    for (d = 0.0; d++ < 9.0;)
                        p += sin(p.yzx * d + t + 0.4 * iStep) / d;

                    z += d = 0.2 * length(float4(cos(p + P.y * 0.2) - 1.0, p.z));

                    O += float4(4.0, z, 2.0, 0.0) / (d * d * z);
                }

                O = tanh(O / 400.0);

                return O;
            }
            ENDHLSL
        }
    }
}
