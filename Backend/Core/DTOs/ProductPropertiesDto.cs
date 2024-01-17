namespace Core.DTOs;

public class ProductPropertiesDto
{
    public List<string?> Color { get; set; }
    public List<string> Brand { get; set; }
    public List<float?> FrameSize { get; set; }
    public List<float?> HandlebarSize { get; set; }
    public List<float?> WheelSize { get; set; }
}