namespace Core.Interfaces.Repository;

public interface IImageRepository
{
    bool CreateImage(IImage image);
    bool UpdateImage(IImage image);
    IImage? GetImageByName(string name);
    List<IImage> GetImages();
}