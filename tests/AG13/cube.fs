#version 330 core

out vec4 fragColor;

in vec3 normal;
in vec3 fragPos;
in vec2 texCoords;
vec4 fragPosLightSpace;

uniform sampler2D diffuseTexture;
uniform sampler2D depthMap;

uniform vec3 lightPos;
uniform vec3 viewPos;

float ShadowCalculation(vec4 fragPosLightSpace) {
	vec3 projCoords = fragPosLightSpace.xyz / fragPosLightSpace.w;
	projCoords = projCoords * 0.5 + 0.5;
	float closestDepth = texture(depthMap, projCoords.xy).r;
	float currentDepth = projCoords.z;
	float shadow = currentDepth > closestDepth ? 1.0 : 0.0;
	return shadow;
}

void main() {
	vec3 color = vec3(texture(diffuseTexture, texCoords));
	vec3 normal = normalize(normal);
	vec3 lightColor = vec3(0.3);

	vec3 ambient = 0.3 * color;    

	vec3 lightDir = normalize(lightPos - fragPos);
	float diff = max(dot(lightDir, normal) , 0.0);
	vec3 diffuse = diff  * lightColor;

    vec3 viewDir = normalize(viewPos - fragPos);
    vec3 halfwayDir = normalize(lightDir + viewDir);
    float spec = pow(max(dot(normal, halfwayDir) ,0.0), 0.01);
    vec3 specular = spec * lightColor;

	float shadow = ShadowCalculation(fragPosLightSpace);

    vec3 phong = (ambient + (1.0 - shadow) * (diffuse + specular)) * color;
	fragColor = vec4(phong, 1.0);
}