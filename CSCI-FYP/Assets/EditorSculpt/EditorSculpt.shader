// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/EditorSculpt"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_MaskColor("Mask Color",Color) = (0.0, 0.0, 0.0, 1.0)
		_DispMode("Display Mode",Int) = 1
	}

		SubShader
		{
		Tags {
			"RenderType" = "Opaque"
			"Queue" = "Geometry+1"
			}

			Pass
			{
				Blend DstColor Zero
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 3.0

				#include "UnityCG.cginc"

				fixed4 _MaskColor;
				uniform sampler2D _MainTex;
				int _DispMode;

				struct appdata
				{
					float4 vertex : POSITION;
					float4 color : COLOR;
					float2 texcoord0 : TEXCOORD0;
					float2 texcoord3 : TEXCOORD3;
				};

				struct v2f
				{
					float4 position : SV_POSITION;
					float4 color : TEXCOORD1;
					float2 texcoord0 : TEXCOORD0;
					float2 texcoord3 : TEXCOORD3;
				};

				v2f vert(appdata i)
				{
					v2f o;
					o.position = UnityObjectToClipPos(i.vertex);
					o.color = i.color;
					o.texcoord0 = i.texcoord0;
					o.texcoord3 = i.texcoord3;
					return o;
				}
				fixed4 frag(v2f i) : SV_Target
				{
					fixed4 maskcol;

				maskcol.x = i.texcoord3.x + _MaskColor.x;
				maskcol.y = i.texcoord3.x + _MaskColor.y;
				maskcol.z = i.texcoord3.x + _MaskColor.z;
				maskcol.a = 1.0;
				if (_DispMode == 0)
				{
					maskcol = i.color*maskcol;
				}
				else if (_DispMode == 2)
				{
					maskcol.x *= i.texcoord3.y;
					maskcol.y *= i.texcoord3.y;
					maskcol.z *= i.texcoord3.y;
				}
				else if (_DispMode == 3)
				{
					maskcol = maskcol * tex2D(_MainTex, i.texcoord0);
				}
				maskcol.a = 1.0;
				return maskcol;
				}
			ENDCG
			}
		}
}