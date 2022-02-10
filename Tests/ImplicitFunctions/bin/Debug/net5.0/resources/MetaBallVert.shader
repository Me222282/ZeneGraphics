#version 330 core

layout(location = 0) in vec3 vPosition;
layout(location = 1) in vec2 texCoord;
layout(location = 2) in vec3 vColour;

out vec2 if_Location;
out vec3 colour_Interp;

uniform vec2 scale;
uniform vec2 offset;

void main()
{
	colour_Interp = vColour;

	if_Location = offset + (texCoord * scale);

	gl_Position = vec4(vPosition, 1);
}