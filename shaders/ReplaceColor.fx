sampler2D SpriteTexture : register(s0);

float4 ReplacementColor : register(c0); // The color to replace with (passed from C#)

struct PixelInput
{
    float2 TexCoord : TEXCOORD0;
};

float4 Main(PixelInput input) : COLOR
{
    return ReplacementColor; // Replace every pixel with the same color
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 Main();
    }
}