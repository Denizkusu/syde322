using System;
namespace _322Mobile.Models
{
  public class Phone
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }
    public string[] Providers { get; set; }
  }
}
