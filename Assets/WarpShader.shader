// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/WarpShader"
{
	Properties {
	_WarpOrigin ("Warp Origin", Vector) = (0,0,0)
	}
	SubShader {
		Pass {
			CGPROGRAM

			#pragma vertex MyVertexProgram
			#pragma fragment MyFragmentProgram

			#include "UnityCG.cginc"

			float4 MyVertexProgram (float4 position : POSITION) : SV_POSITION {
				return UnityObjectToClipPos(position);
			}

			float2 _WarpOrigin;

			float4 MyFragmentProgram () : SV_TARGET {
				return float4(_WarpOrigin.x, _WarpOrigin.y, 0, 1);
			}

			ENDCG
		}
	}
}
