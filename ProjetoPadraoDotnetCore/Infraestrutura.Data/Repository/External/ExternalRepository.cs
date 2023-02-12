using Infraestrutura.Repository.External.Response;

namespace Infraestrutura.Repository.External;

public class ExternalRepository : IExternalRepository
{
    protected readonly HttpClient Http;

    public ExternalRepository()
    {
        Http = new HttpClient();
    }
    public async Task<BaseResponseExternal> SendWebHttp(string url)
    {
        try
        {
            var response = await Http.GetAsync(url);

            return new BaseResponseExternal()
            {
                Sucesso = true,
                ObjetoJson = await response.Content.ReadAsStringAsync(),
                StatusCode = response.StatusCode
            };
        }
        catch (HttpRequestException e)
        {
            return new BaseResponseExternal()
            {
                Sucesso = true,
                ObjetoJson = null,
                StatusCode = e.StatusCode
            };
        }
    }
}