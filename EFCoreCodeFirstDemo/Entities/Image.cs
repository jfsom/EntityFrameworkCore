namespace EFCoreCodeFirstDemo.Entities
{
    // Derived Class representing an Image
    public class Image : Content
    {
        public string ImageUrl { get; set; } // URL of the image
        public string? Caption { get; set; } // Caption for the image
        public string AltText { get; set; } // Alternative text for accessibility
        public string Dimensions { get; set; } // Dimensions of the image (e.g., 1920x1080)
        public string? Photographer { get; set; } // Name of the photographer
    }
}