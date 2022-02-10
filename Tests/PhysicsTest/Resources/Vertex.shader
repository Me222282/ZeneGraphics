#version 330 core

layout(location = 0) in vec3 vPosition;
layout(location = 1) in vec2 texCoord;
layout(location = 2) in mat4 instanceModel;
layout(location = 6) in vec3 instanceColour;

out vec2 tex_Coords;
out vec3 inst_Colour;

uniform mat4 uProj_View;

void main()
{
	inst_Colour = instanceColour;

	tex_Coords = texCoord;

	gl_Position = uProj_View * instanceModel * vec4(vPosition, 1);
}