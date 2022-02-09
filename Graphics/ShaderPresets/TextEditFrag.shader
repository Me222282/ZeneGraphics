#version 330 core

layout(location = 0) out vec4 colour;

in vec2 tex_Coords;
in float charSelect;

uniform sampler2D uTextureSlot;
uniform vec4 uColour;

uniform vec4 uSelectColour;
uniform vec4 uSelectTextColour;

void main()
{
	// Get texel
	vec4 tex = texture(uTextureSlot, tex_Coords);
	
	if (charSelect != 0)
	{
		// Use texel as a mix between backgroud and forgroud text colours
		colour = mix(uSelectColour, uSelectTextColour, tex.r);
		return;
	}

	// Use texel as opacity
	colour = vec4(uColour.rgb, uColour.a * tex.r);
}