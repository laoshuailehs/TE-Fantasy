Shader "HS/MyShader1"
{
    Properties
    {
        [HDR]_Color("MyColor", Color) = (1,1,1,1)
        _Color2("MyColor2", Color) = (1,1,1,1)
        _Int("MyInt", Int) = 1
        [PowerSlider(5)]_Float("MyFloat", Range(0,1)) = 0.5
        [IntRange]_Float2("MyIntRange", Range(0,10)) = 0.5
        [Toggle]_Bool("MyBool", Float) = 0
        [Enum(UnityEngine.Rendering.CullMode)]_Float3("MyCullMode", Float) = 1
        _Vector("MyVector", Vector) = (1,1,1,1)
        [Header(MyHeader)]
        _MainTex2("My_MainTex", 2D) = "white"{}
        _MainTex3("MainTex3", 3D) = "white"{}
        [hideInInspector]
        _Cube("MyCube", Cube) = "white"{}
                
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            // float4 vert (float4 vertex : POSITION): SV_POSITION
            // {
            //     return UnityObjectToClipPos(vertex);
            // }
            //
            fixed4 _Color;
            // fixed4 frag (): SV_Target
            // {
            //     return _Color;
            // }


            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv:TEXCOORD;
            };
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv:TEXCOORD;
            };
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed checker(float2 uv)
            {
                float2 repeatUV=uv*10;
                float2 c=floor(repeatUV)/2;
                float checker = frac(c.x+c.y)* 2;
                return checker;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                // fixed4 col = _Color;
                // return _Color;
                fixed col=checker(i.uv);
                return col;
            }
            
            
            ENDCG
        }
    }
//    FallBack Off
    FallBack "Diffuse"
    CustomEditor "EditorName"
}
