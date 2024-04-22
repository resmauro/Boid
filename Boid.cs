using System.Data;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

public class Boid
{
    private readonly int id;
    private Vector3 position;
    private Vector3 velocity;
    private Triangle shape;
    public Vector3 Position
    {
        get { return this.position; }
        set { this.position = value; }
    }
    public Vector3 Velocity
    {
        get { return this.velocity; }
        set { this.velocity = value; }
    }

    public int Id
    {
        get { return this.id; }
    }

    public Boid(int id)
    {
        this.id = id;
        this.position = Vector3.Zero();
        this.velocity = Vector3.Up();
        this.shape = new Triangle(this.Position, this.Velocity);
    }

    public void Move(float timeDelta)
    {
        this.position = position + velocity.Scale(timeDelta);
        this.shape.Update(this.Position, this.Velocity);
    }

    public void Rotate(Vector3 direction)
    {
        if (direction != Vector3.Zero())
        {
            this.velocity = direction.Normalize().Scale(this.velocity.Length);
            this.shape.Update(this.Position, this.Velocity);
        }
    }

    public void Draw(Shader shader)
    {
        shader.Use();
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
    }

    public void Separation(Boid[] boidList, float perceptionRadius, float deltaTime)
    {
        Vector3 sumDirection = Vector3.Zero();
        foreach (Boid boid in boidList)
        {
            if (boid.Id != this.Id)
            {
                Vector3 direction = boid.Position - this.Position;
                if (direction.Length < perceptionRadius)
                {
                    sumDirection += direction;
                }
            }
        }

        Vector3 steerDirection = Vector3.Interpolate(this.Velocity, -sumDirection, deltaTime);
        this.Rotate(steerDirection);
    }

    public void Alignment(Boid[] boidList, float perceptionRadius, float deltaTime)
    {
        Vector3 sumDirection = Vector3.Zero();
        int nearbyBoids = 0;
        foreach (Boid boid in boidList)
        {
            if (boid.Id != this.Id)
            {
                if ((boid.Position - this.Position).Length < perceptionRadius)
                {
                    sumDirection += boid.Velocity.Normalize();
                    nearbyBoids++;
                }
            }
        }
        if (nearbyBoids == 0)
        {
            return;
        }
        Vector3 steerDirection = Vector3.Interpolate(this.Velocity, sumDirection.Scale(1 / nearbyBoids), deltaTime);
        this.Rotate(steerDirection);
    }

    public void Cohesion(Boid[] boidList, float perceptionRadius, float deltaTime)
    {
        Vector3 centerOfMass = Vector3.Zero();
        int nearbyBoids = 0;
        foreach (Boid boid in boidList)
        {
            if ((boid.Position - this.Position).Length < perceptionRadius)
            {
                centerOfMass += boid.Position;
                nearbyBoids++;
            }
        }
        if (nearbyBoids == 0)
        {
            return;
        }
        centerOfMass.Scale(1 / nearbyBoids);
        Vector3 direction = centerOfMass - this.Position;
        if (direction.IsZero())
        {
            return;
        }
        Vector3 steerDirection = Vector3.Interpolate(this.Velocity, direction.Normalize(), deltaTime);
        this.Rotate(steerDirection);
    }

    public void CollisionDetection(float width, float height, float depth, float deltaTime)
    {
        Vector3 direction;
        if ( this.Position.X > width / 2 )
        {
            direction = Vector3.Interpolate(this.Velocity, Vector3.Left(), deltaTime);
        }
        else if ( this.Position.X < - width / 2 )
        {
            direction = Vector3.Interpolate(this.Velocity, Vector3.Right(), deltaTime);
        }
        else if ( this.Position.Y > height / 2 )
        {
            direction = Vector3.Interpolate(this.Velocity, Vector3.Down(), deltaTime);
        }
        else if ( this.Position.Y < - height / 2 )
        {
            direction = Vector3.Interpolate(this.Velocity, Vector3.Up(), deltaTime);
        }
        else if ( this.Position.Z > depth / 2 )
        {
            direction = Vector3.Interpolate(this.Velocity, Vector3.Backward(), deltaTime);
        }
        else if ( this.Position.Z < - depth / 2 )
        {
            direction = Vector3.Interpolate(this.Velocity, Vector3.Forward(), deltaTime);
        }
        else
        {
            return;
        }

        this.Rotate(direction);
    }
}