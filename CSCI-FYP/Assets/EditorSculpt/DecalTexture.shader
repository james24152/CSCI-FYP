// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/DecalTexture"
	{
		Properties
		{
			_MainTex("Decal Texture", 2D) = "white" {}
			_DecalCam("Decal Camera", Vector) = (0,0,0,0)
			_DecalPos("Decal Position", Vector) = (0,0,0,0)
		}
	
    	SubShader
    	{
			//Add v1.16 for transparent
			Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True" }
			//
       	 Pass
        	{
				//Add v1.16 for tranparent
				ZWrite Off
				Blend SrcAlpha OneMinusSrcAlpha
				//
        		CGINCLUDE
				#include "UnityCG.cginc"
	
				struct appdata
				{
					float4 vertex : POSITION;
					float2 texcoord : TEXCOORD0;
				};
	
				struct v2f
				{
					float4 pos : SV_POSITION;
					float2 uv : TEXCOORD0;
				};
				ENDCG
	
            	CGPROGRAM
				#pragma target 3.0
            	#pragma vertex vert
           		#pragma fragment fdraw
           		 
           		uniform float4x4 _DecalMatrix;
           		uniform sampler2D _MainTex;

				v2f vert(appdata v)
				{
					v2f o;
					float4 posm = UnityObjectToClipPos(v.vertex);
					o.pos = posm;
					o.uv = v.texcoord;
					return o;
				}

				float4 fdraw(v2f i) : SV_Target
				{
					float4 c;
					c = tex2D(_MainTex, i.uv);
					return c;
				}

            	ENDCG
       		 }
    	}
}
