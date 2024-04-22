using System;
using System.Diagnostics.CodeAnalysis;
using OpenTK.Graphics.ES20;

public struct Vector3
{
    private float x;
    private float y;
    private float z;

    static Random rand = new();

    public Vector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public float X
    {
        get { return this.x; }

        set { this.x = value; }
    }
    public float Y
    {
        get { return this.y; }

        set { this.y = value; }
    }
    public float Z
    {
        get { return this.z; }

        set { this.z = value; }
    }

    public float Length
    {
        get { return (float) Math.Sqrt(x*x + y*y + z*z); }
    }

    public static bool operator ==(Vector3 v, Vector3 w)
    {
        if (v.X == w.X && v.Y == w.Y && v.Z == w.Z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool operator !=(Vector3 v, Vector3 w)
    {
        if (v.X != w.X || v.Y != w.Y || v.Z != w.Z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public static Vector3 operator +(Vector3 v, Vector3 w)
    {
        return new Vector3(v.X + w.X, v.Y + w.Y, v.Z + w.Z);
    }

    public static Vector3 operator -(Vector3 v, Vector3 w)
    {
        return new Vector3(v.X - w.X, v.Y - w.Y, v.Z - w.Z);
    }

    public static float operator *(Vector3 v, Vector3 w)
    {
        return v.X * w.X + v.Y * w.Y + v.Z * w.Z;
    }

    public static Vector3 operator -(Vector3 v)
    {
        return new Vector3(-v.X, -v.Y, -v.Z);
    }

    public bool IsZero()
    {
        if (this.X == 0 && this.Y == 0 && this.Z == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static Vector3 Cross(Vector3 v, Vector3 w)
    {
        return new Vector3(v.Y*w.Z - v.Z*w.Y, v.Z*w.X - v.X*w.Z, v.X*w.Y - v.Y*w.X);
    }

    public Vector3 Scale(float k)
    {
        return new Vector3(this.X * k, this.Y * k, this.Z * k);
    }

    public Vector3 Normalize()
    {
        return this.Scale(1 / this.Length);
    }

    public static float Angle(Vector3 v, Vector3 w)
    {
        return v*w / (v.Length * w.Length);
    }

    public void Print()
    {
        Console.WriteLine(string.Format("X = {0}\nY = {1}\nZ = {2}", this.X, this.Y, this.Z));
    }

    public OpenTK.Mathematics.Vector3 ToOpenTK()
    {
        return new OpenTK.Mathematics.Vector3(this.X, this.Y, this.Z);
    }

    public static Vector3 RandomVersor()
    {
        return (new Vector3(rand.NextSingle() - 0.5f, rand.NextSingle() - 0.5f, 0.0f)).Normalize();
    }

    public static Vector3 RandomPosition(float range)
    {
        return (new Vector3((rand.NextSingle() - 0.5f) * 2 * range, (rand.NextSingle() - 0.5f) * 2 * range, 0.0f));
    }

    public static Vector3 Zero()
    {
        return new Vector3(0.0f, 0.0f, 0.0f);
    }
    
    public static Vector3 Up()
    {
        return new Vector3(0.0f, 1.0f, 0.0f);
    }
    public static Vector3 Down()
    {
        return new Vector3(0.0f, -1.0f, 0.0f);
    }
    public static Vector3 Right()
    {
        return new Vector3(1.0f, 0.0f, 0.0f);
    }
    public static Vector3 Left()
    {
        return new Vector3(-1.0f, 0.0f, 0.0f);
    }
    public static Vector3 Forward()
    {
        return new Vector3(0.0f, 0.0f, 1.0f);
    }
    public static Vector3 Backward()
    {
        return new Vector3(0.0f, 0.0f, -1.0f);
    }

    public static Vector3 Interpolate(Vector3 v, Vector3 w, float t)
    {
        return w.Scale(t) + v.Scale(1 - t);
    }
}