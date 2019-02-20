#version 330 core

layout (location=0) in vec3 aPos;
layout (location=1) in vec3 aNormal;
layout (location=2) in vec2 aTexCoord;

uniform mat4 model;
uniform mat4 view;
uniform mat4 proj;
uniform mat4 lightSpaceMatrix;

out vec3 normal;
out vec3 fragPos;
out vec2 texCoords;
out vec4 fragPosLightSpace;

void main() {
	fragPos = vec3(model * vec4(aPos, 1.0));
	normal = transpose(inverse(mat3(model))) * aNormal;
	texCoords = aTexCoord;
	fragPosLightSpace = lightSpaceMatrix * vec4(fragPos, 1.0);
	gl_Position = proj * view * model * vec4(aPos.x, aPos.y, aPos.z, 1.0);
}