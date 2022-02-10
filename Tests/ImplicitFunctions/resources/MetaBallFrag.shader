#version 330 core

layout(location = 0) out vec4 colour;

in vec2 if_Location;
in vec3 colour_Interp;

uniform vec3 balls[@SIZE];

float ImplicitFunc(vec2 loc)
{
	float total = 0;

	for (int i = 0; i < @SIZE; i++)
	{
		total += balls[i].z / distance(loc, vec2(balls[i]));
	}

	return total;
}

void main()
{
	
	float value = ImplicitFunc(if_Location);

	if (value < 1/* || value > 1.075*/)
	{
		colour = vec4(0);
		return;
	}

	colour = vec4(colour_Interp, 1);
	/*
	float x = if_Location.x;
	float y = if_Location.y;

	// Butterfly
	float value = ((0.001 * sqrt(abs(x) / abs(y))) - pow(abs(x), -2)) * sqrt(abs(x) / (abs(x) - (0.01 * pow(abs(y), 2)))) * sqrt(abs(x) / (35 - abs(x)));

	if (value < 0)
	{
		colour = vec4(colour_Interp, 1);
		return;
	}

	colour = vec4(0);*/
}