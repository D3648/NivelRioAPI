namespace NivelRio.Model
{
    public class Rio
    {
        public string link = "https://defesacivil.gaspar.sc.gov.br/estacao/ver/";
        public string local { get; set; }
        public string situacao { get; set; }
        public string nivel { get; set; }
        public string chuvaAtual { get; set; }
        public string acumuladaUH { get; set; }
        public string acumulada6H { get; set; }
        public string acumulada12H { get; set; }
        public string acumulada24H { get; set; }
        public string acumulada48H { get; set; }
        public string acumulada72H { get; set; }
        public string fonte { get; set; }
    }
}
