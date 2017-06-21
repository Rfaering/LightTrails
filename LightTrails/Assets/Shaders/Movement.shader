// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Movement"
{
	Properties
	{
		_EffectTex("Layer", 2D) = "black"{}
		_EffectColor("Color", Color) = (1,1,1,1)
		_MotionLayer("Motion Layer", 2D) = "black"{}
		_Mask("Mask", 2D) = "white"{}
		_Speed("Speed", float) = 0
		_Zoom("Zoom", float) = 1.0
		_InputTime("Time", Range(0, 1000000000)) = 0
		[Toggle(DEBUGUV)] _DEBUGUV("Debug Texture Coordinates", Float) = 0
	}

		SubShader
		{
			Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" }
			Lighting Off
			Fog{ Mode Off }
			ZWrite Off
			LOD 200
			GrabPass{ "_GrabTexture" }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma shader_feature DEBUGUV

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;				
				float2 screenuv : TEXCOORD0;
				float2 uv : TEXCOORD1;
			};

			sampler2D _GrabTexture;

			sampler2D _EffectTex;
			sampler2D _Mask;
			sampler2D _MotionLayer;
			float _Speed;
			half4 _EffectColor;
			float _InputTime;
			float _Zoom;

			v2f vert(appdata v)
			{
				v2f o;
				// transform position to clip space
				// (multiply with model*view*projection matrix)
				o.vertex = UnityObjectToClipPos(v.vertex);

				half4 screenpos = ComputeGrabScreenPos(o.vertex);			
				o.screenuv = screenpos.xy;
				o.uv = v.uv;

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_GrabTexture, i.screenuv);
				fixed bg = col.a;

				fixed4 mask = tex2D(_Mask, i.uv);
				fixed4 motionLayer = tex2D(_MotionLayer, i.uv);
				fixed4 motion = tex2D(_EffectTex, fixed2(_InputTime * _Speed/10, 0) + i.screenuv * _Zoom * fixed2(1.5f - motionLayer.r / 2 , 1.0f));
				motion *= _EffectColor;
				motion *= mask.r;
				col += motion * motion.a;

#if DEBUGUV
				col.r = i.uv2;
				col.g = 0;
				col.b = 0;
#endif

				return col;
		}
		ENDCG
	}
		}
}
