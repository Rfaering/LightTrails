Shader "Custom/Disortion" {
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_Distort("Distort", 2D) = "white" {}
		_AttMask("Mask", 2D) = "white" {}
		_Intensity("Intensity", Range(10, 100)) = 50
		_Speed("Speed", Range(0, 3)) = 1
		_InputTime("Time", Range(0, 1000000000)) = 0
		[Toggle(DEBUGUV)] _DEBUGUV("Debug Texture Coordinates", Float) = 0
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
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
			
			float _Speed;
			float _Intensity;
			sampler2D _AttMask;
			sampler2D _Distort;
			float _InputTime;

            fixed4 frag (v2f i) : SV_Target
            {
				half2 distortLocation = frac(i.uv + _InputTime * _Speed / 10.0f);
				half2 distort = tex2D(_Distort, distortLocation).xy;

				float maskPixel = tex2D( _AttMask, i.uv).r;

				// distort*2-1 transforms range from 0..1 to -1..1.
				// negative values move to the left, positive to the right.
				half2 offset = ((distort.xy * 2 - 1) / (10000 / _Intensity)) * maskPixel;

				// get screen space position of current pixel
				half2 uv = i.uv + offset;
				half4 color = tex2D(_MainTex, uv);

				UNITY_OPAQUE_ALPHA(color.a);

                return color;
            }
            ENDCG
        }
    }
}