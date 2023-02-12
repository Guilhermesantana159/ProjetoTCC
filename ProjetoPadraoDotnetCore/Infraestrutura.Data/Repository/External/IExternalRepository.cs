using Infraestrutura.Repository.External.Response;

namespace Infraestrutura.Repository.External;

public interface IExternalRepository
{
    public Task<BaseResponseExternal> SendWebHttp(string url);
}