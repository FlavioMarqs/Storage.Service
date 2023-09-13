namespace Storage.Client
{
    public class StorageClientOptions
    {
        private const string _stringsControllerPath = "StorageStrings";

        public string ApiServiceUrl { get; set; }

        public string ApiServiceUrlWithSlash => $"{ApiServiceUrl.TrimEnd('/')}/";

        public string StorageStringsUrl => $"{ApiServiceUrlWithSlash}{_stringsControllerPath}";
    }
}
