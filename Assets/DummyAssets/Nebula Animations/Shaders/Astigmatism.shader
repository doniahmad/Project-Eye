Shader "Custom/Astigmatism"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Amount ("Amount", Range(0, 1)) = 0.5
        _Orientation ("Orientation", Vector) = (1, 0, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        
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
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Amount;
            float2 _Orientation;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Calculate distortion based on orientation
                float2 offset = i.uv - 0.5;
                float2 distortedUV = i.uv + offset * _Amount * dot(offset, _Orientation);

                // Sample the texture using distorted UV coordinates
                fixed4 col = tex2D(_MainTex, distortedUV);
                return col;
            }
            ENDCG
        }
    }
}