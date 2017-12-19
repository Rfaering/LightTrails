Shader "Custom/Wave" {
	Properties{
		_MainTex ("Texture", 2D) = "white" {}		
		_InputTime("Time", Range(0, 1000000000)) = 0
        _X("X", Range(0.0, 1.0)) = 0.5
        _Y("Y", Range(0.0, 1.0)) = 0.5
        _Intensity("Intensity", Range(1, 20)) = 5		
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
			sampler2D _AttMask;
			float _WaterDistortion;
            float _X;
            float _Y;
            float _Intensity;
            
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
				float pi = 3.141592;

				float2 uv = i.uv*2-1;

				float2 pos = float2(_X, _Y);
				float dist = length(i.uv - pos);

				float time = _InputTime;
				float diff = abs(time - dist);
				float func = sin(pi * diff);
				
				float2 screenuv = i.uv;
				screenuv += screenuv * uv * func * 0.005 * _Intensity;

				half4 bColor = tex2D(_MainTex, screenuv);

                return bColor;
				//return float4(func*0.2, func*0.2, func, 1.0);// bColor;// half4(dist, dist, 0.0f, 1.0f);
            }
            ENDCG
        }
    }

}