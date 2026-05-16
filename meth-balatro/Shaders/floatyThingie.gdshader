shader_type canvas_item;

uniform float shift_amount = 10.0;
uniform float float_speed = 2.0;

void fragment() {
    float offset_x = sin(TIME * float_speed) * shift_amount;
    float offset_y = cos(TIME * float_speed) * shift_amount;

    vec2 shifted_uv = UV + vec2(
        offset_x * SCREEN_PIXEL_SIZE.x,
        offset_y * SCREEN_PIXEL_SIZE.y
    );

    COLOR = texture(TEXTURE, shifted_uv);
}