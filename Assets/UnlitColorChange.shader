Shader "Unlit/UnlitColorChange"
{
    Properties
    {
		_Color("Color", Color) = (0, 0, 0, 1) 
		_SecondaryColor("Secondary Color", Color) = (1,1,1,1)
		_Speed("Speed", float) = 1
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

			fixed4 _Color;
			fixed4 _SecondaryColor;
			float _Speed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				float t = (sin(_Time * _Speed) + 1) / 2;
				fixed4 col = _Color * (1 - t) + _SecondaryColor * t;

                return col;
            }
            ENDCG
        }
    }
}
