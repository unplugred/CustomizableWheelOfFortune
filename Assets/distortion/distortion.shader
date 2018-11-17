Shader "Hidden/distortion"
{
	Properties
	{
		[HideInInspector]
		_MainTex ("Texture", 2D) = "white" {}
		_DisTex  ("_DisTex" , 2D) = "white" {}
		fg ("Foreground", Color) = (1,1,1,1)
		size("size", Float) = 1
		ratio("ratio", Float) = 1
	}
	SubShader
	{
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
			sampler2D _DisTex;
			fixed4 fg;
			float size;
			float ratio;

			fixed4 GetColor(sampler2D main, sampler2D src, float2 uv){
				size = 1/size;
				float color = 1 - tex2D(src, uv.xy * size * float2(ratio, 1) + 0.5 * (1 - size) + float2((1 - ratio) * size * 0.5, 0)).r*0.9;
				if(color <= 0.101)
					return fg;
				return tex2D(main,(uv * color + 0.5 * (1 - color)));
			}

			fixed4 frag(v2f i) : COLOR  {
				return  GetColor (_MainTex, _DisTex, i.uv );
			}
			ENDCG
		}
	}
}
