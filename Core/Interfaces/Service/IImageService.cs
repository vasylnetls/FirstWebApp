using Core.Interfaces;

namespace Core.Service;

public interface IImageService
{
    Task<byte[]> GetImage(int? id, string? name);
    Task<byte[]> GetImageFromSite(int? id, string? name);
    bool IsImageInBase(string name, out IImage? dbImage);
}