namespace post_it_dotnet_fullstack.Models;

public class Account : Profile
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string Picture { get; set; }
}

public class Profile
{
  public string Email { get; set; }

}
