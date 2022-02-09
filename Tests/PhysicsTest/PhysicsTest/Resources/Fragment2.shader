#version 430 core

layout(location = 0) out vec4 colour;

in vec2 tex_Coords;

uniform sampler2D uTextureSlot;

void main()
{
	colour = texture(uTextureSlot, tex_Coords);
}