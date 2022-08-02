
using HtmlAgilityPack;

namespace Application.Parser
{
    public class NotesParser
    {
        public object Parse()
        {
            string path = "https://www.1tv.ru/shows/neputevye-zametki/vypuski";
            try
            {
                using (HttpClientHandler hdl = new HttpClientHandler { AllowAutoRedirect = false,
                    AutomaticDecompression=System.Net.DecompressionMethods.Deflate|
                    System.Net.DecompressionMethods.GZip |
                    System.Net.DecompressionMethods.None })
                {
                    using (var clnt = new HttpClient(hdl))
                    {
                        using (var response = clnt.GetAsync(path).Result)
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var html = response.Content.ReadAsStringAsync().Result;
                                if (!string.IsNullOrEmpty(html))
                                {
                                    HtmlDocument doc = new HtmlDocument();
                                    doc.LoadHtml(html);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
