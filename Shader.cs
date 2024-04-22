using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
public class Shader: IDisposable
{
    public int Handle;

    private Matrix4 projection;
    private Matrix4 view;

    public Shader(string vertexPath, string fragmentPath, Matrix4 proj, Matrix4 view)
    {
        int VertexShader, FragmentShader;

        this.projection = proj;
        this. view = view;

        string VertexShaderSource = File.ReadAllText(vertexPath);
        VertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(VertexShader, VertexShaderSource);

        
        string FragmentShaderSource = File.ReadAllText(fragmentPath);
        FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(FragmentShader, FragmentShaderSource);

        GL.CompileShader(VertexShader);
        GL.GetShader(VertexShader, ShaderParameter.CompileStatus, out int successVertex);

        if (successVertex == 0)
        {
            string infoLog = GL.GetShaderInfoLog(VertexShader);
            Console.WriteLine(infoLog);
        }

        GL.CompileShader(FragmentShader);
        GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out int successFragment);

        if (successFragment == 0)
        {
            string infoLog = GL.GetShaderInfoLog(FragmentShader);
            Console.WriteLine(infoLog);
        }

        Handle = GL.CreateProgram();

        GL.AttachShader(Handle, VertexShader);
        GL.AttachShader(Handle, FragmentShader);

        GL.LinkProgram(Handle);

        GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int successProgram);

        if (successProgram == 0)
        {
            string infoLog = GL.GetProgramInfoLog(Handle);
            Console.WriteLine(infoLog);
        }

        GL.DetachShader(Handle, VertexShader);
        GL.DetachShader(Handle, FragmentShader);
        GL.DeleteShader(VertexShader);
        GL.DeleteShader(FragmentShader);
    }

    public void UpdateMatrices(Matrix4 proj, Matrix4 view)
    {
        this.projection = proj;
        this. view = view;
    }

    public void Use()
    {
        GL.UseProgram(Handle);
        SetUniformMatrix("projection", projection);
        SetUniformMatrix("view", view);
    }

    public void SetUniformMatrix(string name, Matrix4 mat)
    {
        int handle = GL.GetUniformLocation(this.Handle, name);
        GL.UniformMatrix4(handle, false, ref mat);
    }

    private bool disposedValue = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            GL.DeleteProgram(Handle);

            disposedValue = true;
        }
    }

    ~Shader()
    {
        if (disposedValue == false)
        {
            Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
        }
    }


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}