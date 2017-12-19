Shader "Custom/Paint" {
	Properties{
	}
	SubShader{
		Tags{ "RenderType" = "Transparent" }
		Tags { "Queue" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		Lighting Off
		Fog{ Mode Off }
		ZWrite Off
		LOD 200

	Pass{
	CGPROGRAM
	//#pragma target 3.0
	#pragma vertex vert
	#pragma fragment frag
	#include "UnityCG.cginc"


	sampler2D _MainTex;

	struct appdata_t {
		float4 vertex  : POSITION;
		half2 uv : TEXCOORD0;
	};

	struct v2f {
		float4 vertex  : SV_POSITION;
		half2 uv : TEXCOORD1;
	};

	v2f vert(appdata_t v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = v.uv;
		return o;
	}

	fixed4 frag(v2f i) : COLOR
	{
		half4 color = tex2D(_MainTex, i.uv);

		return float4(color.a, color.a, color.a, 0.5f);
	}
		ENDCG
	}
	}
}