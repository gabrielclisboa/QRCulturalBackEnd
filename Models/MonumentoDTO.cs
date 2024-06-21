namespace QRCulturalBackEnd.Models
{
    public class MonumentoDTO
    {
        public int id { get; set; }
        public string contextoHistorico { get; set; }
        public string endereco { get; set; }
        public string horarioFuncionamento { get; set; }
        public string entrada { get; set; }
        public string carrosel1 { get; set; }
        public string carrosel2 { get; set; }
        public string carrosel3 { get; set; }
        public string imagemPrincipal { get; set; }
        public Card card { get; set; }
    }
}
