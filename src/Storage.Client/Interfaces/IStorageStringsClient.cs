using Storage.DTOs.Requests;
using Storage.DTOs.Responses;

namespace Storage.Client.Interfaces
{
    public interface IStorageStringsClient
    {
        Task<HttpResponseMessage> PutStringAsync(StringCreationRequest request);

        Task<HttpResponseMessage> GetStringByIdAsync(StringQueryRequest request);

        Task<HttpResponseMessage> GetAllStringsAsync(StringsQueryRequest request);

        Task<HttpResponseMessage> DeleteStringAsync(StringRemovalRequest request);
    }
}
