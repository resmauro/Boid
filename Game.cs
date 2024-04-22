using System.Diagnostics;
using System.Numerics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

public class Game : GameWindow
{
    Shader shader;

    static int boidNumber = 100;

    readonly Boid[] boidList = new Boid[boidNumber];
    readonly Camera camera;

    readonly float CameraSpeed = 0.02f;

    public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { ClientSize = (width, height), Title = title })
    {
        camera = new Camera(new Vector3(0.0f, 0.0f, 30.0f), Vector3.Backward(), width, height);
        shader = new Shader("C:\\Users\\frm\\OneDrive\\Desktop\\Otium\\Informatica\\Progetti\\Boid\\shader.vert", "C:\\Users\\frm\\OneDrive\\Desktop\\Otium\\Informatica\\Progetti\\Boid\\shader.frag", camera.projection, camera.view);
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        GL.ClearColor(0.2f, 0.3f, 0.4f, 1.0f);
        GL.Enable(EnableCap.DepthTest);
        for (int i = 0; i < boidNumber; i++)
        {
            boidList[i] = new Boid(i);
            boidList[i].Position = Vector3.RandomPosition(1.0f);
            boidList[i].Rotate(Vector3.RandomVersor());
        }
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        float deltaTime = (float) args.Time;

        GL.Clear(ClearBufferMask.ColorBufferBit);
        GL.Clear(ClearBufferMask.DepthBufferBit);

        foreach (Boid boid in boidList)
        {
            boid.Separation(boidList, 3.0f, 0.002f);
            boid.Alignment(boidList, 10.0f, 0.002f);
            boid.Cohesion(boidList, 10.0f, 0.002f);
            boid.CollisionDetection(20.0f, 20.0f, 20.0f, 0.0045f);
            boid.Move(deltaTime);
            boid.Draw(shader);
        }

        SwapBuffers();
    }
    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);

        if(!IsFocused)
        {
            return;
        }

        if (KeyboardState.IsKeyDown(Keys.Escape))
        {
            Close();
        }

        if (KeyboardState.IsKeyDown(Keys.W))
        {
            camera.Move(Vector3.Up().Scale(CameraSpeed), this.shader);
        }
        if (KeyboardState.IsKeyDown(Keys.A))
        {
            camera.Move(Vector3.Left().Scale(CameraSpeed), this.shader);
        }
        if (KeyboardState.IsKeyDown(Keys.S))
        {
            camera.Move(Vector3.Down().Scale(CameraSpeed), this.shader);
        }
        if (KeyboardState.IsKeyDown(Keys.D))
        {
            camera.Move(Vector3.Right().Scale(CameraSpeed), this.shader);
        }
        if (KeyboardState.IsKeyDown(Keys.Q))
        {
            camera.Move(Vector3.Forward().Scale(CameraSpeed), this.shader);
        }
        if (KeyboardState.IsKeyDown(Keys.E))
        {
            camera.Move(Vector3.Backward().Scale(CameraSpeed), this.shader);
        }


    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
    {
        base.OnFramebufferResize(e);
        camera.UpdateSize(e.Width, e.Height, this.shader);

        GL.Viewport(0, 0, e.Width, e.Height);
    }

    protected override void OnUnload()
    {
        base.OnUnload();
    }
}
