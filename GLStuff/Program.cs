using GLStuff;
using OpenTK.Windowing.Desktop;

using var game = new Game(new GameWindowSettings
{
    RenderFrequency = 500,
    UpdateFrequency = 500
}, new NativeWindowSettings
{
    Size = new OpenTK.Mathematics.Vector2i(800, 600),
    Title = "GLStuff"
});

game.Run();