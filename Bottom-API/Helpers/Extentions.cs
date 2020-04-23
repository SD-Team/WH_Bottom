using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Bottom_API.Helpers
{
    public static class Extentions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, PATCH, DELETE");
            response.Headers.Add("Access-Control-Allow-Headers", "content-type, accept, X-PINGOTHER");
            response.Headers.Add("Access-Control-Allow-Headers", "X-PINGOTHER, Host, User-Agent, Accept, Accept: application/json, application/json, Accept-Language, Accept-Encoding, Access-Control-Request-Method, Access-Control-Request-Headers, Origin, Connection, Content-Type, Content-Type: application/json, Authorization, Connection, Origin, Referer");
            response.Headers.Add("Access-Control-Allow-Credentials", "true");
        }

        public static void AddPagination(this HttpResponse response, 
            int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            response.Headers.Add("Pagination", 
                JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }

    }
}