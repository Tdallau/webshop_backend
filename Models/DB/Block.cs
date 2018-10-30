using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.DB
{
	public class Block
	{
		[MaxLength(10)]
		public string id {get; set;}
		public string name {get; set;}
	}
}