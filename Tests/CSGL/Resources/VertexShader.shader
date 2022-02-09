#version 330 core

layout(location = 0) in vec4 position;
layout(location = 1) in vec4 colour;

out vec4 pos_Colour;

void main()
{
	pos_Colour = colour;

	gl_Position = position;
}