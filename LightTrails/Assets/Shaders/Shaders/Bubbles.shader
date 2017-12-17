Shader "Custom/Bubbles"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
                float2 uv = i.uv * 2 - 1;
            
                float4 col = tex2D(_MainTex, i.uv);

                fixed4 debug = fixed4(uv.x,uv.y,0,1);

                // Background
                fixed4 outColour = fixed4(0, 0, 0, 0);
            
                // Bubbles
                for (int i = 0; i < 10; i++) {

                    // Bubble seeds
                    float pha = sin(float(i)*546.13+1.0)*0.5 + 0.5;
                    float siz = sin(float(i))*0.1+0.25;
                    float pox = sin(float(i)*321.55+4.1);

                    // Bubble size, position and color
                    float rad = 0.1 + 0.5*siz;
                    float2  pos = float2( pox, -1.0-rad + (2.0+2.0*rad)*fmod(pha+0.1*_Time.y*(1+0.5*siz),1.0));
                    float dis = length( uv-pos );
                    float3 col = float3(0,0.8,0.8);
                    
                    // Add a black outline around each bubble
                    col+= 8.0*smoothstep( rad*0.95, rad, dis );
            
                    // Render
                    float f = length(uv-pos)/rad;
                    f = sqrt(clamp(1.0-f*f,0.0,1.0));

                    outColour.rgb -= col.zyx *(1.0-smoothstep( rad*0.95, rad, dis )) * f;
                }
            
                // Vignetting    
                //outColour *= sqrt(1-0.5*length(uv));
                
                return outColour + col;
            }
            ENDCG
        }
    }
}