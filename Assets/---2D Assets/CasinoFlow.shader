Shader "Custom/CasinoFlow" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _Health("Health", Range(0, 1)) = 1
        _Speed("Speed", Range(0.1, 10)) = 1
        _Amplitude("Amplitude", Range(0.01, 2)) = 0.5
    }
        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 100
            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float4 _Color;
                float _Health;
                float _Speed;
                float _Amplitude;

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    float time = _Time.y * _Speed;
                    float2 uv = i.uv;
                    uv.x = frac(uv.x + time * _Amplitude - _Health * _Amplitude);
                    float4 tex = tex2D(_MainTex, uv);
                    return tex * _Color;
                }

                ENDCG
            }
        }
            FallBack "Diffuse"
}
