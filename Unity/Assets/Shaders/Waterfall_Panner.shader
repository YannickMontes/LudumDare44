// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Neandertal/Waterfall_Shader"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_UVScale("UV Scale", Float) = 0
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Float0("Float 0", Float) = 0
		_Float5("Float 5", Float) = 6.4
		_Float10("Float 10", Float) = 6.4
		_Float9("Float 9", Range( -100 , 100)) = 6.4
		_Float6("Float 6", Range( -100 , 100)) = 6.4
		_Speed("Speed", Float) = 0
		_TextureSample2("Texture Sample 2", 2D) = "white" {}
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_NoiseScale("NoiseScale", Float) = 0
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" "IgnoreProjector" = "True" "DisableBatching" = "True" }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha noshadow 
		struct Input
		{
			float3 worldPos;
		};

		uniform sampler2D _TextureSample0;
		uniform float _Speed;
		uniform float _UVScale;
		uniform float _Float0;
		uniform sampler2D _TextureSample1;
		uniform float _NoiseScale;
		uniform sampler2D _TextureSample2;
		uniform float _Float5;
		uniform float _Float6;
		uniform float _Float10;
		uniform float _Float9;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float temp_output_8_0 = ( _Speed * _Time.x );
			float3 ase_worldPos = i.worldPos;
			float2 appendResult19 = (float2(ase_worldPos.x , ase_worldPos.y));
			float2 _worldUVs22 = ( appendResult19 / _UVScale );
			float temp_output_4_0_g1 = 3.0;
			float temp_output_5_0_g1 = 3.0;
			float2 appendResult7_g1 = (float2(temp_output_4_0_g1 , temp_output_5_0_g1));
			float totalFrames39_g1 = ( temp_output_4_0_g1 * temp_output_5_0_g1 );
			float2 appendResult8_g1 = (float2(totalFrames39_g1 , temp_output_5_0_g1));
			float clampResult42_g1 = clamp( 0.0 , 0.0001 , ( totalFrames39_g1 - 1.0 ) );
			float temp_output_35_0_g1 = frac( ( ( ( _Float0 * _Time.y ) + clampResult42_g1 ) / totalFrames39_g1 ) );
			float2 appendResult29_g1 = (float2(temp_output_35_0_g1 , ( 1.0 - temp_output_35_0_g1 )));
			float2 temp_output_15_0_g1 = ( ( _worldUVs22 / appendResult7_g1 ) + ( floor( ( appendResult8_g1 * appendResult29_g1 ) ) / appendResult7_g1 ) );
			float2 panner2 = ( temp_output_8_0 * float2( 0,1 ) + temp_output_15_0_g1);
			o.Albedo = tex2D( _TextureSample0, panner2 ).rgb;
			o.Alpha = 1;
			float temp_output_96_0 = ( temp_output_8_0 * 5.0 );
			float2 temp_output_102_0 = ( _worldUVs22 * _NoiseScale );
			float2 panner95 = ( temp_output_96_0 * float2( 0,1 ) + temp_output_102_0);
			float2 panner100 = ( temp_output_96_0 * float2( 0,0.2 ) + temp_output_102_0);
			float4 temp_output_98_0 = ( tex2D( _TextureSample1, panner95 ) + tex2D( _TextureSample2, panner100 ) );
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float clampResult76 = clamp( ( ( ase_vertex3Pos.x - _Float5 ) / _Float6 ) , 0.0 , 1.0 );
			float clampResult85 = clamp( ( ( ase_vertex3Pos.x - -_Float5 ) / -_Float6 ) , 0.0 , 1.0 );
			float4 lerpResult59 = lerp( float4(1,1,1,0) , float4(0,0,0,0) , ( clampResult76 + clampResult85 ));
			float4 lerpResult93 = lerp( ( temp_output_98_0 * lerpResult59 ) , float4(1,1,1,0) , lerpResult59);
			float clampResult112 = clamp( ( ( ase_vertex3Pos.z - _Float10 ) / _Float9 ) , 0.0 , 1.0 );
			float4 lerpResult118 = lerp( ( temp_output_98_0 * clampResult112 ) , float4( 1,1,1,0 ) , clampResult112);
			clip( ( lerpResult93 * lerpResult118 ).r - _Cutoff );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15500
7;1109;1906;1004;1935.843;131.0081;1.526244;True;True
Node;AmplifyShaderEditor.CommentaryNode;17;-3036.497,-784.1976;Float;False;988.8707;455.5483;;5;22;21;20;19;18;World UVs;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;71;-2256.674,600.7279;Float;False;738.9893;555.8591;Create the world gradient;10;79;78;77;76;75;74;73;72;104;105;;1,1,1,1;0;0
Node;AmplifyShaderEditor.WorldPosInputsNode;18;-2932.261,-653.3782;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;19;-2639.743,-611.9377;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;80;-2264.435,1186.272;Float;False;738.9893;555.8591;Create the world gradient;6;86;85;84;83;82;81;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-2651.323,-486.4238;Float;False;Property;_UVScale;UV Scale;1;0;Create;True;0;0;False;0;0;249.52;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;78;-2239.136,799.6862;Float;False;Property;_Float5;Float 5;4;0;Create;True;0;0;False;0;6.4;-1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;77;-2227.818,648.993;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TimeNode;7;-2205.411,388.5471;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;21;-2454.347,-611.9377;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-2136.411,295.5471;Float;False;Property;_Speed;Speed;8;0;Create;True;0;0;False;0;0;1.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;86;-2235.579,1234.537;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NegateNode;105;-2234.5,1046.159;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;79;-2256.514,905.0991;Float;False;Property;_Float6;Float 6;7;0;Create;True;0;0;False;0;6.4;-5;-100;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;22;-2273.752,-594.6568;Float;False;_worldUVs;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-1954.411,233.5471;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode;104;-2120.919,1016.848;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;106;-1112.394,936.8102;Float;False;738.9893;555.8591;Create the world gradient;8;112;111;110;109;108;107;115;114;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;103;-1902.765,580.5352;Float;False;Property;_NoiseScale;NoiseScale;11;0;Create;True;0;0;False;0;0;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;72;-2047.141,708.6829;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;94;-1937.026,451.2403;Float;False;22;0;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;81;-2054.902,1294.227;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;75;-1937.244,983.5342;Float;False;Constant;_Float2;Float 2;-1;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;73;-1922.408,1076.437;Float;False;Constant;_Float1;Float 1;-1;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;84;-1945.005,1569.079;Float;False;Constant;_Float4;Float 4;-1;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;102;-1733.433,511.4637;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;115;-1115.519,1130.261;Float;False;Property;_Float10;Float 10;5;0;Create;True;0;0;False;0;6.4;-4.6;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;82;-1930.169,1661.982;Float;False;Constant;_Float3;Float 3;-1;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;83;-1888.139,1394.775;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;107;-1083.538,985.0754;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;74;-1880.378,809.2303;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;96;-1869.128,347.9055;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;100;-1654.434,377.4636;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0.2;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ClampOpNode;85;-1674.447,1471.049;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;108;-902.8618,1044.765;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;76;-1666.686,885.5048;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;114;-989.233,1204.08;Float;False;Property;_Float9;Float 9;6;0;Create;True;0;0;False;0;6.4;3.5;-100;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;95;-1664.801,249.5947;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TimeNode;28;-2655.042,273.1882;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;89;-1327.636,1119.687;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;29;-2648.042,175.188;Float;False;Property;_Float0;Float 0;3;0;Create;True;0;0;False;0;0;-7.27;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;111;-792.9646,1319.617;Float;False;Constant;_Float8;Float 8;-1;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;109;-778.1287,1412.519;Float;False;Constant;_Float7;Float 7;-1;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;97;-1422.313,320.5906;Float;True;Property;_TextureSample2;Texture Sample 2;9;0;Create;True;0;0;False;0;None;990e1613af649334d8b0ebd9146e3f96;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;90;-1431.325,130.0538;Float;True;Property;_TextureSample1;Texture Sample 1;10;0;Create;True;0;0;False;0;None;9e459ef5994c8614f8017a26bf8429ad;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;110;-736.0989,1145.313;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;57;-1443.663,664.3295;Float;False;Constant;_Color0;Color 0;7;0;Create;True;0;0;False;0;1,1,1,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;58;-1444.879,827.4216;Float;False;Constant;_Color1;Color 1;7;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;112;-522.406,1221.587;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;23;-2723.439,9.850872;Float;False;22;0;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;98;-1085.434,276.4636;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-2371.042,115.188;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;59;-1125.671,762.3799;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;25;-2115.737,-86.01794;Float;False;Flipbook;-1;;1;53c2488c220f6564ca6c90721ee16673;2,71,0,68,0;8;51;SAMPLER2D;sampler5125;False;13;FLOAT2;0,0;False;4;FLOAT;3;False;5;FLOAT;3;False;24;FLOAT;0;False;2;FLOAT;0;False;55;FLOAT;0;False;70;FLOAT;0;False;5;COLOR;53;FLOAT2;0;FLOAT;47;FLOAT;48;FLOAT;62
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;101;-1028.434,473.4636;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;92;-1443.87,509.7873;Float;False;Constant;_Color2;Color 2;7;0;Create;True;0;0;False;0;1,1,1,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;117;-569.0455,694.9591;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;93;-856.7397,547.3941;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PannerNode;2;-1775.022,99.88214;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;118;-399.9055,679.9788;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;1,1,1,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-1466.022,-83.11784;Float;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;None;9ced0b909087a0b4b9f77f48e26e507f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;113;-254.9557,349.3073;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;33;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Neandertal/Waterfall_Shader;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;TransparentCutout;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;19;0;18;1
WireConnection;19;1;18;2
WireConnection;21;0;19;0
WireConnection;21;1;20;0
WireConnection;105;0;78;0
WireConnection;22;0;21;0
WireConnection;8;0;3;0
WireConnection;8;1;7;1
WireConnection;104;0;79;0
WireConnection;72;0;77;1
WireConnection;72;1;78;0
WireConnection;81;0;86;1
WireConnection;81;1;105;0
WireConnection;102;0;94;0
WireConnection;102;1;103;0
WireConnection;83;0;81;0
WireConnection;83;1;104;0
WireConnection;74;0;72;0
WireConnection;74;1;79;0
WireConnection;96;0;8;0
WireConnection;100;0;102;0
WireConnection;100;1;96;0
WireConnection;85;0;83;0
WireConnection;85;1;84;0
WireConnection;85;2;82;0
WireConnection;108;0;107;3
WireConnection;108;1;115;0
WireConnection;76;0;74;0
WireConnection;76;1;75;0
WireConnection;76;2;73;0
WireConnection;95;0;102;0
WireConnection;95;1;96;0
WireConnection;89;0;76;0
WireConnection;89;1;85;0
WireConnection;97;1;100;0
WireConnection;90;1;95;0
WireConnection;110;0;108;0
WireConnection;110;1;114;0
WireConnection;112;0;110;0
WireConnection;112;1;111;0
WireConnection;112;2;109;0
WireConnection;98;0;90;0
WireConnection;98;1;97;0
WireConnection;30;0;29;0
WireConnection;30;1;28;2
WireConnection;59;0;57;0
WireConnection;59;1;58;0
WireConnection;59;2;89;0
WireConnection;25;13;23;0
WireConnection;25;2;30;0
WireConnection;101;0;98;0
WireConnection;101;1;59;0
WireConnection;117;0;98;0
WireConnection;117;1;112;0
WireConnection;93;0;101;0
WireConnection;93;1;92;0
WireConnection;93;2;59;0
WireConnection;2;0;25;0
WireConnection;2;1;8;0
WireConnection;118;0;117;0
WireConnection;118;2;112;0
WireConnection;1;1;2;0
WireConnection;113;0;93;0
WireConnection;113;1;118;0
WireConnection;33;0;1;0
WireConnection;33;10;113;0
ASEEND*/
//CHKSM=8F83231A441B2CB22966D5EF26F093D118560B1B