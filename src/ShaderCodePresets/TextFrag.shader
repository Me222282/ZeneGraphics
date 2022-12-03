#version 330 core

/***********************************************************************
*
* Copyright (c) 2019-2022 Barbara Geller
* Copyright (c) 2019-2022 Ansel Sermersheim
*
* This file is part of CsPaint.
*
* CsPaint is free software, released under the BSD 2-Clause license.
* For license details refer to LICENSE provided with this project.
*
* CopperSpice is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
*
* https://opensource.org/licenses/BSD-2-Clause
*
***********************************************************************/

layout(location = 0) out vec4 outColour;

//in vec3 inColour;
in vec2 tex_Coords;
in vec4 charColour;
uniform vec4 uColour;

uniform sampler2D fontSampler;

float median(float r, float g, float b)
{
    return max(min(r, g), min(max(r, g), b));
}

const float smoothing = 1.0 / 64.0;
//const float smoothing = 0.0;
const float thickness = 0.25;

void main()
{
    vec3 fontSample = texture(fontSampler, tex_Coords).rgb;
    //float sigDist = median(fontSample.r, fontSample.g, fontSample.b);
    //float opacity = smoothstep(0.5 - smoothing, 0.5 + smoothing, fontSample.r);
    //float opacity = smoothstep(0.5 - smoothing, 0.5 + smoothing, sigDist);
    //float opacity = fontSample.r;

    float opacity = smoothstep(1.0 - thickness - smoothing, 1.0 - thickness + smoothing, fontSample.r);

    //if (opacity < 0.5) { discard; }

    // Insignificant
    if (opacity < 0.05) { discard; }

    //outColour = vec4(charColour.rgb, charColour.a * opacity);
    outColour = vec4(uColour.rgb, uColour.a * opacity);
}