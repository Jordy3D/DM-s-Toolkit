// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BaneArt/FogofWar"
{
	Properties
	{
		_EdgeLength ( "Edge length", Range( 2, 50 ) ) = 13.6
		_ColourA("Colour A", Color) = (1,0,0,1)
		_TextureA("Texture A", 2D) = "white" {}
		_ColourB("Colour B", Color) = (1,0,0,1)
		_TextureB("Texture B", 2D) = "white" {}
		[Toggle]_AppearDisappearToggle("Appear/Disappear Toggle", Float) = 1
		[Toggle]_TransparentTextureSwapToggle("Transparent/Texture Swap Toggle", Float) = 1
		_CentreStrength("Centre Strength", Range( 0 , 1)) = 1
		[Toggle]_ColourDiffuseToggle("Colour/Diffuse Toggle", Float) = 1
		_CentreRadius("Centre Radius", Range( 0 , 10)) = 5.588235
		_CentreSpinSpeed("Centre Spin Speed", Range( -1 , 1)) = 0
		_CentreNoise("Centre Noise", 2D) = "white" {}
		_RimStrength("Rim Strength", Range( 0 , 1)) = 1
		[Toggle]_AutoRadius("Auto Radius", Float) = 1
		_RadiusOffset("Radius Offset", Range( 0 , 1)) = 5.588235
		_RimRadius("Rim Radius", Range( 0 , 10)) = 5.588235
		_RimSpinSpeed("Rim Spin Speed", Range( -1 , 1)) = 0
		_RimNoise("Rim Noise", 2D) = "white" {}
		_RimColour("Rim Colour", Color) = (1,1,1,1)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "Tessellation.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 4.6
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform float _ColourDiffuseToggle;
		uniform float4 _ColourA;
		uniform sampler2D _TextureA;
		uniform float4 _TextureA_ST;
		uniform float4 _ColourB;
		uniform sampler2D _TextureB;
		uniform float4 _TextureB_ST;
		uniform float _CentreSpinSpeed;
		uniform sampler2D _CentreNoise;
		float4 _CentreNoise_TexelSize;
		uniform int ArrayLength;
		uniform float4 positionsArray[42];
		uniform float _CentreRadius;
		uniform float _CentreStrength;
		uniform float4 _RimColour;
		uniform float _RimSpinSpeed;
		uniform sampler2D _RimNoise;
		float4 _RimNoise_TexelSize;
		uniform float _AutoRadius;
		uniform float _RimRadius;
		uniform float _RadiusOffset;
		uniform float _RimStrength;
		uniform float _TransparentTextureSwapToggle;
		uniform float _AppearDisappearToggle;
		uniform float _EdgeLength;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		inline float DitherNoiseTex( float4 screenPos, sampler2D noiseTexture, float4 noiseTexelSize )
		{
			float dither = tex2Dlod( noiseTexture, float4(screenPos.xy * _ScreenParams.xy * noiseTexelSize.xy, 0, 0) ).g;
			float ditherRate = noiseTexelSize.x * noiseTexelSize.y;
			dither = ( 1 - ditherRate ) * dither + ditherRate;
			return dither;
		}


		float DistanceCheck18_g5( float3 WorldPos, int ArrayLength, float3 objectPosition )
		{
			float closest=10000;
			float now=0;
			for(int i=0; i<ArrayLength;i++)
			{
			  now = distance(WorldPos,positionsArray[i]);
			  if(now < closest)
			  {
			    closest = now;
			  }
			}
			return closest;
		}


		float DistanceCheck18_g6( float3 WorldPos, int ArrayLength, float3 objectPosition )
		{
			float closest=10000;
			float now=0;
			for(int i=0; i<ArrayLength;i++)
			{
			  now = distance(WorldPos,positionsArray[i]);
			  if(now < closest)
			  {
			    closest = now;
			  }
			}
			return closest;
		}


		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			return UnityEdgeLengthBasedTess (v0.vertex, v1.vertex, v2.vertex, _EdgeLength);
		}

		void vertexDataFunc( inout appdata_full v )
		{
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TextureA = i.uv_texcoord * _TextureA_ST.xy + _TextureA_ST.zw;
			float2 uv_TextureB = i.uv_texcoord * _TextureB_ST.xy + _TextureB_ST.zw;
			float3 ase_worldPos = i.worldPos;
			float2 appendResult189 = (float2(ase_worldPos.x , ase_worldPos.y));
			float mulTime167 = _Time.y * _CentreSpinSpeed;
			float cos190 = cos( mulTime167 );
			float sin190 = sin( mulTime167 );
			float2 rotator190 = mul( appendResult189 - float2( 0.5,0.5 ) , float2x2( cos190 , -sin190 , sin190 , cos190 )) + float2( 0.5,0.5 );
			float simplePerlin2D192 = snoise( i.uv_texcoord*20.0 );
			simplePerlin2D192 = simplePerlin2D192*0.5 + 0.5;
			float4 ditherCustomScreenPos147 = float4( ( rotator190 * simplePerlin2D192 ), 0.0 , 0.0 );
			float dither147 = DitherNoiseTex(ditherCustomScreenPos147, _CentreNoise, _CentreNoise_TexelSize);
			float3 WorldPos18_g5 = ase_worldPos;
			int ArrayLength18_g5 = ArrayLength;
			float3 objectPosition18_g5 = positionsArray[clamp(0,0,(42 - 1))].xyz;
			float localDistanceCheck18_g5 = DistanceCheck18_g5( WorldPos18_g5 , ArrayLength18_g5 , objectPosition18_g5 );
			float clampResult10_g5 = clamp( pow( ( localDistanceCheck18_g5 / _CentreRadius ) , (0.0 + (_CentreStrength - 0.0) * (100.0 - 0.0) / (1.0 - 0.0)) ) , 0.0 , 1.0 );
			dither147 = step( dither147, clampResult10_g5 );
			float TextureBlend136 = dither147;
			float4 lerpResult143 = lerp( (( _ColourDiffuseToggle )?( tex2D( _TextureA, uv_TextureA ) ):( _ColourA )) , (( _ColourDiffuseToggle )?( tex2D( _TextureB, uv_TextureB ) ):( _ColourB )) , TextureBlend136);
			float4 TextureSwapg225 = lerpResult143;
			float4 Albedo273 = TextureSwapg225;
			o.Albedo = Albedo273.rgb;
			float2 appendResult197 = (float2(ase_worldPos.x , ase_worldPos.y));
			float mulTime198 = _Time.y * _RimSpinSpeed;
			float cos201 = cos( mulTime198 );
			float sin201 = sin( mulTime198 );
			float2 rotator201 = mul( appendResult197 - float2( 0.5,0.5 ) , float2x2( cos201 , -sin201 , sin201 , cos201 )) + float2( 0.5,0.5 );
			float simplePerlin2D202 = snoise( i.uv_texcoord*20.0 );
			simplePerlin2D202 = simplePerlin2D202*0.5 + 0.5;
			float4 ditherCustomScreenPos208 = float4( ( rotator201 * simplePerlin2D202 ), 0.0 , 0.0 );
			float dither208 = DitherNoiseTex(ditherCustomScreenPos208, _RimNoise, _RimNoise_TexelSize);
			float3 WorldPos18_g6 = ase_worldPos;
			int ArrayLength18_g6 = ArrayLength;
			float3 objectPosition18_g6 = positionsArray[clamp(0,0,(42 - 1))].xyz;
			float localDistanceCheck18_g6 = DistanceCheck18_g6( WorldPos18_g6 , ArrayLength18_g6 , objectPosition18_g6 );
			float clampResult10_g6 = clamp( pow( ( localDistanceCheck18_g6 / (( _AutoRadius )?( ( _CentreRadius + _RadiusOffset ) ):( _RimRadius )) ) , (0.0 + (_RimStrength - 0.0) * (100.0 - 0.0) / (1.0 - 0.0)) ) , 0.0 , 1.0 );
			dither208 = step( dither208, clampResult10_g6 );
			float TextureBlendRim209 = dither208;
			float4 lerpResult174 = lerp( _RimColour , float4( 0,0,0,1 ) , TextureBlendRim209);
			float4 Emission227 = lerpResult174;
			o.Emission = Emission227.rgb;
			float lerpResult129 = lerp( (( _AppearDisappearToggle )?( 0.0 ):( 1.0 )) , ( 1.0 - (( _AppearDisappearToggle )?( 0.0 ):( 1.0 )) ) , TextureBlend136);
			float Opacity234 = (( _TransparentTextureSwapToggle )?( lerpResult129 ):( 1.0 ));
			o.Alpha = Opacity234;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows vertex:vertexDataFunc tessellate:tessFunction 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 4.6
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18701
2044;138;1248;659;773.4185;775.3892;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;58;-2282.588,679.2255;Inherit;False;Property;_CentreRadius;Centre Radius;13;0;Create;True;0;0;False;0;False;5.588235;7;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;267;-1817.36,-243.9437;Inherit;False;1508.354;883.549;;15;188;194;57;189;167;193;190;65;192;262;191;148;147;136;288;//centre.effect;1,1,1,1;0;0
Node;AmplifyShaderEditor.WireNode;291;-1971.084,687.6306;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;290;-1933.048,-110.6332;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;194;-1767.36,319.6056;Inherit;False;Property;_CentreSpinSpeed;Centre Spin Speed;14;0;Create;True;0;0;False;0;False;0;0.1;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;188;-1442.306,168.9826;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.CommentaryNode;268;-1817.249,678.0635;Inherit;False;1505.082;883.5493;;16;196;195;198;199;200;197;202;203;201;204;264;206;205;208;209;287;//rim.effect;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;57;-1743.07,-108.1824;Inherit;False;Property;_CentreStrength;Centre Strength;12;0;Create;True;0;0;False;0;False;1;0.533;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;289;-1896.543,-147.1377;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;189;-1253.381,205.6601;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;193;-1475.36,392.6057;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;167;-1424.084,317.8784;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;292;-2283.97,766.454;Inherit;False;Property;_RadiusOffset;Radius Offset;18;0;Create;True;0;0;False;0;False;5.588235;0.1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;196;-1767.249,1241.611;Inherit;False;Property;_RimSpinSpeed;Rim Spin Speed;20;0;Create;True;0;0;False;0;False;0;-0.1;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;195;-1442.195,1090.989;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RotatorNode;190;-1125.472,209.4898;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TFHCRemapNode;65;-1460.226,-101.5137;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;100;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;192;-1161.36,380.6057;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;20;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;288;-1288.137,-142.2702;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;262;-1216.818,-86.59016;Inherit;False;DistanceBlendObjectArray;-1;;5;5b9aec6b10979884f8f92cd3b2f819e8;0;2;20;FLOAT;0;False;21;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;293;-1941.648,747.9286;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;191;-940.3604,208.6053;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;200;-1742.959,813.8244;Inherit;False;Property;_RimStrength;Rim Strength;16;0;Create;True;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;204;-1754.009,716.4543;Inherit;False;Property;_RimRadius;Rim Radius;19;0;Create;True;0;0;False;0;False;5.588235;7.1;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;199;-1475.249,1314.611;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;198;-1423.973,1239.885;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;197;-1253.27,1127.667;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;148;-1169.774,14.67923;Inherit;True;Property;_CentreNoise;Centre Noise;15;0;Create;True;0;0;False;0;False;None;e28dc97a9541e3642a48c0e3886688c5;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.ToggleSwitchNode;287;-1454.088,719.8724;Inherit;False;Property;_AutoRadius;Auto Radius;17;0;Create;True;0;0;False;0;False;1;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;270;-1818.927,-1157.202;Inherit;False;1155.727;841.7297;;9;142;127;141;9;140;125;144;143;225;//albedo.options;1,1,1,1;0;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;202;-1161.249,1302.611;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;20;False;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;201;-1125.361,1131.495;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TFHCRemapNode;203;-1460.115,820.4925;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;100;False;1;FLOAT;0
Node;AmplifyShaderEditor.DitheringNode;147;-810.8867,39.43589;Inherit;False;2;True;3;0;FLOAT;0;False;1;SAMPLER2D;;False;2;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;127;-1754.737,-925.754;Inherit;True;Property;_TextureA;Texture A;7;0;Create;True;0;0;False;0;False;-1;None;b1a5daa50945a474ba9de7147bd4673c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;141;-1686.506,-726.9196;Inherit;False;Property;_ColourB;Colour B;8;0;Create;True;0;0;False;0;False;1,0,0,1;0.8901961,0.882353,0.5921569,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ToggleSwitchNode;132;373.7321,1157.08;Inherit;False;Property;_AppearDisappearToggle;Appear/Disappear Toggle;10;0;Create;True;0;0;False;0;False;1;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;142;-1768.927,-545.4713;Inherit;True;Property;_TextureB;Texture B;9;0;Create;True;0;0;False;0;False;-1;None;10c00c89807f81d40995e2b8c273b526;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;9;-1672.315,-1107.202;Inherit;False;Property;_ColourA;Colour A;6;0;Create;True;0;0;False;0;False;1,0,0,1;0.6627451,0.6588235,0.4352942,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;205;-940.2498,1130.611;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;206;-1169.663,936.6856;Inherit;True;Property;_RimNoise;Rim Noise;21;0;Create;True;0;0;False;0;False;None;e28dc97a9541e3642a48c0e3886688c5;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.FunctionNode;264;-1223.619,798.5704;Inherit;False;DistanceBlendObjectArray;-1;;6;5b9aec6b10979884f8f92cd3b2f819e8;0;2;20;FLOAT;0;False;21;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;136;-533.0062,58.73882;Inherit;False;TextureBlend;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;144;-1348.031,-603.5347;Inherit;False;136;TextureBlend;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;138;652.7852,1157.008;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;140;-1408.898,-711.8849;Inherit;False;Property;_ColourDiffuseToggle;Colour/Diffuse Toggle;5;0;Create;True;0;0;False;0;False;1;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ToggleSwitchNode;125;-1406.189,-950.0889;Inherit;False;Property;_ColourDiffuseToggle;Colour/Diffuse Toggle;12;0;Create;True;0;0;False;0;False;1;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.DitheringNode;208;-810.7762,961.4426;Inherit;False;2;True;3;0;FLOAT;0;False;1;SAMPLER2D;;False;2;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;271;-631.8734,-1163.694;Inherit;False;690.801;339.4798;;4;175;210;174;227;//emission;1,1,1,1;0;0
Node;AmplifyShaderEditor.LerpOp;143;-1077.594,-728.2222;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;1,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;134;667.4106,1182.912;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;139;782.7852,1159.008;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;137;629.519,1255.645;Inherit;False;136;TextureBlend;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;209;-549.1675,968.8975;Inherit;False;TextureBlendRim;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;175;-581.8735,-1113.694;Inherit;False;Property;_RimColour;Rim Colour;22;0;Create;True;0;0;False;0;False;1,1,1,1;0.1792453,0.1792453,0.1792453,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;210;-581.3884,-940.2147;Inherit;False;209;TextureBlendRim;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;225;-887.1997,-707.3795;Inherit;False;TextureSwapg;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;274;-1819.635,-1653.015;Inherit;False;1424.382;416.7797;;2;226;273;//albedo;1,1,1,1;0;0
Node;AmplifyShaderEditor.LerpOp;129;835.7427,1155.889;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;174;-342.6014,-1106.791;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,1;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;226;-1249.421,-1603.015;Inherit;False;225;TextureSwapg;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.ToggleSwitchNode;146;998.394,1124.834;Inherit;True;Property;_TransparentTextureSwapToggle;Transparent/Texture Swap Toggle;11;0;Create;True;0;0;False;0;False;1;2;0;FLOAT;1;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;227;-165.0724,-1096.987;Inherit;False;Emission;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;234;1317.907,1125.477;Inherit;False;Opacity;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;269;-1806.312,1645.073;Inherit;False;1134.144;349.4301;;5;245;249;248;263;255;//settle.effect;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;273;-1039.305,-1590.711;Inherit;False;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;229;1285.059,-23.42868;Inherit;False;227;Emission;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;249;-1562.199,1695.073;Inherit;False;Property;_SettleRadius;Settle Radius;24;0;Create;True;0;0;False;0;False;5.588235;2.81;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;263;-1236.971,1765.58;Inherit;False;DistanceBlendObjectArray;-1;;7;5b9aec6b10979884f8f92cd3b2f819e8;0;2;20;FLOAT;0;False;21;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;245;-1756.312,1780.834;Inherit;False;Property;_SettleStrength;Settle Strength;23;0;Create;True;0;0;False;0;False;1;0.029;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;255;-930.1041,1770.926;Inherit;False;EffectRadius;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;248;-1473.468,1787.503;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;100;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;258;243.5149,-630.3052;Inherit;False;Property;_CustomDisplacementTextureEnable;Custom Displacement Texture Enable;27;0;Create;True;0;0;False;0;False;0;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;154;-601.2546,-644.0463;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;155;-308.6161,-440.1804;Inherit;False;Property;_VoronoiScale;Voronoi Scale;25;0;Create;True;0;0;False;0;False;10;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;161;-542.7513,-400.4997;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;159;-316.4485,-557.1973;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WireNode;286;-342.7361,-642.8582;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;257;-73.28545,-520.2057;Inherit;True;Property;_DisplacementTexture;Displacement Texture;28;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;160;-552.9503,-520.6652;Inherit;False;Property;_VoronoiSpeed;Voronoi Speed;29;0;Create;True;0;0;False;0;False;0.1,0;0.1,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.VoronoiNode;153;24.01963,-666.9143;Inherit;False;0;0;1;0;1;False;1;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;3;FLOAT;0;FLOAT;1;FLOAT2;2
Node;AmplifyShaderEditor.GetLocalVarNode;272;1279.997,-116.551;Inherit;False;273;Albedo;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;281;-147.0828,-711.6973;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;-10;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;285;-615.7717,-723.1871;Inherit;False;Property;_VoronoiAngleChangeSpeed;Voronoi Angle Change Speed;26;0;Create;True;0;0;False;0;False;6;3.31;1;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;230;532.2796,-630.0092;Inherit;True;Voronoise;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;284;-105.7717,-537.1871;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;283;-318.8966,-716.8083;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;235;1286.341,73.4308;Inherit;False;234;Opacity;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1547.272,-65.79855;Float;False;True;-1;6;ASEMaterialInspector;0;0;Standard;BaneArt/FogofWar;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;1;True;True;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;2;13.6;10;25;False;0.582;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;0;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;291;0;58;0
WireConnection;290;0;291;0
WireConnection;289;0;290;0
WireConnection;189;0;188;1
WireConnection;189;1;188;2
WireConnection;167;0;194;0
WireConnection;190;0;189;0
WireConnection;190;2;167;0
WireConnection;65;0;57;0
WireConnection;192;0;193;0
WireConnection;288;0;289;0
WireConnection;262;20;288;0
WireConnection;262;21;65;0
WireConnection;293;0;58;0
WireConnection;293;1;292;0
WireConnection;191;0;190;0
WireConnection;191;1;192;0
WireConnection;198;0;196;0
WireConnection;197;0;195;1
WireConnection;197;1;195;2
WireConnection;287;0;204;0
WireConnection;287;1;293;0
WireConnection;202;0;199;0
WireConnection;201;0;197;0
WireConnection;201;2;198;0
WireConnection;203;0;200;0
WireConnection;147;0;262;0
WireConnection;147;1;148;0
WireConnection;147;2;191;0
WireConnection;205;0;201;0
WireConnection;205;1;202;0
WireConnection;264;20;287;0
WireConnection;264;21;203;0
WireConnection;136;0;147;0
WireConnection;138;0;132;0
WireConnection;140;0;141;0
WireConnection;140;1;142;0
WireConnection;125;0;9;0
WireConnection;125;1;127;0
WireConnection;208;0;264;0
WireConnection;208;1;206;0
WireConnection;208;2;205;0
WireConnection;143;0;125;0
WireConnection;143;1;140;0
WireConnection;143;2;144;0
WireConnection;134;0;132;0
WireConnection;139;0;138;0
WireConnection;209;0;208;0
WireConnection;225;0;143;0
WireConnection;129;0;139;0
WireConnection;129;1;134;0
WireConnection;129;2;137;0
WireConnection;174;0;175;0
WireConnection;174;2;210;0
WireConnection;146;1;129;0
WireConnection;227;0;174;0
WireConnection;234;0;146;0
WireConnection;273;0;226;0
WireConnection;248;0;245;0
WireConnection;258;0;153;0
WireConnection;258;1;257;0
WireConnection;159;0;154;0
WireConnection;159;2;160;0
WireConnection;159;1;161;0
WireConnection;286;0;285;0
WireConnection;257;1;159;0
WireConnection;153;0;159;0
WireConnection;153;1;281;0
WireConnection;153;2;284;0
WireConnection;281;0;283;0
WireConnection;281;1;286;0
WireConnection;230;0;258;0
WireConnection;284;0;155;0
WireConnection;0;0;272;0
WireConnection;0;2;229;0
WireConnection;0;9;235;0
ASEEND*/
//CHKSM=D968F74D4A11414C13C53539F268CABFB6B77339