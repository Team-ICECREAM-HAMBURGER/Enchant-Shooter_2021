// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "water"
{
	Properties
	{
		_DeepColor("DeepColor", Color) = (0.131052,0.1355168,0.8962264,0)
		_ShallowColor("ShallowColor", Color) = (0.3254717,1,0.953834,0)
		_WaterDepth("WaterDepth", Float) = 10
		_TextureSample3("Texture Sample 3", 2D) = "white" {}
		_Vector3("Vector 3", Vector) = (0.35,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		GrabPass{ }
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 3.0
		#if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex);
		#else
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex)
		#endif
		#pragma surface surf Unlit alpha:fade keepalpha noshadow vertex:vertexDataFunc 
		struct Input
		{
			float4 screenPos;
			float2 uv_texcoord;
			float4 screenPosition17;
		};

		ASE_DECLARE_SCREENSPACE_TEXTURE( _GrabTexture )
		uniform sampler2D _TextureSample3;
		uniform float2 _Vector3;
		uniform float4 _TextureSample3_ST;
		uniform float4 _ShallowColor;
		uniform float4 _DeepColor;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _WaterDepth;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 temp_cast_0 = (_WaterDepth).xxx;
			float3 vertexPos17 = temp_cast_0;
			float4 ase_screenPos17 = ComputeScreenPos( UnityObjectToClipPos( vertexPos17 ) );
			o.screenPosition17 = ase_screenPos17;
		}

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float2 uv0_TextureSample3 = i.uv_texcoord * _TextureSample3_ST.xy + _TextureSample3_ST.zw;
			float2 panner25 = ( 1.0 * _Time.y * _Vector3 + uv0_TextureSample3);
			float4 screenColor48 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_GrabTexture,( ase_screenPosNorm + ( tex2D( _TextureSample3, panner25 ) * 0.22 ) ).xy);
			float4 ase_screenPos17 = i.screenPosition17;
			float4 ase_screenPosNorm17 = ase_screenPos17 / ase_screenPos17.w;
			ase_screenPosNorm17.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm17.z : ase_screenPosNorm17.z * 0.5 + 0.5;
			float screenDepth17 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm17.xy ));
			float distanceDepth17 = abs( ( screenDepth17 - LinearEyeDepth( ase_screenPosNorm17.z ) ) / ( 1.0 ) );
			float4 lerpResult18 = lerp( _ShallowColor , _DeepColor , distanceDepth17);
			float4 lerpResult47 = lerp( screenColor48 , lerpResult18 , lerpResult18.a);
			o.Emission = lerpResult47.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18301
789;909;1058;815;4350.284;1970.093;4.055118;True;False
Node;AmplifyShaderEditor.Vector2Node;36;-2623.97,-566.6318;Inherit;True;Property;_Vector3;Vector 3;4;0;Create;True;0;0;False;0;False;0.35,0;0.35,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;34;-2616.152,-845.7181;Inherit;False;0;32;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;25;-2348.777,-688.7459;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;38;-1910.798,-451.878;Inherit;False;Constant;_ReflectionScale;ReflectionScale;6;0;Create;True;0;0;False;0;False;0.22;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-2233.785,107.6203;Inherit;False;Property;_WaterDepth;WaterDepth;2;0;Create;True;0;0;False;0;False;10;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;32;-2029.493,-696.0925;Inherit;True;Property;_TextureSample3;Texture Sample 3;3;0;Create;True;0;0;False;0;False;-1;224f8dc1da357234dbf0886a29259ebb;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;19;-1933.5,-135.8316;Inherit;False;Property;_ShallowColor;ShallowColor;1;0;Create;True;0;0;False;0;False;0.3254717,1,0.953834,0;0.3254717,1,0.953834,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DepthFade;17;-1990.556,105.0864;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;20;-1829.79,274.6377;Inherit;False;Property;_DeepColor;DeepColor;0;0;Create;True;0;0;False;0;False;0.131052,0.1355168,0.8962264,0;0.131052,0.1355168,0.8962264,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;-1680.012,-529.3746;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;39;-1718.868,-837.0557;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;18;-1536.796,148.1733;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;40;-1444.059,-603.2703;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.BreakToComponentsNode;45;-1261.589,173.6754;Inherit;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.ScreenColorNode;48;-1205.611,-553.3606;Inherit;False;Global;_GrabScreen0;Grab Screen 0;6;0;Create;True;0;0;False;0;False;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;47;-995.1046,-16.37147;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;23;-677.697,-189.6623;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;water;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;2;False;-1;3;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;4;1;False;-1;1;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;2;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;25;0;34;0
WireConnection;25;2;36;0
WireConnection;32;1;25;0
WireConnection;17;1;15;0
WireConnection;37;0;32;0
WireConnection;37;1;38;0
WireConnection;18;0;19;0
WireConnection;18;1;20;0
WireConnection;18;2;17;0
WireConnection;40;0;39;0
WireConnection;40;1;37;0
WireConnection;45;0;18;0
WireConnection;48;0;40;0
WireConnection;47;0;48;0
WireConnection;47;1;18;0
WireConnection;47;2;45;3
WireConnection;23;2;47;0
ASEEND*/
//CHKSM=94B219C224E3F3D1FEE2CAD204BC6C4A3437C517