#version 330 core

layout(location = locations.vertex) in vec3 vPosition;
layout(location = locations.colour) in vec4 colour;
layout(location = locations.texture) in vec2 texCoord;
layout(location = locations.noraml) in vec3 vNormal;

layout(location = 6) in vec3 ambientLight;
layout(location = 7) in vec2 ambientTexture;
layout(location = 8) in vec3 specularLight;
layout(location = 9) in vec2 specularTexture;

layout(location = locations.norm_tex) in vec2 normalTexCoord;
layout(location = locations.tangent) in vec3 vTangent;

out vec4 pos_Colour;
out vec2 tex_Coords;

out vec2 tex_ambCoords;
out vec3 pos_ambColour;
out vec2 tex_specCoords;
out vec3 pos_specColour;

out vec2 normal_Coords;
out vec3 normal;
out mat3 TBN;
out vec3 fragPos;
out vec4 lightSpacePos;

uniform mat4 modelM;
uniform mat4 vpM;

uniform mat4 lightSpaceMatrix;

void main()
{
	//mat3 nMat = mat3(transpose(inverse(matrix1)));
	//normal = normalize(nMat * vNormal);
	//vec3 T = normalize(nMat * vTangent);
	//vec3 B = normalize(nMat * vBitangent);
	vec3 T = normalize(vec3(modelM * vec4(vTangent, 0.0)));
	normal = normalize(vec3(modelM * vec4(vNormal, 0.0)));
	vec3 B = normalize(cross(normal, T));
	TBN = mat3(T, B, normal);

	normal_Coords = normalTexCoord;

	fragPos = vec3(modelM * vec4(vPosition, 1.0));

	pos_Colour = colour;

	pos_ambColour = ambientLight;
	tex_ambCoords = ambientTexture;
	pos_specColour = specularLight;
	tex_specCoords = specularTexture;

	tex_Coords = texCoord;

	lightSpacePos = lightSpaceMatrix * vec4(fragPos, 1.0);

	gl_Position = vpM * vec4(fragPos, 1.0);
}