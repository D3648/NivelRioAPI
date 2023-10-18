using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using NivelRio.Model;

namespace NivelRio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NivelRioController : ControllerBase
    {
        [HttpGet, Route("getRio")]
        public async Task<IActionResult> GetRio(string local)
        {
            Rio rio = new Rio();
            string url = rio.link;
            switch (local)
            {
                case "Gaspar":
                    url += "21";
                    break;
                case "Taio":
                    url += "42";
                    break;
                case "Ituporanga":
                    url += "43";
                    break;
                case "Blumenau":
                    url += "3";
                    break;
                case "Rio do sul":
                    url += "44";
                    break;
                default:
                    break;
            }

            using (var httpClient = new HttpClient())
            {
                var html = await httpClient.GetStringAsync(url);

                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);


                var divs = htmlDocument.DocumentNode.Descendants("div")
            .Where(node => node.GetAttributeValue("class", "").Contains("widget-holder")).ToList();

                foreach (var div in divs)
                {
                    var h5List = div.Descendants("h5").ToList();

                    foreach (var h5 in h5List)
                    {
                        var value = h5.InnerText.Trim().Replace("\n", "").Replace("  ", "");

                        if (h5.InnerText.Trim().Contains("Nivel do Rio"))
                        {
                            rio.nivel = value;
                        }
                        else if (h5.InnerText.Contains("Chuva atual"))
                        {
                            rio.chuvaAtual = value;
                        }
                        else if (h5.InnerText.Contains("Acumulada Última Hora"))
                        {
                            rio.acumuladaUH = value;
                        }
                        else if (h5.InnerText.Contains("Acumulada 6 Hrs"))
                        {
                            rio.acumulada6H = value;
                        }
                        else if (h5.InnerText.Contains("Acumulada 12 Hrs"))
                        {
                            rio.acumulada12H = value;
                        }
                        else if (h5.InnerText.Contains("Acumulada 24 Hrs"))
                        {
                            rio.acumulada24H = value;
                        }
                        else if (h5.InnerText.Contains("Acumulada 48 Hrs"))
                        {
                            rio.acumulada48H = value;
                        }
                        else if (h5.InnerText.Contains("Acumulada 72 Hrs"))
                        {
                            rio.acumulada72H = value;
                        }
                        else if (h5.InnerText.Contains("FONTE:"))
                        {
                            rio.fonte = value;
                        }
                    }

                }

                //================= SITUAÇÃO ========================
                var sitDiv = htmlDocument.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("success-box alert")).ToList();

                foreach (var div in sitDiv)
                {
                    var situacao = div.Descendants("span").FirstOrDefault();

                    rio.situacao = situacao.InnerText.Trim().Replace("&nbsp", "").Replace("  ", "").Replace("\n", "").Replace(";", "");
                }
                //================= ========== ========================
                rio.local = local;
                return new JsonResult(rio);
            }

        }

        [HttpGet, Route("getRios")]
        public async Task<IActionResult> GetRios()
        {
            List<Rio> rios = new List<Rio>();
            List<string> urlList = new List<string>
            {
                "21",
                "3",
                "42",
                "43",
                "44"
            };

            foreach (var rioId in urlList)
            {
                Rio rio = new Rio();
                string url = $"{rio.link}{rioId}";



                using (var httpClient = new HttpClient())
                {
                    var html = await httpClient.GetStringAsync(url);

                    var htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(html);


                    var divs = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("widget-holder")).ToList();

                    foreach (var div in divs)
                    {
                        var h5List = div.Descendants("h5").ToList();

                        foreach (var h5 in h5List)
                        {
                            var value = h5.InnerText.Trim().Replace("\n", "").Replace("  ", "");

                            if (h5.InnerText.Trim().Contains("Nivel do Rio"))
                            {
                                rio.nivel = value;
                            }
                            else if (h5.InnerText.Contains("Chuva atual"))
                            {
                                rio.chuvaAtual = value;
                            }
                            else if (h5.InnerText.Contains("Acumulada Última Hora"))
                            {
                                rio.acumuladaUH = value;
                            }
                            else if (h5.InnerText.Contains("Acumulada 6 Hrs"))
                            {
                                rio.acumulada6H = value;
                            }
                            else if (h5.InnerText.Contains("Acumulada 12 Hrs"))
                            {
                                rio.acumulada12H = value;
                            }
                            else if (h5.InnerText.Contains("Acumulada 24 Hrs"))
                            {
                                rio.acumulada24H = value;
                            }
                            else if (h5.InnerText.Contains("Acumulada 48 Hrs"))
                            {
                                rio.acumulada48H = value;
                            }
                            else if (h5.InnerText.Contains("Acumulada 72 Hrs"))
                            {
                                rio.acumulada72H = value;
                            }
                            else if (h5.InnerText.Contains("FONTE:"))
                            {
                                rio.fonte = value;
                            }
                        }

                    }

                    //================= SITUAÇÃO ========================
                    var sitDiv = htmlDocument.DocumentNode.Descendants("div")
                        .Where(node => node.GetAttributeValue("class", "")
                        .Contains("success-box alert")).ToList();

                    foreach (var div in sitDiv)
                    {
                        var situacao = div.Descendants("span").FirstOrDefault();

                        rio.situacao = situacao.InnerText.Trim().Replace("&nbsp", "").Replace("  ", "").Replace("\n", "").Replace(";", "");
                    }
                    //================= ========== ========================
                    var local = "";
                    switch (rioId)
                    {
                        case "21":
                            local = "Gaspar";
                            break;
                        case "3":
                            local = "Blumenau";
                            break;
                        case "42":
                            local = "Taió";
                            break;
                        case "43":
                            local = "Ituporanga";
                            break;
                        case "44":
                            local = "Rio do Sul";
                            break;
                        default:
                            break;
                    }
                    rio.local = local;
                    rios.Add(rio);
                }
            }

            return new JsonResult(rios);
        }
    }
}
