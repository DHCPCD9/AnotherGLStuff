namespace GLStuff.Graphics;

public abstract class IDrawable
{
    public abstract float[] Vertices { get; }
    public abstract uint[] Indices { get; }
    public int VertexBufferObject { get; set; }
    public int VertexArrayObject { get; set; }
    public int ElementBufferObject { get; set; }

    public abstract void Load();
    public abstract void Update();
    public abstract void Draw();
}