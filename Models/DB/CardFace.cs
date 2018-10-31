using System.Collections.Generic;

namespace Models.DB
{
	public class CardFace
	{
		public int id{ get; set; }
		public Card card{ get; set; }
		public Colors colorIndicator{ get; set; }
		public string manaCost{ get; set; }
		public string name{ get; set; }
		public List<Type> typeLine{ get; set; }
		public string oracleText{ get; set; }
		
		public string power{ get; set; }
		public string toughness{ get; set; }
		public string loyalty{ get; set; }	
	}
}