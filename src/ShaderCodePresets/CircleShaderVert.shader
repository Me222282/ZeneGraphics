#version 330 core

layout(location = locations.vertex) in vec3 vPosition;
layout(location = locations.colour) in vec4 colour;
layout(location = locations.texture) in vec2 texCoord;

out vec4 pos_Colour;
out vec2 tex_Coords;
out vec2 pos;

uniform mat4 matrix;
uniform float size;
uniform vec2 c_off;

void main()
{
    pos_Colour = colour;
    tex_Coords = texCoord;

    pos = (texCoord - c_off) * size;
    
	gl_Position = matrix * vec4(vPosition, 1);
}