Shader "Custom/Vignetting"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Intensity("Intensity", Range(0.1, 10)) = 1
        _X("X", Range(0.0, 1.0)) = 0.5
        _Y("Y", Range(0.0, 1.0)) = 0.5
        _Size("Size", Range(2.0, 0.5)) = 1.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha


        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            float _Intensity;
            float _X;
            float _Y;
            float _Size;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                float2 offset = float2(_X, _Y) * 2 - 1;
                float2 uv = i.uv * 2 - 1;
                        

                float size = 2.0 - _Size;
                fixed4 outColour = fixed4(_Intensity, _Intensity, _Intensity, 1);                        
                outColour *= sqrt(1-0.5*( size * length( uv + offset)));
                outColour.w = 1.0;
                return outColour * tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}