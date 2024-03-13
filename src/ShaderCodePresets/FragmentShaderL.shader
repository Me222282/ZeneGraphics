#version 430 core

layout(location = 0) out vec4 colour;

in vec4 pos_Colour;
in vec2 tex_Coords;
in vec2 normal_Coords;
in vec3 normal;
in mat3 TBN;
in vec3 fragPos;
in vec4 lightSpacePos;

in vec2 tex_ambCoords;
in vec3 pos_ambColour;
in vec2 tex_specCoords;
in vec3 pos_specColour;

uniform bool normalMapping;
uniform sampler2D uNormalMap;

uniform int colourType;
uniform vec4 uColour;
uniform sampler2D uTextureSlot;

struct Material
{
	int DiffuseLightSource;
	vec3 DiffuseLight;
	sampler2D DiffTextureSlot;

	int SpecularLightSource;
	vec3 SpecularLight;
	sampler2D SpecTextureSlot;

	int Shine;
};

struct Light
{
	vec3 LightColour;
	vec3 AmbientLight;
	vec4 LightVector;

	float Linear;
	float Quadratic;
};

uniform bool ingorBlackLight;

layout(location = 12) uniform Light lights[##dp##];

struct SpotLight
{
	vec3 Colour;
	vec3 Position;
	vec3 Direction;

	float CosInner;
	float CosOuter;
	float Linear;
	float Quadratic;
};

layout(location = ##sl##) uniform SpotLight spotLights[##s##];

uniform Material uMaterial;

uniform bool drawLight;

uniform vec3 ambientLight;

uniform vec3 cameraPos;

uniform sampler2D uShadowMapSlot;

vec3 Lighting(Light light);

vec3 SpotLighting(SpotLight light);

float Shadow(vec3 normal, vec3 lightDir);

vec3 norm;

void main()
{
	//colour = vec4(vec3((texture(uDepthTexSlot, tex_Coords).r * 5f) - 4f), 1.0);

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

	if (drawLight && colour.a != 0.0)
	{
		if (normalMapping)
		{
			vec3 map = texture(uNormalMap, normal_Coords).rgb;
			map = (map * 2.0) - 1.0;
			//norm = normalize(TBN * map);
			norm = (TBN[0] * map.r) + (TBN[1] * map.g) + (TBN[2] * map.b);
		}
		else { norm = normalize(normal); }

		vec3 light = ambientLight;

		int nLights = lights.length();

		for (int i = 0; i < nLights; i++)
		{
			if (lights[i].LightColour == vec3(0, 0, 0) && ingorBlackLight) { continue; }

			light += Lighting(lights[i]);
		}

		int nSLights = spotLights.length();

		for (int i = 0; i < nSLights; i++)
		{
			if (spotLights[i].Colour == vec3(0, 0, 0) && ingorBlackLight) { continue; }

			light += SpotLighting(spotLights[i]);
		}

		colour *= vec4(light, 1);
	}
}

vec3 Lighting(Light light)
{
	vec3 diffusedLight;

	switch (uMaterial.DiffuseLightSource)
	{
		case 4:
			diffusedLight = vec3(0.0, 0.0, 0.0);
			break;

		case 1:
			diffusedLight = uMaterial.DiffuseLight;
			break;

		case 2:
			diffusedLight = pos_ambColour;
			break;

		case 3:
			diffusedLight = vec3(texture(uMaterial.DiffTextureSlot, tex_ambCoords));
			break;

		default:
			diffusedLight = vec3(1.0, 1.0, 1.0);
			break;
	}

	// Ambient Light
	vec3 lightAmbient = diffusedLight * light.AmbientLight;

	// Diffuse Light
	vec3 lightDir;

	if (light.LightVector.w == 0) { lightDir = normalize(vec3(light.LightVector)); }
	else { lightDir = normalize(vec3(light.LightVector) - fragPos); }

	vec3 diffuse = vec3(0, 0, 0);
	if (uMaterial.DiffuseLightSource != 4)
	{
		float diff = max(dot(norm, lightDir), 0.0);
		diffuse = diffusedLight * diff * light.LightColour;
	}

	vec3 specular = vec3(0, 0, 0);
	if (uMaterial.SpecularLightSource != 4)
	{
		vec3 specularedLight;

		switch (uMaterial.SpecularLightSource)
		{
			case 4:
				specularedLight = vec3(0.0, 0.0, 0.0);
				break;

			case 1:
				specularedLight = uMaterial.SpecularLight;
				break;

			case 2:
				specularedLight = pos_specColour;
				break;

			case 3:
				specularedLight = vec3(texture(uMaterial.SpecTextureSlot, tex_specCoords));
				break;

			default:
				specularedLight = vec3(0.5, 0.5, 0.5);
				break;
		}

		// Specular Light
		vec3 viewDir = normalize(cameraPos - fragPos);
		vec3 halfwayDir = normalize(lightDir + viewDir);

		float spec = pow(max(dot(norm, halfwayDir), 0.0), uMaterial.Shine);
		specular = specularedLight * spec * light.LightColour;
	}

	float attenuation = 1;

	if (light.LightVector.w != 0)
	{
		float distance = length(vec3(light.LightVector) - fragPos);
		attenuation = 1.0 / (1 + light.Linear * distance +
			light.Quadratic * (distance * distance));
	}

	return (lightAmbient + ((diffuse + specular) * Shadow(norm, lightDir))) * attenuation;
}

vec3 SpotLighting(SpotLight light)
{
	vec3 diffusedLight;

	switch (uMaterial.DiffuseLightSource)
	{
		case 4:
			diffusedLight = vec3(0.0, 0.0, 0.0);
			break;

		case 1:
			diffusedLight = uMaterial.DiffuseLight;
			break;

		case 2:
			diffusedLight = pos_ambColour;
			break;

		case 3:
			diffusedLight = vec3(texture(uMaterial.DiffTextureSlot, tex_ambCoords));
			break;

		default:
			diffusedLight = vec3(1.0, 1.0, 1.0);
			break;
	}

	// Diffuse Light
	vec3 lightDir;

	lightDir = normalize(light.Position - fragPos);

	float theta = dot(lightDir, normalize(light.Direction));

	// Is fragment outside of spot light
	if (theta <= light.CosOuter) { return vec3(0, 0, 0); }

	float intensity = 1;

	if (theta < light.CosInner)
	{
		float epsilon = light.CosInner - light.CosOuter;
		intensity = smoothstep(0.0, 1.0, (theta - light.CosOuter) / epsilon);
	}

	vec3 diffuse = vec3(0, 0, 0);
	if (uMaterial.DiffuseLightSource != 4)
	{
		float diff = max(dot(norm, lightDir), 0.0);
		diffuse = diffusedLight * diff * light.Colour;
	}

	vec3 specular = vec3(0, 0, 0);
	if (uMaterial.SpecularLightSource != 4)
	{
		vec3 specularedLight;

		switch (uMaterial.SpecularLightSource)
		{
			case 1:
				specularedLight = uMaterial.SpecularLight;
				break;

			case 2:
				specularedLight = pos_specColour;
				break;

			case 3:
				specularedLight = vec3(texture(uMaterial.SpecTextureSlot, tex_specCoords));
				break;

			default:
				specularedLight = vec3(0.5, 0.5, 0.5);
				break;
		}

		// Specular Light
		vec3 viewDir = normalize(cameraPos - fragPos);
		vec3 halfwayDir = normalize(lightDir + viewDir);

		float spec = pow(max(dot(norm, halfwayDir), 0.0), uMaterial.Shine);
		specular = specularedLight * spec * light.Colour;
	}

	float distance = length(light.Position - fragPos);
	float attenuation = 1.0 / (1 + light.Linear * distance +
		light.Quadratic * (distance * distance));

	return (((diffuse + specular) * Shadow(norm, lightDir)) * attenuation) * intensity;
}

float Shadow(vec3 normal, vec3 lightDir)
{
	vec3 projCoords = lightSpacePos.xyz / lightSpacePos.w;
	projCoords = (projCoords * 0.5) + 0.5;

	// No change if outside the range of the depth map
	if (projCoords.z > 1.0) { return 1; }

	float closestDepth = texture(uShadowMapSlot, projCoords.xy).r;

	//float bias = min(0.05 * (1.0 - dot(normal, lightDir)), 0.005);

	float bias = 0.000005;

	//float bias = 0.0005;

	//float bias = 0;

	//return projCoords.z > closestDepth ? 0.0 : 1.0;
	//return (projCoords.z - bias) > closestDepth ? 0.0 : 1.0;

	float shadow = 0.0;
	vec2 texelSize = 1.0 / textureSize(uShadowMapSlot, 0);
	for (int x = -1; x <= 1; ++x)
	{
		for (int y = -1; y <= 1; ++y)
		{
			float pcfDepth = texture(uShadowMapSlot, projCoords.xy + vec2(x, y) * texelSize).r;
			shadow += projCoords.z - bias > pcfDepth ? 1.0 : 0.0;
		}
	}

	return 1.0 - (shadow / 9.0);
}