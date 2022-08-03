using Application.Common.Models;
using HtmlAgilityPack;

namespace Application.Parser
{
    public class NotesParser
    {
        private List<NoteModel> noteModels = new List<NoteModel>();
        public object Parse()
        {
            string path = "https://www.1tv.ru/shows/neputevye-zametki/vypuski";
            try
            {
                using (HttpClientHandler hdl = new HttpClientHandler
                {
                    AllowAutoRedirect = false,
                    AutomaticDecompression = System.Net.DecompressionMethods.Deflate |
                    System.Net.DecompressionMethods.GZip |
                    System.Net.DecompressionMethods.None
                })
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

                                    var cards = doc.DocumentNode
                                    .SelectNodes(".//section[@class='video-cards']//div[@class='collection_items']")[0]
                                    .ChildNodes;

                                    foreach (var item in cards)
                                    {
                                        NoteModel note = new NoteModel
                                        {
                                            ImagePath = item.ChildNodes[0].ChildNodes[1].ChildNodes[0]
                                            .GetAttributeValue("data-src", null),
                                            Title = item.ChildNodes[0].ChildNodes[2].ChildNodes[0].InnerText,
                                            Description = item.ChildNodes[0].ChildNodes[2].ChildNodes[1].InnerText
                                        };
                                        noteModels.Add(note);
                                    }
                                    return noteModels;
                                }
                                return null;
                            }
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
