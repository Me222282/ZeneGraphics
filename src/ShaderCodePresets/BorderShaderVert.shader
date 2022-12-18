#version 330 core

layout(location = 0) in vec3 vPosition;
layout(location = 1) in vec4 colour;
layout(location = 2) in vec2 texCoord;

out vec4 pos_Colour;
out vec2 tex_Coords;

uniform mat4 matrix;
uniform vec2 size;

void main()
{
    pos_Colour = colour;

    tex_Coords = ((texCoord - 0.5) * size) + 0.5;
    
	gl_Position = matrix * vec4(vPosition, 1);
}