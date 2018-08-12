// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/BrownHoleShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
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
			#pragma fragmentoption ARB_precision_hint_fastest
			
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float2 _HolePosition;
			uniform float _Ratio;
			uniform float _Radius;
			uniform float _Black;
			
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
			
			fixed4 frag (v2f i) : COLOR {
			    float2 offset = i.uv - _HolePosition;
			    float2 ratio = {_Ratio, 1};
			    float rad = length(offset / ratio);
			    //rad = length(offset);
			    float radiusRatio = rad / _Radius;
			    float blackRatio = _Black / _Radius;
			    if (rad < _Radius){
			        float deformation = blackRatio + ((1 - blackRatio) * radiusRatio);
//			        return float4(deformation, deformation, deformation, 1);
			        offset = (normalize(offset / ratio) * ratio) * (deformation * _Radius);
			        //return float4(length(offset), length(offset), length(offset), 1);
			    }
                offset += _HolePosition;
			    
			    half4 res = tex2D(_MainTex, offset);
			    if (rad < _Black){
			        float blackness = pow(rad / _Black, 6);
			        res *= blackness;
			    }
			    // debugger
//			    if (rad < _Radius){
//			        res[1] *= 1.1;
//			    }
			    return res;
			}
			
			ENDCG
		}
	}
}
