Shader "Custom/Bars"
{
	Properties{
		_Color("Main Color", Color) = (1,1,1,1)
		_Add("Add", Range(-1, 1)) = 1.2
		_Bars("Bar Count", Range(1, 200)) = 10
		_Distance("Distance", Range(2, 5)) = 2
		_Speed("Speed", Range(0, 5)) = 1
		_InputTime("Time", Range(0, 1000000000)) = 0
		_MainTex ("Texture", 2D) = "white" {}
	}
    SubShader {
        Pass {

        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha


        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag

        #include "UnityCG.cginc"
        
        struct v2f {
            float4 pos : SV_POSITION;
            float2 uv : TEXCOORD0;
        };

		sampler2D _MainTex;
        float4 _MainTex_ST;
		fixed4 _Color;
		int _Bars;
		int _Distance;
		float _Speed;
		float _Add;		
		float _InputTime;

        v2f vert (appdata_base v)
        {
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);
            o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
            return o;
        }

        fixed4 frag (v2f i) : SV_Target
        {			
			fixed4 col = tex2D (_MainTex, i.uv) + float4(_Add, _Add, _Add, 0);

			if (fmod(round((i.uv[0] + _InputTime * _Speed / 10.0f) * _Bars), _Distance) != 0) {
				col = tex2D(_MainTex, i.uv);
			}

			return col;
        }
        ENDCG

        }
	}
}