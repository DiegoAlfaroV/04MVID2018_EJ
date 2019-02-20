#version 330 core

out vec4 fragColor;
in vec2 texCoords;

uniform sampler2D screenTexture;

void make_kernel(inout vec4 n[9], sampler2D tex, vec2 coord)

{
	float offset = 1.0 / 300.0;

	n[0] = texture2D(tex, coord + vec2( -offset, -offset));
	n[1] = texture2D(tex, coord + vec2(0.0, -offset));
	n[2] = texture2D(tex, coord + vec2(  offset, -offset));
	n[3] = texture2D(tex, coord + vec2( -offset, 0.0));
	n[4] = texture2D(tex, coord);
	n[5] = texture2D(tex, coord + vec2(  offset, 0.0));
	n[6] = texture2D(tex, coord + vec2( -offset, offset));
	n[7] = texture2D(tex, coord + vec2(0.0, offset));
	n[8] = texture2D(tex, coord + vec2(  offset, offset));
}

void main() {
	vec4 n[9];

	make_kernel(n, screenTexture, texCoords);

	vec4 sobel_edge_h = n[2] + (2.0*n[5]) + n[8] - (n[0] + (2.0*n[3]) + n[6]);
  	vec4 sobel_edge_v = n[0] + (2.0*n[1]) + n[2] - (n[6] + (2.0*n[7]) + n[8]);
	vec4 sobel = sqrt((sobel_edge_h * sobel_edge_h) + (sobel_edge_v * sobel_edge_v));

	fragColor = vec4(1.0 - sobel.rgb, 1.0);
}