using System.Collections.Generic;

namespace Models.DB
{
	public class CardFace
	{
		public int id{ get; set; }
		public Card card{ get; set; }
		public ColorCombinations colorIndicator{ get; set; }
		public Costs manaCost{ get; set; }
		public ColorCombinations color {get;set;}
		public string name{ get; set; }
		public TypeLine typeLine{ get; set; }
		public string oracleText{ get; set; }
		
		public string power{ get; set; }
		public string toughness{ get; set; }
		public string loyalty{ get; set; }	
	}
}