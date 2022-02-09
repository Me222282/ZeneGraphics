#version 330 core

layout(location = 0) in vec3 vPosition;
layout(location = 1) in vec2 texCoord;
layout(location = 2) in vec2 offset;
layout(location = 5) in vec2 size;
layout(location = 3) in vec2 texOffset;
layout(location = 4) in vec2 texSize;

out vec2 tex_Coords;

uniform mat4 matrix;

void main()
{
	tex_Coords = (texCoord * texSize) + texOffset;

	gl_Position = matrix * vec4((vPosition.xy * size) + offset, vPosition.z, 1);
}