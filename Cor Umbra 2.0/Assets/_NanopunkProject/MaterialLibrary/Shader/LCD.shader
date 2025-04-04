Shader "Custom/LCD"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Resolution ("Resolution", Vector) = (1920, 1080, 1, 1)
        _PB ("Brightness", Float) = 0.4
        _SclV ("Scanline Darkness", Float) = 0.25
        _VignetteIntensity ("Vignette Intensity", Range(0, 1)) = 0.5
        _VignetteSmoothness ("Vignette Smoothness", Range(0, 1)) = 0.5 // Suavidade da vinheta
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

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
            float4 _MainTex_ST;
            float4 _Resolution;
            float _PB;
            float _SclV;
            float _VignetteIntensity;
            float _VignetteSmoothness;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Get pos relative to 0-1 screen space
                float2 fragCoord = i.uv * _Resolution.xy;
                float2 uv = fragCoord / _Resolution.xy;

                // Map texture to 0-1 space
                fixed4 texColor = tex2D(_MainTex, uv);

                // Default lcd color (affects brightness)
                float pb = _PB;
                fixed4 lcdColor = fixed4(pb, pb, pb, 1.0);

                // Change every 1st, 2nd, and 3rd vertical strip to RGB respectively
                int px = int(fmod(fragCoord.x, 3.0));
                if (px == 1) lcdColor.r = 1.0;
                else if (px == 2) lcdColor.g = 1.0;
                else lcdColor.b = 1.0;

                // Darken every 3rd horizontal strip for scanline
                float sclV = _SclV;
                if (int(fmod(fragCoord.y, 3.0)) == 0) lcdColor.rgb = float3(sclV, sclV, sclV);

                // Calculate vignette effect with smoothness
                float2 center = float2(0.5, 0.5);
                float dist = distance(uv, center);
                float vignette = smoothstep(_VignetteIntensity, _VignetteIntensity - _VignetteSmoothness, dist);

                // Apply vignette to the final color
                texColor.rgb *= vignette;

                return texColor * lcdColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
