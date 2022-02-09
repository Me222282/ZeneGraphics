#version 330 core

layout(location = 0) in vec3 vPosition;
layout(location = 1) in vec2 texCoord;

out vec2 tex_Coords;

uniform mat4 matrix;

void main()
{
	vec4 newTexCoords = matrix * vec4(texCoord, 1, 1);
	tex_Coords = vec2(newTexCoords.x, newTexCoords.y);

	gl_Position = vec4(vPosition, 1);
}