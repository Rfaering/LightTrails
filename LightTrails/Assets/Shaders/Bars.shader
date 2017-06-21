Shader "Custom/Bars"
{
	Properties{
		_Color("Main Color", Color) = (1,1,1,1)
		_Add("Add", Range(-1, 1)) = 1.2
		_Bars("Bar Count", Range(1, 200)) = 10
		_Distance("Distance", Range(2, 5)) = 2
		_Speed("Speed", Range(0, 5)) = 1
		_InputTime("Time", Range(0, 1000000000)) = 0
	}

		SubShader{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" }
		Lighting Off
		Fog{ Mode Off }
		ZWrite Off
		LOD 200
		GrabPass{ "_GrabTexture" }
		PASS {
			CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag
#include "UnityCG.cginc"

				// vertex shader inputs
				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

	// vertex shader outputs ("vertex to fragment")
	struct v2f
	{
		float2 uv : TEXCOORD0;
		float4 vertex : SV_POSITION;
	};

	// vertex shader
	v2f vert(appdata v)
	{
		v2f o;
		// transform position to clip space
		// (multiply with model*view*projection matrix)
		o.vertex = UnityObjectToClipPos(v.vertex);

		half4 screenpos = ComputeGrabScreenPos(o.vertex);
		o.uv = screenpos.xy;

		return o;
	}

	// texture we will sample
	sampler2D _MainTex;

	// color from the material
	fixed4 _Color;
	int _Bars;
	int _Distance;
	float _Speed;
	float _Add;
	sampler2D _GrabTexture;
	float _InputTime;

	// pixel shader; returns low precision ("fixed4" type)
	// color ("SV_Target" semantic)
	float4 frag(v2f i) : COLOR
	{
		fixed4 col = tex2D(_GrabTexture, i.uv) + float4(_Add, _Add, _Add, 0);

		if (fmod(round((i.uv[0] + _InputTime * _Speed / 10.0f) * _Bars), _Distance) != 0) {
			col = tex2D(_GrabTexture, i.uv);
		}

		return col;
	}

	ENDCG
}
	}
}