#version 430 core

layout(location = 0) in vec3 vPosition;
layout(location = 2) in vec2 vTex;

out vec2 v_texCoord;

void main(void)
{
	v_texCoord = vTex;

	gl_Position = vec4(vPosition, 1.0);
}