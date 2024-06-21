namespace QRCulturalBackEnd.Models
{
    public class Card
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public string titulo { get; set; }
        public byte[] imagem { get; set; }
        public int monumentoId { get; set; }
        public Monumento monumento { get; set; }
    }
}