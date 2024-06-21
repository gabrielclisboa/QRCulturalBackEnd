namespace QRCulturalBackEnd.Models
{
    public class CardInputModel
    {
        public int id {  get; set; }
        public string descricao { get; set; }
        public string titulo { get; set; }
        public int monumentoId { get; set; }
        public IFormFile imagem { get; set; }
    }
}
