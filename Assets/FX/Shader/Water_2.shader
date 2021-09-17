// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "water_2"
{
	Properties
	{
		_Color0("Color 0", Color) = (0,0,0,0)
		[HDR]_RippleColor("RippleColor", Color) = (0,0,0,0)
		_RippleSlimness("RippleSlimness", Float) = 2.44
		_RippleDensity("RippleDensity", Float) = 7
		_RippleSpeed("RippleSpeed", Float) = 1
		_NoiseTex("NoiseTex", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha noshadow 
		struct Input
		{
			float3 worldNormal;
			INTERNAL_DATA
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform sampler2D _NoiseTex;
		uniform float4 _Color0;
		uniform float _RippleDensity;
		uniform float _RippleSpeed;
		uniform float _RippleSlimness;
		uniform float4 _RippleColor;


		float2 voronoihash55( float2 p )
		{
			
			p = float2( dot( p, float2( 127.1, 311.7 ) ), dot( p, float2( 269.5, 183.3 ) ) );
			return frac( sin( p ) *43758.5453);
		}


		float voronoi55( float2 v, float time, inout float2 id, inout float2 mr, float smoothness )
		{
			float2 n = floor( v );
			float2 f = frac( v );
			float F1 = 8.0;
			float F2 = 8.0; float2 mg = 0;
			for ( int j = -1; j <= 1; j++ )
			{
				for ( int i = -1; i <= 1; i++ )
			 	{
			 		float2 g = float2( i, j );
			 		float2 o = voronoihash55( n + g );
					o = ( sin( time + o * 6.2831 ) * 0.5 + 0.5 ); float2 r = f - g - o;
					float d = 0.5 * dot( r, r );
			 		if( d<F1 ) {
			 			F2 = F1;
			 			F1 = d; mg = g; mr = r; id = o;
			 		} else if( d<F2 ) {
			 			F2 = d;
			 		}
			 	}
			}
			return F1;
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 ase_vertexNormal = mul( unity_WorldToObject, float4( ase_worldNormal, 0 ) );
			float2 panner69 = ( 1.0 * _Time.y * float2( 0.1,0.1 ) + i.uv_texcoord);
			float3 ase_worldPos = i.worldPos;
			o.Normal = ( ( float4( ase_vertexNormal , 0.0 ) * tex2D( _NoiseTex, panner69 ) ) + float4( ase_worldPos , 0.0 ) ).rgb;
			float time55 = ( _Time.y * _RippleSpeed );
			float2 temp_output_1_0_g1 = i.uv_texcoord;
			float2 temp_output_11_0_g1 = ( temp_output_1_0_g1 - float2( 0.5,0.5 ) );
			float2 break18_g1 = temp_output_11_0_g1;
			float2 appendResult19_g1 = (float2(break18_g1.y , -break18_g1.x));
			float dotResult12_g1 = dot( temp_output_11_0_g1 , temp_output_11_0_g1 );
			float2 coords55 = ( temp_output_1_0_g1 + ( appendResult19_g1 * ( dotResult12_g1 * float2( 1,1 ) ) ) + float2( 0,0 ) ) * _RippleDensity;
			float2 id55 = 0;
			float2 uv55 = 0;
			float fade55 = 0.5;
			float voroi55 = 0;
			float rest55 = 0;
			for( int it55 = 0; it55 <4; it55++ ){
			voroi55 += fade55 * voronoi55( coords55, time55, id55, uv55, 0 );
			rest55 += fade55;
			coords55 *= 2;
			fade55 *= 0.5;
			}//Voronoi55
			voroi55 /= rest55;
			o.Albedo = ( _Color0 + ( pow( voroi55 , _RippleSlimness ) * _RippleColor ) ).rgb;
			o.Alpha = 0.83;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18301
67;681;1293;357;1781.078;32.41939;1;True;False
Node;AmplifyShaderEditor.SimpleTimeNode;56;-2500.998,-413.9278;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;58;-2452.998,-222.9278;Inherit;False;Property;_RippleSpeed;RippleSpeed;5;0;Create;True;0;0;False;0;False;1;1.21;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;67;-2626.595,-613.7947;Inherit;False;Constant;_RippleShear;RippleShear;8;0;Create;True;0;0;False;0;False;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;70;-2291.991,279.3134;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;59;-2250.277,-144.48;Inherit;False;Property;_RippleDensity;RippleDensity;4;0;Create;True;0;0;False;0;False;7;7;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;72;-2322.991,488.3132;Inherit;False;Constant;_PannerSpeedXY;PannerSpeed X/Y;9;0;Create;True;0;0;False;0;False;0.1,0.1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;-2270.998,-359.9279;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;66;-2403.986,-746.2622;Inherit;True;Radial Shear;-1;;1;c6dc9fc7fa9b08c4d95138f2ae88b526;0;4;1;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.VoronoiNode;55;-2036.611,-365.9051;Inherit;True;0;0;1;0;4;False;1;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;3;FLOAT;0;FLOAT2;1;FLOAT2;2
Node;AmplifyShaderEditor.RangedFloatNode;61;-2096.884,-58.22623;Inherit;False;Property;_RippleSlimness;RippleSlimness;3;0;Create;True;0;0;False;0;False;2.44;1.73;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;69;-1993.706,433.7411;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PowerNode;60;-1844.189,-299.5393;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;63;-1818.494,-33.38907;Inherit;False;Property;_RippleColor;RippleColor;2;1;[HDR];Create;True;0;0;False;0;False;0,0,0,0;0.7749602,0.9875599,1.309888,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;68;-1802.354,403.2104;Inherit;True;Property;_NoiseTex;NoiseTex;6;0;Create;True;0;0;False;0;False;-1;None;6d36a8794f16f0a48ac940ffaccaf701;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NormalVertexDataNode;73;-1774.811,201.028;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;62;-1545.292,-273.2292;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;74;-1449.482,270.2807;Inherit;True;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WorldPosInputsNode;77;-1414.873,553.8707;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ColorNode;53;-1515.898,-535.7809;Inherit;False;Property;_Color0;Color 0;0;0;Create;True;0;0;False;0;False;0,0,0,0;0.3035286,0.1662069,0.7830189,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;54;-1059.69,-585.0471;Inherit;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;False;-1;None;b31207d48d5cbbb4b8e0811d4c3876b4;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;64;-1254.55,-323.7364;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;76;-1214.812,441.0284;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;65;-1046.498,85.44999;Inherit;False;Constant;_Float0;Float 0;8;0;Create;True;0;0;False;0;False;0.83;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;51;-872.5283,-107.1988;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;water_2;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;2;False;-1;3;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;True;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;5;False;-1;10;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;57;0;56;0
WireConnection;57;1;58;0
WireConnection;66;3;67;0
WireConnection;55;0;66;0
WireConnection;55;1;57;0
WireConnection;55;2;59;0
WireConnection;69;0;70;0
WireConnection;69;2;72;0
WireConnection;60;0;55;0
WireConnection;60;1;61;0
WireConnection;68;1;69;0
WireConnection;62;0;60;0
WireConnection;62;1;63;0
WireConnection;74;0;73;0
WireConnection;74;1;68;0
WireConnection;64;0;53;0
WireConnection;64;1;62;0
WireConnection;76;0;74;0
WireConnection;76;1;77;0
WireConnection;51;0;64;0
WireConnection;51;1;76;0
WireConnection;51;9;65;0
ASEEND*/
//CHKSM=15281682A4CD30F9324E6182E3288D93CE803098