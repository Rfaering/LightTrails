Shader "Custom/Voronoi"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _InputTime("Time", Range(0, 1000000000)) = 0
		_Movement("Movement", Range(0, 5)) = 1.0
		_Shade("Shade", Range(0, 1.0)) = 1.0
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
            float _InputTime;
			float _Movement;
			float _Shade;

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
                float2 p = 10*i.uv;
                
                float2 pi = floor(p);    
                float4 v = float4( pi.xy, pi.xy + 1.0);
                v -= 64.*floor(v*0.015);
                v.xz = v.xz*1.435 + 34.423;
                v.yw = v.yw*2.349 + 183.37;
                v = v.xzxz*v.yyww;
                v *= v;
                
                v *= _InputTime* ( _Movement * 0.000004 ) + 1.5;
                float4 vx = 0.25*sin(frac(v*0.00047)*6.2831853);
                float4 vy = 0.25*sin(frac(v*0.00074)*6.2831853);

                
                float2 pf = p - pi;
                vx += float4(0., 1., 0., 1.)-pf.xxxx;
                vy += float4(0., 0., 1., 1.)-pf.yyyy;
                v = vx*vx + vy*vy;
                
                v.xy = min(v.xy, v.zw);
                float3 col = lerp(float3(0.0,0.4,0.9), float3(0.0,0.95,0.9), min(v.x,v.y));
                float4 fragColor = float4(col, 1.0);

                float2 uv = i.uv;

                return ( fragColor * _Shade) + ( tex2D(_MainTex, uv) * ( 1 - _Shade ));
            }
            ENDCG
        }
    }
}