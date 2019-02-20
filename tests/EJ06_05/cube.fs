#version 330 core

out vec4 fragColor;
in vec4 aFragColor;

void main() {
	fragColor = aFragColor;
}