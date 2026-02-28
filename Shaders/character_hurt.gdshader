shader_type spatial;

render_mode unshaded,cull_disabled;

uniform bool active = false;
uniform vec4 flash_color: source_color = vec4(1);
uniform sampler2D tex: source_color;

void fragment() {
	// Called for every pixel the material is visible on.
	vec4 color = texture(tex,UV);
	ALPHA = color.a;

	if(active == true){
		color = flash_color;
	}

	ALBEDO = vec3(color.r,color.g,color.b) * flash_color.rgb;
}
