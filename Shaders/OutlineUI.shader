Shader "FairyGUI/OutlineUI"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineWidth("Outline Width", Range(0, 1.0)) = 0.9
        _OutlineColor("Outline Color", Color) = (1,0,0,1)
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            float _OutlineWidth;
            float4 _OutlineColor;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
            
                 float2 uv = i.uv;
                 uv = abs(uv - float2(0.5, 0.5)) / 0.5;
                 float maxU = uv.x - _OutlineWidth;
                 float maxV = uv.y - _OutlineWidth;
                 float t = 1 - step(max(maxU, maxV), 0);

                 col.rgb += t * _OutlineColor;
                 col.a = max(col.a, t); 
                return col;
            }
            ENDCG
        }
    }
}
