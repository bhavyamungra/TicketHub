using System.ComponentModel.DataAnnotations;

namespace TicketHub
{
    // Class representing the data needed for a ticket purchase.
    public class TicketData
    {
        // The unique identifier for the concert/event. It must be a positive integer.
        [Required(ErrorMessage = "Concert identifier is necessary.")]
        [Range(1, int.MaxValue, ErrorMessage = "Concert ID must be a positive value.")]
        public int EventId { get; set; }

        // The email address of the person making the purchase.
        // It must be a valid email format.
        [Required(ErrorMessage = "Please enter a valid email address.")]
        [EmailAddress(ErrorMessage = "The email provided is not valid.")]
        public string Email { get; set; } = string.Empty;

        // The full name of the person purchasing the tickets.
        // It must be between 2 and 100 characters, and only contain letters and spaces.
        [Required(ErrorMessage = "Name of the buyer is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Name should only include alphabetic characters.")]
        public string FullName { get; set; } = string.Empty;

        // The phone number of the buyer. 
        // It must contain exactly 10 digits.
        [Required(ErrorMessage = "Please provide a phone number.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number should contain exactly 10 digits.")]
        public string Phone { get; set; } = string.Empty;

        // The quantity of tickets the buyer wants to purchase.
        // It must be between 1 and 10 tickets.
        [Required(ErrorMessage = "The number of tickets is required.")]
        [Range(1, 10, ErrorMessage = "You can only purchase between 1 and 10 tickets.")]
        public int Quantity { get; set; }

        // The credit card number of the buyer.
        // It must be exactly 12 digits.
        [Required(ErrorMessage = "Credit card number is mandatory.")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Credit card number must be exactly 12 digits.")]
        public string CardNumber { get; set; } = string.Empty;

        // The expiration date of the credit card.
        // It must be in the MM/YY format.
        [Required(ErrorMessage = "Expiration date is mandatory.")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/\d{2}$", ErrorMessage = "Expiration date should be in MM/YY format.")]
        public string Expiry { get; set; } = string.Empty;

        // The security code (CVV) on the credit card.
        // It must consist of exactly 3 numeric digits.
        [Required(ErrorMessage = "Please provide the card's security code.")]
        [StringLength(3, ErrorMessage = "Security code must be exactly 3 digits.")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "Security code must consist of 3 numeric digits.")]
        public string SecurityCode { get; set; } = string.Empty;

        // The shipping address for the ticket order.
        // The address cannot exceed 200 characters.
        [Required(ErrorMessage = "Shipping address is necessary.")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string Address { get; set; } = string.Empty;

        // The city where the tickets will be shipped to.
        // The city name cannot exceed 100 characters.
        [Required(ErrorMessage = "City name is required.")]
        [StringLength(100, ErrorMessage = "City name must not exceed 100 characters.")]
        public string City { get; set; } = string.Empty;

        // The state or province of the shipping address.
        // It must not exceed 50 characters.
        [Required(ErrorMessage = "Please provide a valid state or province.")]
        [StringLength(50, ErrorMessage = "State/Province name cannot exceed 50 characters.")]
        public string Province { get; set; } = string.Empty;

        // The postal code of the shipping address.
        // It must be exactly 6 characters long.
        [Required(ErrorMessage = "Postal code is mandatory.")]
        [StringLength(6, ErrorMessage = "Postal code must be exactly 6 characters long.")]
        public string Postal { get; set; } = string.Empty;

        // The country where the tickets will be shipped to.
        // The country name cannot exceed 100 characters.
        [Required(ErrorMessage = "Please mention the country.")]
        [StringLength(100, ErrorMessage = "Country name cannot exceed 100 characters.")]
        public string Country { get; set; } = string.Empty;
    }
}
