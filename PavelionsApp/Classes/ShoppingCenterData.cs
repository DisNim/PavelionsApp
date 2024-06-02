
namespace PavelionsApp.Classes
{
    static class ShoppingCenterData
    {

        public static string? ShoppingCenterId { get; set; }
        public static string? ShoppingCenterName { get; set; }
        public static string? ShoppingCenterStatus { get; set; }
        public static string? ShoppingCenterCountPav { get; set; }
        public static string? ShoppingCenterCity { get; set; }
        public static string? ShoppingCenterCast { get; set; } 
        public static string? ShoppingCenterFloors { get; set; } 
        public static string? ShoppingCenterAddedCost { get; set; }
        
        public static void clearVal()
        {
            ShoppingCenterId = "";
            ShoppingCenterName = "";
            ShoppingCenterStatus = "";
            ShoppingCenterFloors = "";
            ShoppingCenterCountPav = "";
            ShoppingCenterCast = "";
            ShoppingCenterAddedCost = "";
            ShoppingCenterCity = "";
        }
    }
}
