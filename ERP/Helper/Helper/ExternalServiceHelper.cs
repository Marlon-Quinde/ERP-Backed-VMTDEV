using ERP.Helper.Models;
using System.Text;

namespace ERP.Helper.Helper
{
    public class ExternalServiceHelper
    {

        public async Task<ResponseGeneralModel<string?>> PostServiceExternal(string url, Dictionary<string, string>? headers = null, string jsonData = "{}")
        {
            HttpClient httpC = new HttpClient();

            try
            {
                //httpC.DefaultRequestHeaders.Add("Content-Type", "application/json");
                if (headers != null) {
                    foreach (var item in headers)
                    {
                        httpC.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }

                var contentBody = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpC.PostAsync(url, contentBody);

                var temp = await response.Content.ReadAsStringAsync();

                var item2222 = response.IsSuccessStatusCode;

                return new ResponseGeneralModel<string>(200, temp, "");
            } catch (Exception ex)
            {
                return new ResponseGeneralModel<string>(500, "Error al generar el pdf");
            }
        }
    }
}
