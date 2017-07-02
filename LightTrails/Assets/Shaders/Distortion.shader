// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//
			   // Created with Unity 5.1
			   // Example code to help answer the forum post at:
			   // http://forum.unity3d.com/threads/horizontal-wave-distortion.295769/
			   //
			   // This source code is not a full fledged distortion solution.
			   // It's just an example and hopefully proves useful for someone.
			   //
Shader "Custom/Disortion" {
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_AttMask("Mask", 2D) = "white" {}
		_Intensity("Intensity", Range(10, 100)) = 50
		_Speed("Speed", Range(0, 3)) = 1
		_InputTime("Time", Range(0, 1000000000)) = 0
		[Toggle(DEBUGUV)] _DEBUGUV("Debug Texture Coordinates", Float) = 0
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
	#pragma shader_feature MASK
	#pragma shader_feature DEBUGUV
	#include "UnityCG.cginc"

		// _MainTex is our distortion texture and
		// should use "Bypass sRGB Sampling" import setting.
		sampler2D _MainTex;
		float4 _MainTex_ST; // texture tiling and offset

							// _GrabTexture contains the contents of the screen
							// where the object is about to be drawn.
		sampler2D _GrabTexture;

		struct appdata_t {
			float4 vertex  : POSITION;
			half2 texcoord : TEXCOORD0;
		};

		struct v2f {
			float4 vertex  : SV_POSITION;
			half2 texcoord : TEXCOORD0;
			half2 screenuv : TEXCOORD1;
		};

		float _Speed;

		v2f vert(appdata_t v)
		{
			v2f o;
			o.vertex = UnityObjectToClipPos(v.vertex);

			o.texcoord = v.texcoord;

			half4 screenpos = ComputeGrabScreenPos(o.vertex);
			o.screenuv = screenpos.xy;

			return o;
		}

		float _Intensity;
		sampler2D _AttMask;
		float _InputTime;

		fixed4 frag(v2f i) : COLOR
		{
			half2 distortLocation = i.texcoord + _InputTime * _Speed / 10.0f;
			half2 distort = tex2D(_MainTex, frac(distortLocation)).xy;
			float maskPixel = tex2D(_AttMask, i.texcoord).r;

			// distort*2-1 transforms range from 0..1 to -1..1.
			// negative values move to the left, positive to the right.
			half2 offset = ((distort.xy * 2 - 1) / (10000 / _Intensity)) * maskPixel;

			// get screen space position of current pixel
			half2 uv = i.screenuv + offset;
			half4 color = tex2D(_GrabTexture, uv);

			UNITY_OPAQUE_ALPHA(color.a);


	#if DEBUGUV
			color.rg = maskPixel;
			color.b = 0;
	#endif


			return color;
		}
			ENDCG
		}
		}
}