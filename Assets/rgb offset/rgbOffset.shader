Shader "Hidden/rgbOffset"
{
	Properties
	{
		[HideInInspector]
		_MainTex ("Texture", 2D) = "white" {}
		much ("much", Float) = 0.5
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
			float much;
			
			fixed4 frag (v2f i) : SV_Target
			{
				much = much*0.001;
				fixed4 col = tex2D(_MainTex, i.uv).g;
				// just invert the colors
				col.r = tex2D(_MainTex, i.uv + float2(-much,0)).r;
				col.b = tex2D(_MainTex, i.uv + float2( much,0)).b;
				return col;
			}
			ENDCG
		}
	}
}