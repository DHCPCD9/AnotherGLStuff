using GLStuff.Graphics.Shaders;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using StbImageSharp;

namespace GLStuff.Graphics;

public class Box : IDrawable
{
    public override float[] Vertices => new []{
        // Позиция       // Текстурные координаты
        -0.5f, -0.5f, -0.5f,  0.0f, 0.0f, // Нижняя грань
        0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
        0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
        -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
        -0.5f, -0.5f,  0.5f,  0.0f, 0.0f, // Верхняя грань
        0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
        0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
        -0.5f,  0.5f,  0.5f,  0.0f, 1.0f,
        -0.5f, -0.5f, -0.5f,  0.0f, 0.0f, // Задняя грань
        0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
        0.5f, -0.5f,  0.5f,  1.0f, 1.0f,
        -0.5f, -0.5f,  0.5f,  0.0f, 1.0f,
        -0.5f,  0.5f, -0.5f,  0.0f, 0.0f, // Передняя грань
        0.5f,  0.5f, -0.5f,  1.0f, 0.0f,
        0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
        -0.5f,  0.5f,  0.5f,  0.0f, 1.0f,
    };

    public override uint[] Indices => new uint[]
    {
        0, 1, 2, // Нижняя грань
        2, 3, 0,
        4, 5, 6, // Верхняя грань
        6, 7, 4,
        8, 9, 10, // Задняя грань
        10, 11, 8,
        12, 13, 14, // Передняя грань
        14, 15, 12,
        3, 2, 6, // Боковые грани
        6, 7, 3,
        0, 1, 5,
        5, 4, 0,
        1, 2, 6,
        6, 5, 1,
        2, 3, 7,
        7, 6, 2,
        3, 0, 4,
        4, 7, 3,
        11, 10, 9, // Пустые грани
        9, 8, 11,
    };
    public Shader Shader { get; set; }
    public Game Game { get; }
    public Texture Texture { get; set; }
    public Vector3 Position { get; set; }
    public Vector3 Scale { get; set; }

    public Box(Game game)
    {
        Game = game;
    }
    
    public override void Load()
    {
        VertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(VertexArrayObject);
        VertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float), Vertices, BufferUsageHint.StaticDraw);

        ElementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, Indices.Length * sizeof(uint), Indices, BufferUsageHint.StaticDraw);
        Shader.Use();
        var vertexLocation = Shader.GetAttribLocation("aPosition");
        GL.EnableVertexAttribArray(vertexLocation);
        GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
        var texCoordLocation = Shader.GetAttribLocation("aTexCoord");
        GL.EnableVertexAttribArray(texCoordLocation);
        GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
        Texture = Texture.LoadFromFile(@"Assets/Textures/box.png");
        Texture.Use(TextureUnit.Texture0);
        

        Shader.SetInt("texture0", 0);
    }

    public override void Update()
    {
        
    }

    public override void Draw()
    {
        GL.BindVertexArray(VertexArrayObject);
        
        Texture.Use(TextureUnit.Texture0);
        Shader.Use();
        
      
        
        var model = Matrix4.Identity * Matrix4.CreateScale(Scale) * Matrix4.CreateTranslation(Position);
        Shader.SetMatrix4("model", model);
        Shader.SetMatrix4("view", Game.Camera.GetViewMatrix());
        Shader.SetMatrix4("projection", Game.Camera.GetProjectionMatrix());

        GL.DrawElements(PrimitiveType.Triangles, Indices.Length, DrawElementsType.UnsignedInt, 0);
        
        GL.BindVertexArray(0);
    }
}