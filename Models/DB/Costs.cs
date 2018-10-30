using System.Collections.Generic;

namespace Models.DB
{
    public class Costs {
		public int id {get;set;}
		public virtual List<SymbolsInCosts> Symbols {get;set;}

	}
}