namespace Models.DB
{
    public class ShoppingCardItem
    {
        public int Id {get; set;}
        public int ShoppingCardId {get; set;}
        public string PrintId { get; set; }
        public int Quantity { get; set; }
    }
}