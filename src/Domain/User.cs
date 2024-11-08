namespace Domain;
public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string BirthDate { get; set; }
    public string Phone { get; set; }
    public List<string> FavoriteGenres { get; set; } = [];
    public List<string> FavoriteAuthors { get; set; } = [];
}