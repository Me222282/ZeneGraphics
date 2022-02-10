#version 330 core

layout(location = 0) in vec3 vPosition;
layout(location = 1) in vec2 texCoord;

out vec2 coords;

uniform mat4 matrix;

void main()
{
	coords = texCoord;

	gl_Position = matrix * vec4(vPosition, 1);
}