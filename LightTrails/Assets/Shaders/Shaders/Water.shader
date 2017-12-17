Shader "Custom/Water" {
	Properties{
		_MainTex ("Texture", 2D) = "white" {}
		_AttMask("Mask", 2D) = "white"{}
		_Speed("Speed", Range(1.0, 5.0)) = 1.0
		_Zoom("Zoom", Range(1.0, 3.0)) = 1.0
		_InputTime("Time", Range(0, 1000000000)) = 0
		_WaterDistortion("Distortion", Range(0.0, 3.0)) = 0
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
			float _Zoom;
			float _Speed;
			sampler2D _AttMask;
			float _WaterDistortion;
            
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
				float tau = 6.28318530718f;
				int maxIterations = 5;

				half2 p = fmod(i.uv * tau * _Zoom / 1.5f, tau) - 250.0;

				half2 o = p;

				float c = 1.0;
				float inten = .005;

				for (int n = 0; n < maxIterations; n++)
				{
					float t = (_InputTime * (1.0f - (3.5f / float(n + 1))) / 5) * _Speed;
					o = p + half2(cos(t - o.x) + sin(t + o.y), sin(t - o.y) + cos(t + o.x));
					c += 1.0 / length(half2(p.x / (sin(o.x + t) / inten), p.y / (cos(o.y + t) / inten)));
				}

				c = c / float(maxIterations);
				c = 1.17 - pow(c, 1.4);
				float v = pow(abs(c), 8.0);
				float3 waterColor = float3(v, v, v);

				half4 mask = tex2D(_AttMask, i.uv);
				waterColor = (clamp(waterColor + float3(0.0, 0.35, 0.5), 0.0, 1.0)) * mask;

				half2 localuv = i.uv;

				float sum = 0.0f;
				
				if ( _WaterDistortion > 0.0 ) {
					sum = ( ( waterColor[0] - 0.5f) * ( waterColor[1] - 0.5f)* ( waterColor[2] - 0.5f) ) * _WaterDistortion;
				}

				half2 newuv = i.uv + float2( sum , -sum );

				if( newuv[0] < 0 ){
					newuv[0] = 0.001;
				}
				if( newuv[0] > 1 ){
					newuv[0] = 0.999;
				}
				if( newuv[1] < 0 ){
					newuv[1] = 0.001;
				}
				if( newuv[1] > 1 ){
					newuv[1] = 0.999;
				}

				half4 color = tex2D( _MainTex, newuv );

				return color + half4(waterColor, 1.0f);
            }
            ENDCG
        }
    }
}