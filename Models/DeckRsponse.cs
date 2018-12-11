using System.Collections.Generic;
using Models.DB;
using webshop_backend.Models;
using webshop_backend.Models.DB;

namespace Models
{
  public class DeckResponse
  {
    public int Id {get; set;}
    public string Name {get; set; }
    public string Image {get; set;}
    public string FullImage {get; set;}

    public List<CardResponse> Cards { get; set; }
  }

}