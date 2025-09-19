namespace MutluSepet.Models.ViewModels
{
    public class CheckoutViewModel
    {
        // Adres bilgileri
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

        // Kart bilgileri
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CVV { get; set; }
    }
}
