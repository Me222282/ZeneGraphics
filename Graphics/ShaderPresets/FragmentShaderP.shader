#version 430 core

layout(location = 0) out vec4 colour;

in vec2 tex_Coords;

uniform sampler2D uTextureSlot;

uniform int screenWidth;
uniform int screenHeight;

uniform vec2 bitCrush;
uniform bool crushBit;

uniform bool greyScale;
uniform bool invertedColour;

uniform bool useKernel;
uniform float[9] kernel;
uniform float kernelOffset = 700.0;

void main()
{
    vec2 coords = tex_Coords;

    if (crushBit)
    {
        float x = tex_Coords.x;

        x = floor(x * bitCrush.x) / bitCrush.x;

        float y = tex_Coords.y;

        y = floor(y * bitCrush.y) / bitCrush.y;

        coords = vec2(x, y);
    }

    if (useKernel)
    {
        float offset = 1.0 / kernelOffset;

        vec2 offsets[9] = vec2[](
            vec2(-offset, offset),  // top-left
            vec2(0.0f, offset),     // top-center
            vec2(offset, offset),   // top-right
            vec2(-offset, 0.0f),    // center-left
            vec2(0.0f, 0.0f),       // center-center
            vec2(offset, 0.0f),     // center-right
            vec2(-offset, -offset), // bottom-left
            vec2(0.0f, -offset),    // bottom-center
            vec2(offset, -offset)   // bottom-right    
        );

        vec3 col = vec3(0.0);
        for (int i = 0; i < 9; i++)
        {
            col += vec3(texture(uTextureSlot, coords.st + offsets[i])) * kernel[i];
        }

        colour = vec4(col, 1.0);
    }
    else
    {
        colour = texture(uTextureSlot, coords);
    }

    if (greyScale)
    {
        float average = 0.2126 * colour.r + 0.7152 * colour.g + 0.0722 * colour.b;
        colour = vec4(average, average, average, 1.0);
    }

    if (invertedColour)
    {
        colour = vec4(1 - colour.rgb, 1.0);
    }
}