
using FileService.Client.Commons;
using FileService.Client.Dtos.Res;
using FileService.Client.Interfaces.Services;
using System.Net;

namespace FileService.Client.Services;

public class FileServiceClient : IFileServiceClient
{
    private readonly string fileServiceURL = "";
    private readonly IHttpClientFactory httpClientFactory;
    private readonly HttpClient httpClient;
    public FileServiceClient(IHttpClientFactory httpClientFactory, string fileServiceURL, string apiKey)
    {
        this.httpClientFactory = httpClientFactory;
        this.fileServiceURL = fileServiceURL;
        httpClient = httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(fileServiceURL);
        httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
    }
    public async Task<ApiResponse<FileResDto>> DownloadFileAsync(int fileId)
    {
        var url = $"{Constants.API_DOWNLOAD_FILE}/{fileId}";
        try
        {
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var fileBytes = await response.Content.ReadAsByteArrayAsync();

                var contentType = response.Content.Headers.ContentType?.ToString();
                var fileName = response.Content.Headers.ContentDisposition?.FileNameStar;

                var fileRes = new FileResDto(fileBytes, contentType ?? "", fileName ?? "");
                return new ApiResponse<FileResDto>(fileRes);
            }else
            {
                return new ApiResponse<FileResDto>((int)response.StatusCode, "Download failed.");
            }
        }
        catch (Exception ex)
        {

            return new ApiResponse<FileResDto>((int) HttpStatusCode.InternalServerError, $"File Service Internal Error: {ex.Message}, Inner Exception: {ex.InnerException}");

        }
    }
}
