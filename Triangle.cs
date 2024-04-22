using System.Data;
using System.Numerics;

public class Triangle
{
    public float[] vertices = new float[9];
    public Mesh mesh;

    public Triangle(Vector3 center, Vector3 direction)
    {
        direction = direction.Normalize().Scale(0.5f);
        this.vertices[0] = center.X + direction.X;
        this.vertices[1] = center.Y + direction.Y;
        this.vertices[2] = center.Z + direction.Z;
        
        this.vertices[3] = center.X - 0.25f;
        this.vertices[4] = center.Y - 0.25f;
        this.vertices[5] = center.Z;
        
        this.vertices[6] = center.X + 0.25f;
        this.vertices[7] = center.Y - 0.25f;
        this.vertices[8] = center.Z;
        this.mesh = new Mesh(this.vertices);
    }

    public void Update(Vector3 center, Vector3 direction)
    {
        direction = direction.Normalize().Scale(0.5f);
        this.vertices[0] = center.X + direction.X;
        this.vertices[1] = center.Y + direction.Y;
        this.vertices[2] = center.Z + direction.Z;
        
        this.vertices[3] = center.X - 0.25f;
        this.vertices[4] = center.Y - 0.25f;
        this.vertices[5] = center.Z;
        
        this.vertices[6] = center.X + 0.25f;
        this.vertices[7] = center.Y - 0.25f;
        this.vertices[8] = center.Z;
        this.mesh.Update(this.vertices);
    }
}