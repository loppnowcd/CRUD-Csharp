// User.cs
public class User
{
    // O 'Id' será gerado automaticamente pelo banco de dados (AUTOINCREMENT)
    // mas é bom ter ele na classe para quando você buscar os dados.
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string CPF { get; set; }
    public string UserType { get; set; }
}
