#version 430 core

layout(location = 0) out vec4 colour;

in vec4 pos_Colour;
in vec2 tex_Coords;

uniform int colourType;
uniform vec4 uColour;
uniform vec4 uBorderColour;
uniform sampler2D uTextureSlot;

uniform float outerRadius;
uniform float halfBW;
uniform vec2 borderCrossOver;
uniform vec2 aspect;
uniform vec2 innerDMinusR;
uniform float radius;
uniform float rValue;

float squaredLength(vec2 v)
{
	return (v.x * v.x) + (v.y * v.y);
}

void insideBorders()
{
	switch (colourType)
	{
		case 1:
			colour = uColour;
			return;

		case 2:
			colour = pos_Colour;
			return;

		case 3:
			colour = texture(uTextureSlot, tex_Coords);
			return;

		default:
			colour = vec4(1.0, 1.0, 1.0, 1.0);
			return;
	}
}

void onBorders(vec2 coords)
{
	// On area between curves
	if ((coords.x > rValue &&
		coords.x < innerDMinusR.x) ||
		(coords.y > rValue &&
		coords.y < innerDMinusR.y))
	{
		colour = uBorderColour;
		return;
	}

	// On bounding curve
	if (squaredLength(vec2(rValue) - coords) > outerRadius &&
		squaredLength(vec2(rValue, innerDMinusR.y) - coords) > outerRadius &&
		squaredLength(vec2(innerDMinusR.x, rValue) - coords) > outerRadius &&
		squaredLength(innerDMinusR - coords) > outerRadius)
	{
		discard;
		return;
	}

	// On border
	colour = uBorderColour;
	return;
}

void main()
{
	vec2 coords = tex_Coords * aspect;

	// Inside box inside radius
	if (coords.x > rValue &&
		coords.y > rValue &&
		coords.x < innerDMinusR.x &&
		coords.y < innerDMinusR.y)
	{
		insideBorders();
		return;
	}

	// Ouside inner box
	if (coords.x < halfBW ||
		coords.y < halfBW ||
		coords.x > borderCrossOver.x ||
		coords.y > borderCrossOver.y)
	{
		onBorders(coords);
		return;
	}

	// On area between curves
	if ((coords.x > rValue &&
		coords.x < innerDMinusR.x) ||
		(coords.y > rValue &&
		coords.y < innerDMinusR.y))
	{
		insideBorders();
		return;
	}

	// On border curve
	if (squaredLength(vec2(rValue) - coords) > radius &&
		squaredLength(vec2(rValue, innerDMinusR.y) - coords) > radius &&
		squaredLength(vec2(innerDMinusR.x, rValue) - coords) > radius &&
		squaredLength(innerDMinusR - coords) > radius)
	{
		onBorders(coords);
		return;
	}

	insideBorders();
}