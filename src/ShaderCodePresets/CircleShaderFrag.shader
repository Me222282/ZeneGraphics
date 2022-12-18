#version 430 core

layout(location = 0) out vec4 colour;

in vec4 pos_Colour;
in vec2 tex_Coords;
in vec2 pos;

uniform int colourType;
uniform vec4 uColour;
uniform sampler2D uTextureSlot;

uniform float minRadius;
uniform float radius;

void main()
{
	float len = (pos.x * pos.x) + (pos.y * pos.y);

	// Outside main circle
	if (len > radius) { discard; }
	// Inside mini circle
	if (len < minRadius) { discard; }

	switch (colourType)
	{
		case 1:
			colour = uColour;
			break;

		case 2:
			colour = pos_Colour;
			break;

		case 3:
			colour = texture(uTextureSlot, tex_Coords);
			break;

		default:
			colour = vec4(1.0, 1.0, 1.0, 1.0);
			break;
	}
}