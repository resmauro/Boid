#version 420 core
#define e 2.718281828459045

out vec4 FragColor;


void main()
{
   //FragColor = vec4(log(vec3(1.0,1.0,1.0) + outColor * (e-1)), 1.0);
   //FragColor = vec4(outColor, 1.0);
   FragColor = vec4(0.3, 0.5, 0.1, 1.0);
}