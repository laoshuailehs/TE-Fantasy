Shader "Custom/HsShader2"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Transparent" "Queue"="Transparent"
        }
        LOD 100
        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            Name "ForwardLit"
            Tags
            {
                "LightMode" = "UniversalForward"
            }

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

            sampler2D _MainTex;
            float4 _MainTex_ST;

            Varyings vert(Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            // 定义变量
            float iTime;
            float2 iResolution;

            // smax 函数
            float smax(float a, float b, float k)
            {
                float h = max(k - abs(a - b), 0.0);
                return max(a, b) + h * h * 0.25 / k;
            }

            // sdSphere
            float sdSphere(float3 p, float r)
            {
                return length(p) - r;
            }

            // sdVerticalSemiCapsule
            float sdVerticalSemiCapsule(float3 p, float h, float r)
            {
                p.y = max(p.y - h, 0.0);
                return length(p) - r;
            }

            // sdCross
            float sdCross(float2 p, float2 b, float r)
            {
                p = abs(p);
                p = (p.y > p.x) ? p.yx : p.xy;
                float2 q = p - b;
                float k = max(q.y, q.x);
                float2 w = (k > 0.0) ? q : float2(b.y - p.x, -k);

                return sign(k) * length(max(w, 0.0)) + r;
            }

            // sdTrapezoid
            float dot2(float2 v) { return dot(v, v); }

            float sdTrapezoid(float2 p, float r1, float r2, float he)
            {
                float2 k1 = float2(r2, he);
                float2 k2 = float2(r2 - r1, 2.0 * he);

                p.x = abs(p.x);
                float2 ca = float2(max(0.0, p.x - ((p.y < 0.0) ? r1 : r2)), abs(p.y) - he);
                float2 cb = p - k1 + k2 * clamp(dot(k1 - p, k2) / dot2(k2), 0.0, 1.0);

                float s = (cb.x < 0.0 && ca.y < 0.0) ? -1.0 : 1.0;

                return s * sqrt(min(dot2(ca), dot2(cb)));
            }

            // iSphere
            float2 iSphere(float3 ro, float3 rd, float rad)
            {
                float b = dot(ro, rd);
                float c = dot(ro, ro) - rad * rad;
                float h = b * b - c;
                if (h < 0.0) return float2(-1.0, -1.0);
                h = sqrt(h);
                return float2(-b - h, -b + h);
            }

            // dents
            float dents(float2 q, float tr, float y)
            {
                const float an = 6.283185 / 12.0;
                float fa = (atan2(q.y, q.x) + an * 0.5) / an;
                float sym = an * floor(fa);
                float co = cos(sym), si = sin(sym);
                float2 r = float2(co * q.x - si * q.y, si * q.x + co * q.y);

                float d = length(max(abs(r - float2(0.17, 0)) - tr * float2(0.042, 0.041 * y), 0.0));
                return d - 0.005 * tr;
            }

            // gear
            float4 gear(float3 q, float off, float time)
            {
                {
                    float an = 2.0 * time * sign(q.y) + off * 6.283185 / 24.0;
                    float co = cos(an), si = sin(an);
                    q.xz = float2(co * q.x - si * q.z, si * q.x + co * q.z);
                }

                q.y = abs(q.y);

                float an2 = 2.0 * min(1.0 - 2.0 * abs(frac(0.5 + time / 10.0) - 0.5), 1.0 / 2.0);
                float3 tr = min(10.0 * an2 - float3(4.0, 6.0, 8.0), 1.0);

                // ring
                float d = abs(length(q.xz) - 0.155 * tr.y) - 0.018;

                // add dents
                float r = length(q);
                d = min(d, dents(q.xz, tr.z, r));

                // slice it
                float de = -0.0015 * clamp(600.0 * abs(dot(q.xz, q.xz) - 0.155 * 0.155), 0.0, 1.0);
                d = smax(d, abs(r - 0.5) - 0.03 + de, 0.005 * tr.z);

                // add cross
                float d3 = sdCross(q.xz, float2(0.15, 0.022) * tr.y, 0.02 * tr.y);
                float2 w = float2(d3, abs(q.y - 0.485) - 0.005 * tr.y);
                d3 = min(max(w.x, w.y), 0.0) + length(max(w, 0.0)) - 0.003 * tr.y;
                d = min(d, d3);

                // add pivot
                d = min(d, sdVerticalSemiCapsule(q, 0.5 * tr.x, 0.01));

                // base
                d = min(d, sdSphere(q - float3(0.0, 0.12, 0.0), 0.025));

                return float4(d, q.xzy);
            }

            float2 rot(float2 v)
            {
                return float2(v.x - v.y, v.y + v.x) * 0.707107;
            }

            // map
            float4 map(float3 p, float time)
            {
                float4 d = float4(sdSphere(p, 0.12), p);

                float3 qx = float3(rot(p.zy), p.x);
                if (abs(qx.x) > abs(qx.y)) qx = qx.zxy;
                float3 qy = float3(rot(p.xz), p.y);
                if (abs(qy.x) > abs(qy.y)) qy = qx.zxy;
                float3 qz = float3(rot(p.yx), p.z);
                if (abs(qz.x) > abs(qz.y)) qz = qx.zxy;
                float3 qa = abs(p);
                qa = (qa.x > qa.y && qa.x > qa.z) ? p.zxy : (qa.z > qa.y) ? p.yzx : p.xyz;

                float4 t;
                t = gear(qa, 0.0, time);
                if (t.x < d.x) d = t;
                t = gear(qx, 1.0, time);
                if (t.x < d.x) d = t;
                t = gear(qz, 1.0, time);
                if (t.x < d.x) d = t;
                t = gear(qy, 1.0, time);
                if (t.x < d.x) d = t;

                return d;
            }

            // calcNormal
            float3 calcNormal(float3 pos, float time)
            {
                float eps = 0.0005;
                float3 nor;
                nor.x = map(pos + float3(eps, 0, 0), time).x - map(pos - float3(eps, 0, 0), time).x;
                nor.y = map(pos + float3(0, eps, 0), time).x - map(pos - float3(0, eps, 0), time).x;
                nor.z = map(pos + float3(0, 0, eps), time).x - map(pos - float3(0, 0, eps), time).x;
                return normalize(nor);
            }

            // calcAO
            float calcAO(float3 pos, float3 nor, float time)
            {
                float occ = 0.0;
                float sca = 1.0;
                for (int i = 0; i < 5; i++)
                {
                    float h = 0.01 + 0.12 * float(i) / 4.0;
                    float d = map(pos + h * nor, time).x;
                    occ += (h - d) * sca;
                    sca *= 0.95;
                }
                return clamp(1.0 - 3.0 * occ, 0.0, 1.0);
            }

            // calcSoftshadow
            float calcSoftshadow(float3 ro, float3 rd, float k, float time)
            {
                float res = 1.0;
                float2 b = iSphere(ro, rd, 0.535);
                if (b.y > 0.0)
                {
                    float tmax = b.y;
                    float t = max(b.x, 0.001);
                    for (int i = 0; i < 64; i++)
                    {
                        float h = map(ro + rd * t, time).x;
                        res = min(res, k * h / t);
                        t += clamp(h, 0.012, 0.2);
                        if (res < 0.001 || t > tmax) break;
                    }
                }
                return clamp(res, 0.0, 1.0);
            }

            // intersect
            float4 intersect(float3 ro, float3 rd, float time)
            {
                float4 res = float4(-1.0, 0, 0, 0);
                float2 tminmax = iSphere(ro, rd, 0.535);
                if (tminmax.y > 0.0)
                {
                    float t = max(tminmax.x, 0.001);
                    for (int i = 0; i < 128 && t < tminmax.y; i++)
                    {
                        float4 h = map(ro + rd * t, time);
                        if (h.x < 0.001)
                        {
                            res = float4(t, h.yzw);
                            break;
                        }
                        t += h.x;
                    }
                }
                return res;
            }

            // setCamera
            float3x3 setCamera(float3 ro, float3 ta, float cr)
            {
                float3 cw = normalize(ta - ro);
                float3 cp = float3(sin(cr), cos(cr), 0.0);
                float3 cu = normalize(cross(cw, cp));
                float3 cv = cross(cu, cw);
                return float3x3(cu, cv, cw);
            }

            float3 getBackground(float3 rd)
                {
                    float sunFactor = pow(max(0.0, dot(normalize(float3(0.3, 0.4, 0.5)), rd)), 8.0);
                    float3 sky = float3(0.3, 0.6, 0.8) * (0.5 + 0.5 * rd.y);
                    float3 sun = float3(1.0, 0.8, 0.6) * sunFactor;
                    return sky + sun;
                }

            half4 frag(Varyings i) : SV_Target
            {
                iResolution = _ScreenParams.xy;
                iTime = _Time.y;

                float2 fragCoord = i.uv * iResolution;
                float2 p = (2.0 * i.uv - 1.0) * float2(iResolution.x / iResolution.y, 1.0);

                float an = 6.2831 * iTime / 40.0;
                float3 ta = float3(0.0, 0.0, 0.0);
                float3 ro = ta + float3(1.3 * cos(an), 0.5, 1.2 * sin(an));

                ro += 0.005 * sin(92.0 * iTime / 40.0 + float3(0.0, 1.0, 3.0));
                ta += 0.009 * sin(68.0 * iTime / 40.0 + float3(2.0, 4.0, 6.0));

                float3x3 ca = setCamera(ro, ta, 0.0);
                float fl = 2.0;
                float3 rd = mul(ca, normalize(float3(p, fl)));

                float3 col = float3(1.0 + rd.y, 1.0 + rd.y, 1.0 + rd.y) * 0.015;

                float4 tuvw = intersect(ro, rd, iTime);
                if (tuvw.x > 0.0)
                {
                    float3 pos = ro + tuvw.x * rd;
                    float3 nor = calcNormal(pos, iTime);
                    float len = length(pos);

                    float focc = 0.1 + 0.9 * clamp(0.5 + 0.5 * dot(nor, pos / len), 0.0, 1.0);
                    focc *= 0.1 + 0.9 * clamp(len * 2.0, 0.0, 1.0);

                    float occ = calcAO(pos, nor, iTime) * focc;

                    // Lighting
                    float3 lig = normalize(float3(0.8, 0.2, 0.6));
                    float dif = clamp(dot(nor, lig), 0.0, 1.0);
                    float sha = calcSoftshadow(pos + 0.001 * nor, lig, 20.0, iTime);
                    float3 hal = normalize(lig - rd);
                    float3 spe = pow(clamp(dot(nor, hal), 0.0, 1.0), 16.0) * (float3(0.5, 0.5, 0.5) + (1.0 - 0.5) * pow(
                        clamp(1.0 + dot(hal, rd), 0.0, 1.0), 5.0));

                    col += 1.5 * float3(1.0, 0.8, 0.7) * dif * sha * spe * 3.14;
                    col += 15.0 * float3(0.19, 0.22, 0.24) * sha * smoothstep(
                        -1.0 + 1.5 * focc, 1.0 - 0.4 * focc, reflect(rd, nor).y) * (0.5 + 0.5 * pow(
                        1.0 + dot(nor, rd), 5.0));
                }
                col = (tuvw.x > 0.0) ? col : float3(0.2, 0.4, 0.7);
                col = min(col, 1.0);
                // col = col * col * (3.0 - 2.0 * col);
                col * col * (2.0 - col); // 更柔和的提亮
                col += sin(fragCoord.x * 114.0) * sin(fragCoord.y * 211.1) / 512.0;

                return half4(col, 1.0);
            }
            ENDHLSL
        }
    }
}