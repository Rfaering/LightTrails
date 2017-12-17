// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Movement"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_EffectTex("Layer", 2D) = "black"{}
		_EffectColor("Color", Color) = (1,1,1,1)
		_MotionLayer("Motion Layer", 2D) = "black"{}
		_AttMask("Mask", 2D) = "white"{}
		_Speed("Speed", float) = 0
		_Zoom("Zoom", float) = 1.0
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
			
			sampler2D _EffectTex;
			sampler2D _AttMask;
			sampler2D _MotionLayer;
			float _Speed;
			half4 _EffectColor;
			float _InputTime;
			float _Zoom;

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
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed bg = col.a;

				fixed4 mask = tex2D(_AttMask, i.uv);
				fixed4 motionLayer = tex2D(_MotionLayer, i.uv);
				fixed4 motion = tex2D(_EffectTex, fixed2(_InputTime * _Speed / 10, 0) + i.uv * _Zoom * fixed2(1.5f - motionLayer.r / 2 , 1.0f));
				motion *= _EffectColor;
				motion *= mask.r;
				col += motion * motion.a;

				return col;
			}
			ENDCG
		}
	}	
}
