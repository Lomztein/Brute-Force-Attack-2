Shader "Custom/DatastreamNumberShader" {
	Properties {
		_NormalColor("Normal Color", Color) = (0,1,0,1)
		_CorruptColor ("Corrupt Color", Color) = (1,0,0,1)
		_MainTex ("Number Texture", 2D) = "white" {}
		_Index("Number Index", Range (0,1)) = 0
		_DatastreamHealth("Datastream Health", Range(0, 1)) = 0
	}
	SubShader {
		Tags { "RenderType"="Transparent" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard alpha

		#pragma target 3.0

		sampler2D _MainTex;
		float4 _NormalColor;
		float4 _CorruptColor;
		float _Index;
		float _DatastreamHealth;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			if (_Index > _DatastreamHealth) {
				c *= _CorruptColor;
			}else{
				c *= _NormalColor;
			}
			c *= 3;

			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
