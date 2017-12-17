Shader "Custom/Grid" {
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_AttMask("Mask", 2D) = "white"{}
		_Speed("Speed", Range(1.0, 5.0)) = 1.0
		_Zoom("Zoom", Range(1.0, 3.0)) = 1.0
		_InputTime("Time", Range(0, 1000000000)) = 0
		[Toggle(_WaterDistortion)] _WaterDistortion("Distortion", Float) = 0
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
            
			float _InputTime;
			float _Zoom;
			float _Speed;
			sampler2D _AttMask;
			float _WaterDistortion;

            fixed4 frag (v2f i) : SV_Target
            {
				float pi = 3.141592;

				float2 uv = i.uv;

				float2 pos = float2(0.5f, 0.5f);
				float dist = length(i.uv - pos);

				float time = _InputTime;
				float diff = abs(time - dist);
				float func = sin(pi * diff);
				
				uv += uv * func * 0.05;

				float2 screenuv = i.uv;
				screenuv += screenuv * func * 0.02;

				float stripes = 10.0;
				float thickness = 15.0;
				float sharpness = 5.0;
				half2 a = sin(stripes * 0.5 * pi * uv - pi / 2.0);
				half2 b = abs(a);

				half4 color = half4(0.0f, 0.0f, 0.0f, 1.0f);
				color += 1.0 * exp(-thickness * b.x);
				color += 1.0 * exp(-thickness * b.y);
				//color += 0.5 * exp(-(thickness / 4.0) * sin(b.x));
				//color += 0.5 * exp(-(thickness / 3.0) * b.y);

				half4 t = half4(uv.x * 0.5 + 0.5*sin(time), uv.y * 0.5 + 0.5*cos(time), pow(cos(time), 4.0), 0.5f) + 0.5;

				color *= t;

				half4 bColor = tex2D(_MainTex, screenuv);

				return bColor+=color;
            }
            ENDCG
        }
    }
}