
using System.ComponentModel.DataAnnotations;

namespace Models.DB
{
    public class Legalities 
	{
		public int id {get;set;}
		[MaxLength(10)]
		public string name {get;set;}
		public string backgroundColor {get;set;}
	}
}