using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using OpenTK.Mathematics;

class Camera
{
    private Vector3 position;

    public Vector3 Position
    {
        get { return this.position; }
        set { this.position = value; }
    }

    private Vector3 direction;
    public Vector3 Direction
    {
        get { return this.direction; }
        set { this.direction = value; }
    }
    
    private Vector3 up;

    public Vector3 Up
    {
        get {return this.up;}
    }

    public Matrix4 view;

    public Matrix4 projection;

    public Camera(Vector3 pos, Vector3 direction, int width, int height, Vector3? vup = null)
    {
        this.projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), (float)width/(float)height, 0.1f, 100.0f);
        this.position = pos;
        this.direction = direction;
        if (vup == null)
        {
            this.up = Vector3.Up();
        }
        else
        {
            this.up = ((Vector3) vup).Normalize();
        }

        this.view = Matrix4.LookAt(this.position.ToOpenTK(), (this.direction + this.position).ToOpenTK(), this.up.ToOpenTK());
    }

    public void Move(Vector3 change, Shader shader)
    {
        this.position += change;
        Vector3 target = this.position + Vector3.Backward();
        Console.WriteLine("Posizione: ");
        this.position.Print();
        Console.WriteLine("Direzione: ");
        this.direction.Print();
        Console.WriteLine("Guarda verso: ");
        target.Print();

        this.view = Matrix4.LookAt(position.X, position.Y, position.Z, target.X, target.Y, target.Z, 0.0f, 1.0f, 0.0f);
        shader.UpdateMatrices(this.projection, this.view);
    }

    public void UpdateSize(int width, int height, Shader shader)
    {
        this.projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), (float)width / (float)height, 0.1f, 100.0f);
        shader.UpdateMatrices(this.projection, this.view);
    }
}