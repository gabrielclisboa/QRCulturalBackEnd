namespace QRCulturalBackEnd.Models
{
    public class MonumentoInputModel
    {
        public int id {  get; set; }
        public string history { get; set; }
        public string address { get; set; }
        public string openingHours { get; set; }
        public string entryFee { get; set; }
        public IFormFile carousel1 { get; set; }
        public IFormFile carousel2 { get; set; }
        public IFormFile carousel3 { get; set; }
        public IFormFile image { get; set; }
    }

}
