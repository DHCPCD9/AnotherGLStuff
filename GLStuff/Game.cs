using GLStuff.Graphics;
using GLStuff.Graphics.Shaders;
using OpenTK.Graphics.ES11;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ErrorCode = OpenTK.Graphics.ES11.ErrorCode;

namespace GLStuff;

public class Game : GameWindow
{
    public Camera Camera { get; private set; }
    private Box TestBox { get; set; }
    public ShaderManager ShaderManager { get; set; } = new();

    public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(
        gameWindowSettings, nativeWindowSettings)
    {
    }

    protected override void OnLoad()
    {
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

        Camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);
        TestBox = new Box(this);
        TestBox.Shader =
            ShaderManager.GetShader("BaseShader", BaseShader.VertexShaderSource, BaseShader.FragmentShaderSource);
        TestBox.Load();
        TestBox.Scale = new Vector3(1, 1, 1);
        TestBox.Position = new Vector3(0, 0, 0);
        Camera = new Camera(Vector3.UnitY * 2, Size.X / (float)Size.Y);

        CursorState = CursorState.Grabbed;
    }


    protected override void OnRenderFrame(FrameEventArgs args)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit);

        TestBox.Draw();
        var error = GL.GetError();
        if (error != ErrorCode.NoError)
        {
            System.Console.WriteLine(error);
        }

        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        if (KeyboardState.IsKeyDown(Keys.Escape))
        {
            Close();
        }

        //Falling player down
        
        if (KeyboardState.IsKeyDown(Keys.W))
        {
            Camera.Position += Camera.Front * (float)args.Time;
        }
        if (KeyboardState.IsKeyDown(Keys.S))
        {
            Camera.Position -= Camera.Front * (float)args.Time;
        }
        
        if (KeyboardState.IsKeyDown(Keys.A))
        {
            Camera.Position -= Camera.Right * (float)args.Time;
        }
        
        if (KeyboardState.IsKeyDown(Keys.D))
        {
            Camera.Position += Camera.Right * (float)args.Time;
        }
        
        if (KeyboardState.IsKeyDown(Keys.Space))
        {
            Camera.Position += Camera.Up * (float)args.Time;
        }
        
        if (KeyboardState.IsKeyDown(Keys.LeftShift))
        {
            Camera.Position -= Camera.Up * (float)args.Time;
        }
    }

    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
        var sensitivity = 0.1f;
        Camera.Yaw += e.DeltaX * sensitivity;
        Camera.Pitch += -e.DeltaY * sensitivity;
    }
}