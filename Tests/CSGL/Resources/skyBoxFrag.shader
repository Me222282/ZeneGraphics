#version 330 core
out vec4 Colour;

in vec3 TexCoords;

uniform samplerCube skybox;

void main()
{
    Colour = texture(skybox, TexCoords);
}
