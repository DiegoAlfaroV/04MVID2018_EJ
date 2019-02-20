#version 330 core

layout (location=0) in vec3 aPos;
layout (location=1) in vec2 aTexCoord;
layout (location=2) in vec3 aNormal;

uniform mat4 model;
uniform mat4 view;
uniform mat4 proj;
uniform mat3 normalMat;

uniform vec3 objectColor;
uniform vec3 lightColor;
uniform float ambientStrength;
uniform vec3 lightPos;

uniform vec3 viewPos;
uniform int shineness;
uniform float specularStrength;

out vec4 aFragColor;

void main() {
	vec3 normal = normalMat * aNormal;
	vec3 fragPos = vec3(model * vec4(aPos, 1.0));

	vec3 ambient = ambientStrength * lightColor;
    
    	vec3 norm = normalize(normal);
    	vec3 lightDir = normalize(lightPos - fragPos);
    	float diff = max(dot(norm, lightDir) , 0.0);
    	vec3 diffuse = diff * lightColor;

    	vec3 viewDir = normalize(viewPos - fragPos);
    	vec3 reflectDir = reflect(-lightDir, norm);
    	float spec = pow(max(dot(viewDir, reflectDir) ,0.0), shineness);
    	vec3 specular = specularStrength * spec * lightColor;

    	vec3 phong = (ambient + diffuse + specular) * objectColor;
	aFragColor = vec4(phong, 1.0);

	gl_Position = proj * view * model * vec4(aPos.x, aPos.y, aPos.z, 1.0);
}