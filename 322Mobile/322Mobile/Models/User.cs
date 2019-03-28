using System;
public enum Roles { User, Admin };
namespace _322Mobile.Models
{
  public class User
  {
    public int Id { get; set; }
    public Roles Name { get; set; }
    public string Username { get; set; }
    public string[] History { get; set; }
  }
}
