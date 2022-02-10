#version 330 core

layout(location = 0) out vec4 colour;

in vec2 coords;

void main()
{
	vec2 loc = vec2(coords.x - 0.5, coords.y - 0.5);
	float radiusSquared = (loc.x * loc.x) + (loc.y * loc.y);

	// Outside circle bounds
	if ((radiusSquared < -0.25) || (radiusSquared > 0.25))
	{
		colour = vec4(0);
		return;
	}

	colour = vec4(0.8, 0.5, 0.3, 1);
}