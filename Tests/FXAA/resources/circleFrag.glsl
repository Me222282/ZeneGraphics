#version 430 core

layout(location = 0) out vec4 colour;

in vec2 pos;

uniform float minRadius;
uniform float radius;
uniform vec4 uColour;

void main()
{
    float len = (pos.x * pos.x) + (pos.y * pos.y);
    
    // Outside main circle
    if (len > radius) { discard; }
    // Inside mini circle
    if (len < minRadius) { discard; }
    
	colour = uColour;
}