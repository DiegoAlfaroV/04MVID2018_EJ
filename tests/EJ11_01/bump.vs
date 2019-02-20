#version 330 core

layout (location=0) in vec3 aPos;
layout (location=1) in vec3 aNormal;
layout (location=2) in vec2 aTexCoord;
layout (location=3) in vec3 aTangent;
layout (location=4) in vec4 aBitangent;

uniform mat4 model;
uniform mat4 view;
uniform mat4 proj;
uniform mat3 normalMat;

out vec3 fragPos;
out vec2 texCoords;
out vec3 tangentLightPos;
out vec3 tangentViewPos;
out vec3 tangentFragPos;

struct Light {
    vec3 position;

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

uniform Light light;
uniform vec3 viewPos;

void main() {
	fragPos = vec3(model * vec4(aPos, 1.0));
	texCoords = aTexCoord;

	vec3 T = normalize(normalMat * aTangent);
	vec3 N = normalize(normalMat * aNormal);

	T = normalize(T - dot(T, N) * N);
	vec3 B = cross(N, T);

	mat3 TBN = transpose(mat3(T, B, N));

	tangentLightPos = TBN * light.position;
	tangentViewPos = TBN * viewPos;
	tangentFragPos = TBN * fragPos;

	gl_Position = proj * view * model * vec4(aPos.x, aPos.y, aPos.z, 1.0);
}