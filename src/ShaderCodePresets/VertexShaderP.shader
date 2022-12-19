#version 330 core

layout(location = locations.vertex) in vec2 vPosition;
layout(location = locations.texture) in vec2 texCoord;

out vec2 tex_Coords;

void main()
{
	tex_Coords = texCoord;

	gl_Position = vec4(vPosition * vec2(2.0), 0, 1);
}