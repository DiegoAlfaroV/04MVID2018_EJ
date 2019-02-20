#version 330 core

out vec4 fragColor;

in vec3 normal;
in vec3 fragPos;

uniform vec3 objectColor;
uniform vec3 lightColor;
uniform vec3 lightPos;

void main() {
    vec3 norm = normalize(normal);
    vec3 lightDir = normalize(lightPos - fragPos);
    float diff = max(dot(norm, lightDir) , 0.0);
    vec3 diffuse = diff * lightColor;

    vec3 color = diffuse * objectColor;
    fragColor = vec4(color, 1.0);
}