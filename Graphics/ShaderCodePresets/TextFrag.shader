#version 330 core

layout(location = 0) out vec4 colour;

in vec2 tex_Coords;

uniform sampler2D uTextureSlot;
uniform vec4 uColour;

void main()
{
	// Get texel
	vec4 tex = texture(uTextureSlot, tex_Coords);
	// Use texel as opacity
	colour = vec4(uColour.rgb, uColour.a * tex.r);
}