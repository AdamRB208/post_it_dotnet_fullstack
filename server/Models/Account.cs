namespace post_it_dotnet_fullstack.Models;

public class Account : Profile
{
  public string Email { get; set; }
}

public class Profile : RepoItem<string>
{
  public string Name { get; set; }
  public string Picture { get; set; }

}
