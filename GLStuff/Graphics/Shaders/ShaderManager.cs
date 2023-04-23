namespace GLStuff.Graphics.Shaders;

public class ShaderManager
{
    
    public Dictionary<string, Shader> Shaders { get; set; }
    
    public ShaderManager()
    {
        Shaders = new Dictionary<string, Shader>();
    }
    
    public void AddShader(Shader shader)
    {
        Shaders.Add(shader.GetType().Name, shader);
    }

    public Shader GetShader(string name, string vertexShaderSource, string fragmentShaderSource)
    {
        if (!Shaders.ContainsKey(name))
        {
            Shader shader = new Shader(vertexShaderSource, fragmentShaderSource);
            Shaders.Add(name, shader);
            return shader;
        }
        
        return Shaders[name];
    }
}
    