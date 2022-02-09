#version 330 core

layout(location = 0) out vec4 colour;

in vec2 if_Location;

bool ImplicitFunc(float x, float y)
{
	return ((0.001 * sqrt(abs(x) / abs(y))) - pow(abs(x), -2))
		* sqrt(abs(x) / (abs(x) - (0.01 * pow(y, 2))))
		* sqrt(abs(x) / (35 - abs(x)))
		< 0;
}

void main()
{
	if (ImplicitFunc(if_Location.x, if_Location.y))
	{
		colour = vec4(1);
		return;
	}

	colour = vec4(0);
}