using OpenTK.Graphics.OpenGL4;

public class Mesh
{
    public int VertexBufferObject;
    public int VertexArrayObject;
    public Mesh(float[] vertices)
    {
        VertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.DynamicDraw);

        VertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(VertexArrayObject);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3*sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
    }

    public void Update(float[] vertices)
    {
        //this.VertexBufferObject = GL.GenBuffer();
        //GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.DynamicDraw);

        //this.VertexArrayObject = GL.GenVertexArray();
        //GL.BindVertexArray(VertexArrayObject);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3*sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
    }
}
