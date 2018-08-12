// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/WarpShader"
{
	Properties {
		_MainTex ("Texture", 2D) = "white" {}	
	}
	SubShader {
		Pass {
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment MyFragmentProgram

			#include "UnityCG.cginc"

			struct v2f {
			    float4 pos : POSITION;
			    float2 uv : TEXCOORD0; 
			};

			v2f vert (appdata_img v){
			    v2f o;
			    o.pos = UnityObjectToClipPos(v.vertex);
			    o.uv = v.texcoord;
			    return o;
			}

			uniform sampler2D _MainTex;
			uniform float2 _WarpOrigin;
			uniform float _Radius;
			uniform float _WaveThickness;
			uniform float _Ratio;
			uniform float _Distortion;
			

			float4 MyFragmentProgram (v2f i) : COLOR {
			    float2 offset = _WarpOrigin - i.uv;
			    // float2 offset = i.uv - _WarpOrigin;
			    float2 ratio = {_Ratio, 1};			
				float rad = length(offset / ratio);
				if (rad < _Radius) {
					// checkDistance is between 0 and _Radius
					float2 checkDistance = _Radius - rad;
					// float strength = checkDistance / _Radius;
					// strength = _WaveThickness - checkDistance;
					// strength = strength / _WaveThickness;

					float strength = (_WaveThickness - checkDistance) / _WaveThickness;
					strength = max(strength, 0);
					// strength is from 1 to 0 at this point
					
					strength = pow(strength, _Distortion);

					// strength *= _Distortion;					
					// strength = sqrt(sqrt(strength));

					float2 checkPoint = i.uv + (normalize(offset) * strength);
					return tex2D(_MainTex, checkPoint);
				// } else if (rad < _Radius + 0.1) {
					// return float4(1,0,0,1);
				} else {
					return tex2D(_MainTex, i.uv);
				}
			}

			ENDCG
		}
	}
}
