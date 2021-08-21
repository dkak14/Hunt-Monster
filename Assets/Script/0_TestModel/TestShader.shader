Shader "Custom/TestShader"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _BumpTex("Normal Texture", 2D) = "bump" {}
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        //1Pass
        cull front
        CGPROGRAM
        #pragma surface surf Lambert vertex:vert
        struct Input
        {
            float _Blank;
        };

        void vert(inout appdata_full v)
        {
            v.vertex.xyz += v.normal.xyz * 0.008;
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            o.Albedo = 0;
        }
        ENDCG

        //2Pass
        cull back
        CGPROGRAM
        #pragma surface surf _CustomCell

        sampler2D _MainTex;
        sampler2D _BumpTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpTex;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            float4 mainTex = tex2D(_MainTex, IN.uv_MainTex);
            
            o.Normal = UnpackNormal(tex2D(_BumpTex, IN.uv_BumpTex));
            o.Albedo = mainTex.rgb;
        }

        float4 Lighting_CustomCell(SurfaceOutput o, float3 lightDir , float atten)
        {
            float fNDotl = dot(o.Normal, lightDir) * 0.7 + 0.3;

            if (fNDotl > 0.3)
                fNDotl = 1;
            else
                fNDotl = 0.1;

            float4 fResult;
            fResult.rgb = fNDotl * o.Albedo * _LightColor0.rgb * atten;
            fResult.a = o.Alpha;

            return fResult;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
