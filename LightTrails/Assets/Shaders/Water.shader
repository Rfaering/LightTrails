Shader "Custom/Water" {
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
			float tau = 6.28318530718f;
			int maxIterations = 5;

			half2 p = fmod(i.screenuv * tau * _Zoom * 2.0f, tau) - 250.0;

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

			half2 localuv = i.screenuv;

			float sum = 0.0f;
			if (_WaterDistortion) {
				sum = ((waterColor[0] - 0.5f) / 150.0f);
			}


			half4 color = tex2D(_GrabTexture, i.screenuv + float2(sum , -sum));

			return color + half4(waterColor, 1.0f);
		}
			ENDCG
		}
		}
}