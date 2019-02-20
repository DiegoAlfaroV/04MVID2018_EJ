#version 330 core

out vec4 fragColor;

in vec3 normal;
in vec3 fragPos;
in vec2 texCoords;

struct Material {
    sampler2D diffuse;
    sampler2D specular;
    float shininess;
};

uniform Material material;

struct Light {
    vec3 position;
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

uniform Light light;

uniform vec3 viewPos;

void main() {
    vec3 ambient = light.ambient * vec3(texture(material.diffuse, texCoords));
    
    vec3 norm = normalize(normal);
    vec3 lightDir = normalize(light.position - fragPos);
    float diff = max(dot(norm, lightDir) , 0.0);
    vec3 diffuse = diff  * vec3(texture(material.diffuse, texCoords)) * light.diffuse;

    vec3 viewDir = normalize(viewPos - fragPos);
    vec3 halfwayDir = normalize(lightDir + viewDir);
    float spec = pow(max(dot(lightDir, halfwayDir) ,0.0), material.shininess);
    vec3 specular = spec * vec3(texture(material.specular, texCoords)) * light.specular;

    vec3 phong = ambient + diffuse + specular;
	fragColor = vec4(phong, 1.0);
}