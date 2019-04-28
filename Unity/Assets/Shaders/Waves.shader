// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Ludum/Waves"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Speed("Speed", Float) = 0.15
		_Vector0("Vector 0", Vector) = (0,0,0,0)
		_OffsetSpeed("OffsetSpeed", Float) = 0
		_Scale("Scale", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _OffsetSpeed;
		uniform float3 _Vector0;
		uniform sampler2D _TextureSample0;
		uniform float _Speed;
		uniform float _Scale;
		uniform float _Cutoff = 0.5;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			v.vertex.xyz += ( ( _OffsetSpeed * _SinTime.w ) * _Vector0 );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 temp_cast_0 = (_Scale).xx;
			float2 uv_TexCoord30 = i.uv_texcoord * temp_cast_0;
			float2 panner26 = ( ( ( _Speed * _Time.x ) * 5.0 ) * float2( 1,0 ) + uv_TexCoord30);
			float4 tex2DNode6 = tex2D( _TextureSample0, panner26 );
			o.Emission = tex2DNode6.rgb;
			o.Alpha = 1;
			clip( tex2DNode6.a - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16600
205;504;1234;657;1508.382;36.34467;1.885461;True;True
Node;AmplifyShaderEditor.RangedFloatNode;23;-1420.44,-22.96912;Float;False;Property;_Speed;Speed;3;0;Create;True;0;0;False;0;0.15;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;22;-1469.94,81.73125;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;41;-1528.563,-207.6692;Float;False;Property;_Scale;Scale;6;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-1218.942,-73.26883;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-1133.658,41.08953;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;30;-1290.845,-287.9108;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SinTimeNode;32;-1273.179,241.7503;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;38;-1308.16,181.7357;Float;False;Property;_OffsetSpeed;OffsetSpeed;5;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;-987.5269,244.2644;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;36;-1107.216,503.5541;Float;False;Property;_Vector0;Vector 0;4;0;Create;True;0;0;False;0;0,0,0;0,-0.33,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.PannerNode;26;-929.3306,-57.22128;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;12;-1653.742,-802.7193;Float;False;988.8707;455.5483;;5;19;16;15;14;13;World UVs;1,1,1,1;0;0
Node;AmplifyShaderEditor.WorldPosInputsNode;42;-1408.725,781.8091;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.GetLocalVarNode;28;-1019.95,-279.2484;Float;False;19;_worldUVs;1;0;OBJECT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-744.54,401.0196;Float;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WorldPosInputsNode;13;-1549.506,-671.8994;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;14;-1256.988,-630.4583;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-1268.568,-504.9443;Float;False;Property;_UVScale;UV Scale;2;0;Create;True;0;0;False;0;25;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;6;-480.3386,-121.0663;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;None;b2513a678ad4c7446b53a42e794d7c7e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;16;-1071.593,-630.4583;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;19;-890.9979,-613.1773;Float;False;_worldUVs;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;9;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Ludum/Waves;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;TransparentCutout;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;24;0;23;0
WireConnection;24;1;22;1
WireConnection;25;0;24;0
WireConnection;30;0;41;0
WireConnection;37;0;38;0
WireConnection;37;1;32;4
WireConnection;26;0;30;0
WireConnection;26;1;25;0
WireConnection;35;0;37;0
WireConnection;35;1;36;0
WireConnection;14;0;13;1
WireConnection;14;1;13;2
WireConnection;6;1;26;0
WireConnection;16;0;14;0
WireConnection;16;1;15;0
WireConnection;19;0;16;0
WireConnection;9;2;6;0
WireConnection;9;10;6;4
WireConnection;9;11;35;0
ASEEND*/
//CHKSM=0DFFA02508B64D29F016AD98C522E47404B6E5C6