Shader "Custom/Wave" {
	Properties{
		_AttMask("Mask", 2D) = "white"{}
		_Speed("Speed", Range(1.0, 5.0)) = 1.0
		_Zoom("Zoom", Range(1.0, 3.0)) = 1.0
		_InputTime("Time", Range(0, 1000000000)) = 0
		[Toggle(_WaterDistortion)] _WaterDistortion("Distortion", Float) = 0
	}
		SubShader{
			Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" }
			Lighting Off
			Fog{ Mode Off }
			ZWrite Off
			LOD 200
			GrabPass{ "_GrabTexture" }
			Pass{
			CGPROGRAM
			//#pragma target 3.0
	#pragma vertex vert
	#pragma fragment frag
	#include "UnityCG.cginc"


			sampler2D _GrabTexture;

		struct appdata_t {
			float4 vertex  : POSITION;
			half2 uv : TEXCOORD0;
		};

		struct v2f {
			float4 vertex  : SV_POSITION;
			float2 screenuv : TEXCOORD0;
			float2 uv : TEXCOORD1;
		};

		v2f vert(appdata_t v)
		{
			v2f o;
			o.vertex = UnityObjectToClipPos(v.vertex);

			half4 screenpos = ComputeGrabScreenPos(o.vertex);
			o.screenuv = screenpos.xy;
			o.uv = v.uv;

			return o;
		}

		float _InputTime;
		float _Zoom;
		float _Speed;
		sampler2D _AttMask;
		float _WaterDistortion;

		fixed4 frag(v2f i) : COLOR
		{			
			/*vec2 uv = (fragCoord.xy / iResolution.xy) * 2.0 - 1.0;
			float aspect = iResolution.x / iResolution.y;
			uv.x *= aspect;*/
			float pi = 3.141592;

			float2 uv = i.uv;

			float2 pos = float2(0.5f, 0.5f);
			float dist = length(i.uv - pos);

			float time = _InputTime;
			float diff = abs(time - dist);
			float func = sin(pi * diff);
			
			float2 screenuv = i.screenuv;
			screenuv += screenuv * uv * func * 0.02;

			half4 bColor = tex2D(_GrabTexture, screenuv);

			return bColor;// half4(dist, dist, 0.0f, 1.0f);
		}
			ENDCG
		}
		}
}