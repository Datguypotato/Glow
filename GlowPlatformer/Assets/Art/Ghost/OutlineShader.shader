// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "mistral/OutlineShader"
{
	properties
	{
		_Outline("The scale of the Outline", Range(0, 0.1)) = 0.02
		_Factor("The Factor", Range(0, 0.1)) = 0.02
		_Color("The Color of your Outline", Color) = (1, 1, 1, 1)
	}

		SubShader
	{
		Pass
		{
			Tags { "LightMode" = "Always" }
			Cull Front
			ZWrite On

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform float _Outline;
			uniform float _Factor;
			uniform fixed4 _Color;

			struct vertexOutput
			{
				float4 pos : SV_POSITION;
			};

			vertexOutput vert(appdata_full v)
			{
				vertexOutput o;

				o.pos = UnityObjectToClipPos(v.vertex);
				float3 dir = normalize(v.vertex.xyz);
				float3 dir2 = v.normal;
				dir = lerp(dir, dir2, _Factor);
				dir = mul((float3x3)UNITY_MATRIX_IT_MV, dir);
				float2 offset = TransformViewToProjection(dir.xy);
				offset = normalize(offset);
				o.pos.xy += offset * o.pos.z * _Outline;

				return o;
			}

			fixed4 frag(vertexOutput i) : COLOR
			{
				return _Color;
			}
			ENDCG
		}

		Pass
		{
			Tags {"LightMode" = "ForwardBase"}

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform fixed4 _LightColor0;

			struct vertexOutput
			{
				float4 pos : SV_POSITION;
				float4 col : COLOR;
			};

			vertexOutput vert(appdata_full v)
			{
				vertexOutput o;
				o.pos = UnityObjectToClipPos(v.vertex);

				float3 normalDirection = normalize(mul(float4 (v.normal, 0.0), unity_WorldToObject).xyz);
				float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				float3 diffuseRelection = dot(normalDirection, lightDirection);
				o.col = fixed4(diffuseRelection, 1.0) * _LightColor0;
				o.pos = UnityObjectToClipPos(v.vertex);

				return o;
			}

			fixed4 frag(vertexOutput i) : COLOR
			{
				return i.col;
			}

			ENDCG

		}
	}
}
