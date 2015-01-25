Shader "VertexBlend" {
     Properties
     {
         _Color    ("Color", Color) = (0,0,0,1)
         _Layer1 ("Layer1", 2D) = "white" {}
     }
     SubShader
     {
         Pass
         {
             Fog { Mode Off }
             CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag
              
             sampler2D    _Layer1;
             float         _Color;
             
             struct appdata
             {
                 float4 vertex : POSITION;
                 fixed4 color : COLOR;
                 float2 uv_Layer1 : TEXCOORD0;
             };
              
             struct v2f
             {
                 float4 pos : SV_POSITION;
                 fixed4 color : COLOR;
                 float2 uv_Layer1 : TEXCOORD0;
             };
              
             v2f vert (appdata v)
             {
                 v2f o;
                 o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                 o.color = v.color;
				 o.uv_Layer1 = v.uv_Layer1;
                 return o;
             }
              
             float4 frag (v2f i) : COLOR
             {
                 half4 l1 = tex2D (_Layer1, i.uv_Layer1);
                  
                 float4 color = float4(1, 1, 1, 1);
                 color.rgb = l1 * i.color;
                 return color;
             }
         ENDCG
         }
     }
 }