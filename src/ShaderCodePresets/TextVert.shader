#version 330 core

layout(location = 1) in vec3 vPosition;
layout(location = locations.texture) in vec2 texCoord;
// Instance data
layout(location = 3) in vec2 offset;
layout(location = 6) in vec2 size;
layout(location = 4) in vec2 texOffset;
layout(location = 5) in vec2 texSize;
layout(location = 7) in vec4 colour;

out vec2 tex_Coords;
out vec4 charColour;

uniform mat4 matrix;

void main()
{
	charColour = colour;
	tex_Coords = (texCoord * texSize) + texOffset;

	gl_Position = matrix * vec4((vPosition.xy * size) + offset, vPosition.z, 1);
}