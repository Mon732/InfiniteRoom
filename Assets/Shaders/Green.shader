Shader "Green"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Colour ("Colour", Color) = (1, 1, 1, 1)
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque" "Queue" = "Transparent+1" }
		Pass
		{
			ZWrite On
			ZTest Greater

			Stencil
			{
				Ref 2
				Comp equal
				Pass keep
				ZFail decrWrap
			}

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
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Colour;

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			half4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv) * _Colour;
				return col;
			}

			ENDCG
		}
	}
}