#version 330 core

layout(location = locations.vertex) in vec3 vPosition;

uniform mat4 matrix;

uniform float depthOffset;

void main()
{
	vec4 pos = (matrix) * vec4(vPosition, 1.0);

	pos.z += depthOffset;

	gl_Position = pos;
}