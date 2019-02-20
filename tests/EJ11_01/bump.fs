#version 330 core

out vec4 fragColor;

in vec3 tangentLightPos;
in vec3 tangentViewPos;
in vec3 tangentFragPos;
in vec2 texCoords;

struct Material {
    sampler2D diffuse;
    sampler2D specular;
    sampler2D normal;
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

void main() {
    vec3 norm = texture(material.normal, texCoords).rgb;
    norm = normalize(norm * 2.0 - 1.0);

    vec3 color = texture(material.diffuse, texCoords).rgb;

    vec3 ambient = light.ambient * color;
    
    vec3 lightDir = normalize(tangentLightPos - tangentFragPos);
    float diff = max(dot(norm, lightDir) , 0.0);
    vec3 diffuse = diff  * color * light.diffuse;

    vec3 viewDir = normalize(tangentViewPos - tangentFragPos);
    vec3 halfwayDir = normalize(lightDir + viewDir);
    float spec = pow(max(dot(norm, halfwayDir) ,0.0), material.shininess);
    vec3 specular = spec * texture(material.specular, texCoords).rgb * light.specular;

    vec3 phong = ambient + diffuse + specular;
	fragColor = vec4(phong, 1.0);
}