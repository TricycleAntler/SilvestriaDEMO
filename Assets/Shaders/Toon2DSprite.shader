Shader "Toon/2D Sprite"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)
        _LightColor("Light Color", Color) = (1,1,1,1)
        _LightDirection("Light Direction", Vector) = (0,0,1)
        _BumpMap("Bump Map", 2D) = "bump" {}
        _Smoothness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }

        SubShader
        {
            Tags
            {
                "RenderType" = "Opaque"
                "Queue" = "Transparent"
            }

            LOD 200

            CGPROGRAM
            // Use shader model 5.0 target, to get nicer looking lighting
            #pragma target 5.0

            #pragma multi_compile __ UNITY_HDR_ON
            #pragma multi_compile _ LIGHTMAP_ON
            #pragma multi_compile _ DIRLIGHTMAP_COMBINED
            #pragma multi_compile _ DYNAMICLIGHTMAP_ON
            #pragma multi_compile _ SHADOWS_SHADOWMASK

            #pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
            #pragma multi_compile _ SHADOWS_SCREEN

            #include "Lighting.cginc"
            #include "UnityCG.cginc"
            #include "UnityCG.cginc"

            #pragma surface surf Toon fullforwardshadows

            sampler2D _MainTex;
            sampler2D _BumpMap;
            fixed4 _Color;
            fixed4 _LightColor;
            float4 _LightDirection;
            fixed _Smoothness;
            fixed _Metallic;

            struct Input
            {
                float2 uv_MainTex;
                float2 uv_BumpMap;
            }

            void surf(Input IN, inout SurfaceOutput o);
            {
                // Sample texture and bump map
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                half3 bump = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));

                // Calculate lighting
                half3 viewDir = normalize(_WorldSpaceLightPos0.xyz);
                half3 lightDir = normalize(_LightDirection.xyz);
                half3 norm = bump * 2 - 1;
                half diff = max(dot(norm, lightDir), 0);
                half spec = _Smoothness;
                half3 r = reflect(-lightDir, norm);
                spec *= pow(saturate(dot(r, viewDir)), 5);
                half3 specular = _LightColor.rgb * spec * _Metallic;

                // Combine diffuse and specular lighting
                o.Albedo = c.rgb * diff;
                o.Emission = c;
                o.Metallic = _Metallic;
                o.Smoothness = _Smoothness;
                o.Normal = bump;
            }

            ENDCG
        }
}


