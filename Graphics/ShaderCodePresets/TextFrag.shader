#version 330 core

layout(location = 0) out vec4 colour;

in vec2 tex_Coords;
in vec4 charColour;

uniform sampler2D uTextureSlot;
uniform vec4 uColour;
uniform int uColurSource;

void main()
{
	// Get texel
	vec4 tex = texture(uTextureSlot, tex_Coords);

	colour = vec4(uColour.rgb, uColour.a * tex.r);
	return;

	// Use texel as opacity
	switch (uColurSource)
	{
		case 1:
			colour = vec4(uColour.rgb, uColour.a * tex.r);
			return;

		case 2:
			colour = vec4(charColour.rgb, charColour.a * tex.r);
			return;

		default:
			colour = vec4(1.0, 1.0, 1.0, tex.r);
			return;
	}
}