Shader "Custom/DatastreamShader" {
	Properties{

		_NormalColor("Normal Color", Color) = (1,1,1,1)
		_CorruptColor("Corrupt Color", Color) = (1,1,1,1)
		_Alpha("Transparency", Range(-0,1)) = 1

		_DatastreamHealth("Datastream Health", Range(-0,1)) = 1
		_DatastreamSpeed("Datastream Speed", float) = 1
		_MainTex("Edge Texture", 2D) = "white" {}
		_Numbers("Numbers Texture", 2D) = "white" {}
		_NumberGradiant ("Number Gradiant", 2D) = "white" {}

		_MainNoise("Main Noise", 2D) = "white" {}
		_UniformNoise("Uniform Noise", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Transparent" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard alpha

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Numbers;
		sampler2D _NumberGradiant;
		sampler2D _MainNoise;
		sampler2D _UniformNoise;

		float4 _Numbers_ST;

		struct Input {
			float2 uv_MainTex;
		};

		float4 _NormalColor;
		float4 _CorruptColor;
		float _Alpha;
		float _DatastreamHealth;
		float _DatastreamSpeed;

		#define TRANSFORM_TEX(tex,name) (tex.xy * name##_ST.xy + name##_ST.zw)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 background = tex2D (_MainTex, IN.uv_MainTex) * lerp (_CorruptColor, _NormalColor, _DatastreamHealth);

			float4 main = tex2D(_MainNoise, IN.uv_MainTex + float2(-_Time[1] * _DatastreamSpeed * 2, 0))
				+ tex2D(_MainNoise, IN.uv_MainTex + float2(-_Time[1] * _DatastreamSpeed * 1.5, 0))
				+ tex2D(_MainNoise, IN.uv_MainTex + float2(-_Time[1] * _DatastreamSpeed, 0)) / 3;

			float4 other = tex2D(_UniformNoise, IN.uv_MainTex + float2(-_Time[1] * _DatastreamSpeed * 2, 0))
				+ tex2D(_UniformNoise, IN.uv_MainTex + float2(-_Time[1] * _DatastreamSpeed * 1.5, 0))
				+ tex2D(_UniformNoise, IN.uv_MainTex + float2(-_Time[1] * _DatastreamSpeed, 0)) / 3;

			float4 ntex = tex2D(_Numbers, IN.uv_MainTex * _Numbers_ST.xy);
			float4 gradiant = clamp(pow(tex2D(_NumberGradiant, IN.uv_MainTex), lerp (1, 3, _DatastreamHealth)), 0, 1);

			float4 numbers = (gradiant.a) *
				lerp(
					lerp(_NormalColor, _CorruptColor, _DatastreamHealth),
					lerp(_CorruptColor, _NormalColor, _DatastreamHealth),
					lerp(
						other.a,
						main.a,
						_DatastreamHealth
					)
				);
			float4 c = (background + numbers) * float4(1, 1, 1, _Alpha);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
