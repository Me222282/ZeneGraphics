#version 430 core

layout(location = 0) out vec4 colour;

in vec2 tex_Coords;
in vec3 inst_Colour;

uniform sampler2D uTextureSlot;

void main()
{
	float radius = length(tex_Coords - vec2(0.5, 0.5));
	// Outside circle bounds
	if ((radius < -0.5) || (radius > 0.5))
	{
		return;
	}

	vec4 tex = texture(uTextureSlot, tex_Coords);
	colour = vec4(tex.r * inst_Colour, 1);
}