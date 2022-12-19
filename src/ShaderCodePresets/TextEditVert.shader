#version 330 core

layout(location = locations.vertex) in vec3 vPosition;
layout(location = locations.texture) in vec2 texCoord;
// Instance data
layout(location = 3) in vec2 offset;
layout(location = 6) in vec2 size;
layout(location = 4) in vec2 texOffset;
layout(location = 5) in vec2 texSize;
layout(location = 7) in vec2 selected;

out vec2 tex_Coords;
out float charSelect;

uniform mat4 matrix;

void main()
{
	tex_Coords = (texCoord * texSize) + texOffset;
	
	if (selected == vec2(0))
	{
		charSelect = 0;
	}
	else
	{
		charSelect = 1;
	}

	gl_Position = matrix * vec4((vPosition.xy * size) + offset, vPosition.z, 1);
}