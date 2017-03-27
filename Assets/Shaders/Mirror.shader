Shader "FX/MirrorReflection"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		[HideInInspector] _ReflectionTex ("", 2D) = "white" {}
		[HideInInspector] _Index("", Int) = -1
	}

	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
 
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 refl : TEXCOORD1;
				float4 pos : SV_POSITION;
			};

			float4 _MainTex_ST;
			sampler2D _MainTex;
			sampler2D _ReflectionTex;

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = mul (UNITY_MATRIX_MVP, v.pos);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.refl = ComputeScreenPos (o.pos);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 tex = tex2D(_MainTex, i.uv);
				fixed4 refl = tex2Dproj(_ReflectionTex, UNITY_PROJ_COORD(i.refl));
				return tex * refl;
			}

			ENDCG
	    }
	}
}