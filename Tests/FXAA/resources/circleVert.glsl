#version 330 core

layout(location = 0) in vec3 vPosition;
layout(location = 2) in vec2 vTex;

out vec2 pos;

uniform mat4 matrix;
uniform float size;

void main()
{
    pos = (vTex - 0.5) * size;
    
	gl_Position = matrix * vec4(vPosition, 1);
}