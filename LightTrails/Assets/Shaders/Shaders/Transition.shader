// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Transition"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_TransitionTex("Transition Texture", 2D) = "white" {}
		_Color("Screen Color", Color) = (1,1,1,0)
		_Wait("Wait", Range(0, 10)) = 5
		_Length("Length", Range(1, 10)) = 5
		_Softness("Softness", Range(0, 20)) = 1
		_InputTime("_Time", Range(0, 10000000)) = 0
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

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float2 uv1 : TEXCOORD1;
					float4 vertex : SV_POSITION;
				};

				float4 _MainTex_TexelSize;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					o.uv1 = v.uv;

					return o;
				}

				sampler2D _TransitionTex;
				int _Distort;
				float _Fade;
				float _InputTime;
				float _Length;
				float _Softness;
				float _Wait;

				sampler2D _MainTex;
				fixed4 _Color;

				fixed4 frag(v2f i) : SV_Target
				{
					fixed4 transit = tex2D(_TransitionTex, i.uv1);

					fixed4 col = tex2D(_MainTex, i.uv);
 					float diff = transit.b - (( ( _InputTime * 4 ) / _Length ) - _Wait);
					if ( diff < 0 )
						col.a = 1 - clamp( abs(diff) / _Softness, 0, 1 );
						return col;
						//return col = lerp( _Color, col, clamp( abs(diff), 0, 1 ) );

					return col;
				}					
				ENDCG
			}
		}
}
