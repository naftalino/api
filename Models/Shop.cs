namespace pd.Models
{
    // classe meme, nao é real.
    public class ShopItem
    {
        int Id;
        int Value;

        public void InsertItemOnShop(int id, int value)
        {
            Id = id;
            Value = value;
            Console.WriteLine("[!] Item inserido no shop.");
        }
    }
}