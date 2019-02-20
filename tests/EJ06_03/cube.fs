#version 330 core

out vec4 fragColor;

in vec3 normal;
in vec3 fragPos;
in vec3 ligPos;

uniform vec3 objectColor;
uniform vec3 lightColor;
uniform float ambientStrength;

uniform vec3 viewPos;
uniform int shineness;
uniform float specularStrength;

void main() {
    vec3 ambient = ambientStrength * lightColor;
    
    vec3 norm = normalize(normal);
    vec3 lightDir = normalize(ligPos - fragPos);
    float diff = max(dot(norm, lightDir) , 0.0);
    vec3 diffuse = diff * lightColor;

    vec3 viewDir = normalize(fragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir) ,0.0), shineness);
    vec3 specular = specularStrength * spec * lightColor;

    vec3 phong = (ambient + diffuse + specular) * objectColor;
	fragColor = vec4(phong, 1.0);
}