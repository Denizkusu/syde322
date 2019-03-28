using System;
namespace _322Mobile.Models
{
  public class PhoneReview
  {
    public int PhoneId { get; set; }
    public int Id { get; set; }
    public int SourceId { get; set; }
    public string SourceName { get; set; }
    public string Category { get; set; }
    public string ReviewText { get; set; }
    public string ReviewUrl { get; set; }

  }
}
