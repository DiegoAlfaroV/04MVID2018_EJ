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

struct DirLight {
    vec3 direction;
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

uniform DirLight dirLight;

struct PointLight {
    vec3 position;
    
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;

    float constant;
    float linear;
    float quadratic;
};

#define NUMBER_POINT_LIGHTS 2
uniform PointLight pointLights[NUMBER_POINT_LIGHTS];

uniform vec3 viewPos;

vec3 calcDirectionalLight(DirLight light, vec3 norm, vec3 viewDir) {
    vec3 ambient = light.ambient * vec3(texture(material.diffuse, texCoords));
    
    vec3 lightDir = normalize(-light.direction);
    float diff = max(dot(norm, lightDir) , 0.0);
    vec3 diffuse = diff  * vec3(texture(material.diffuse, texCoords)) * light.diffuse;

    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir) ,0.0), material.shininess);
    vec3 specular = spec * vec3(texture(material.specular, texCoords)) * light.specular;

    return ambient + diffuse + specular;
}

vec3 calcPointLight(PointLight light, vec3 norm, vec3 fragPos, vec3 viewDir) {
    float distance = length(light.position - fragPos);
    float attenuation = 1.0 / (light.constant +
				light.linear * distance +
				light.quadratic * (distance * distance));

    vec3 ambient = light.ambient * vec3(texture(material.diffuse, texCoords));
    
    vec3 lightDir = normalize(light.position - fragPos);
    float diff = max(dot(norm, lightDir) , 0.0);
    vec3 diffuse = diff  * vec3(texture(material.diffuse, texCoords)) * light.diffuse;

    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir) ,0.0), material.shininess);
    vec3 specular = spec * vec3(texture(material.specular, texCoords)) * light.specular;

    return (ambient + diffuse + specular) *attenuation;
}

void main() {
   vec3 norm = normalize(normal);
   vec3 viewDir = normalize(viewPos - fragPos);

   vec3 color = calcDirectionalLight(dirLight, norm, viewDir);

   for (int i = 0; i < NUMBER_POINT_LIGHTS; i++) {
	color += calcPointLight(pointLights[i], norm, fragPos, viewDir);
   }

   fragColor = vec4(color, 1.0);
}