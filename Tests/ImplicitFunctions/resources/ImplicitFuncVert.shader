#version 330 core

layout(location = 0) in vec3 vPosition;
layout(location = 1) in vec2 texCoord;

out vec2 if_Location;

void main()
{
	if_Location = texCoord * 70;

	gl_Position = vec4(vPosition, 1);
}