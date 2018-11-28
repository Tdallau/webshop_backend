using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.DB
{
	public class Print
	{
		[MaxLength(36)]
		public string Id {get; set;}
		public Card Card {get; set;}
		public int? price {get; set;}
		public Set set {get; set;}
		public bool foil {get; set;}
		public bool nonfoil {get; set;}
		public bool oversized {get; set;}
		public string borderColor {get; set;}
		public string collectorsNumber {get; set;}
		public bool fullArt {get; set;}
		public Language language {get;set;}
		public bool isLatest {get;set;}
		public int? stock {get; set;}

	}
}