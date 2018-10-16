using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.DB
{
	public class Set {
		[MaxLength(4)]
		public string Id {get; set;}
		public string name {get; set;}
		public string setType {get; set;}
		public int releasedAt {get; set;}
		public string blockCode {get; set;}
		public string paretnSetCode {get; set;}
		public int cardCount {get; set;}
		public bool foilOnly {get; set;}
		public string iconSVG {get; set;}
		public virtual List<CardInSet> Cards { get; set; }

	}

}