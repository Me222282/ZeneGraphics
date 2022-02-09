#version 330 core

layout(location = 0) out vec4 colour;

in vec4 pos_Colour;

uniform float saturation;

void main()
{
	colour = new vec4(pos_Colour.x + saturation, pos_Colour.y + saturation, pos_Colour.z + saturation, pos_Colour.w + saturation);
	//colour = new vec4(1.0f, 0.5f, 0.0f, 1.0f);
}