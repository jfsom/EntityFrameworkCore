namespace EFCoreCodeFirstDemo.Entities
{
    // Derived Class representing a card payment (credit or debit)
    public class CardPayment : Payment
    {
        public string? CardNumber { get; set; } // The 16-digit (or appropriate length) number printed on the card used for the transaction.
        public string? CardHolderName { get; set; } // The full name of the person to whom the card is issued.
        public DateTime? ExpiryDate { get; set; } // The date after which the card is no longer valid.
        public string? CVV { get; set; } // The Card Verification Value, a security code typically found on the back of the card.
        public CardType? CardType { get; set; } // An enumeration indicating whether the card is a credit or debit card.
    }

    // Enumeration to specify the type of card
    public enum CardType
    {
        Credit,
        Debit
    }
}