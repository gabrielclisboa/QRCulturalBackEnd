namespace QRCulturalBackEnd.Models
{
    public class CardDto
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public string titulo { get; set; }
        public string imagem { get; set; }
        public int monumentoId { get; set; }
        public Monumento monumento { get; set; }
    }
}
