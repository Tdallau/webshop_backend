using System.Collections.Generic;

public class CartPriceResponse
{
    public List<Data> Data { get; set; }
    public bool Has_more { get; set; }
    public string next_page { get; set; }
}
