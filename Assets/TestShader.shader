Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _Tex1 ("Texture1", 2D) = "white" {} 
        _Tex2 ("Texture2", 2D) = "white" {} 
        _MixValue("Mix Value", Range(0,1)) = 0.5 
        _Color("Main Color", COLOR) = (1,1,1,1) 
        _Bend("Height", Range(-20,20)) = 0.5 
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert // директива для обработки вершин
            #pragma fragment frag // директива для обработки фрагментов
            #include "UnityCG.cginc" // библиотека с полезными функциями
            sampler2D _Tex1; // текстура1
            float4 _Tex1_ST;
            sampler2D _Tex2; // текстура2
            float4 _Tex2_ST;
            float _MixValue; // параметр смешивания
            float4 _Color;
            float _Bend;

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f
            {
                float2 uv : TEXCOORD0; // UV-координаты вершины
                float4 vertex : SV_POSITION; // координаты вершины
            };
            //здесь происходит обработка вершин
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
 
                // Сгибаем вершины в зависимости от _BendAmount
                float bend = sin(o.uv.x * 3.14159);
                bend *= _Bend * 0.5;
                o.vertex.y += bend;
                return o;
            }
            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 color;
                color = tex2D(_Tex1, i.uv) * _MixValue;
                color += tex2D(_Tex2, i.uv) * (1 - _MixValue);
                color = color * _Color;
                return color;
            }
            ENDCG
        }

    }
}
