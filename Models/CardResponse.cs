using System.Collections.Generic;
using Models.DB;

namespace webshop_backend.Models
{
    public class CardResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string FlavorText { get; set; }
        public string OracleText { get; set; }
        public string Loyalty { get; set; }
        public string Power { get; set; }
        public string Toughness { get; set; }
        public int? Price { get; set; }
        public string TypeLine { get; set; }
        public List<CostSymbols> Mana { get; set; }
        public List<string> Color {get; set;}

        public static string GetTypeLine(List<Typeline> typeLine) {
            var tl = "";
                for (int i = 0; i < typeLine.Count; i++)
                {
                    if (i != typeLine.Count - 1)
                    {
                        tl += typeLine[i].TypeName + " ";
                    }
                    else
                    {
                        tl += typeLine[i].TypeName;
                    }
                }
            return tl;
        }
    }

}