#version 420 core

layout (location = 0) in vec3 aPosition;

uniform mat4 projection;
uniform mat4 view;

void main()
{
    gl_Position = vec4(aPosition, 1.0)*projection*view;
}