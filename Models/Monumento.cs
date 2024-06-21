namespace QRCulturalBackEnd.Models
{
    public class Monumento
    {
        public int id { get; set; }
        public string contextoHistorico { get; set; }
        public string endereco { get; set; }
        public string horarioFuncionamento { get; set; }
        public string entrada { get; set; }
        public byte[] carrosel1 { get; set; }
        public byte[] carrosel2 { get; set; }
        public byte[] carrosel3 { get; set; }
        public byte[] imagemPrincipal { get; set; }
        public Card card { get; set; }
    }
}
