#version 330 core

in vec3 myColor;
in vec3 myPosition;

out vec4 fragColor;

void main() {
	fragColor = vec4(myPosition, 1.0);
}