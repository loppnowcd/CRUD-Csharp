using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly string _connectionString = "Server=mysql-cdloppnow-crud-database.e.aivencloud.com;Port=23771;Database=defaultdb;User=avnadmin;Password=SUA_SENHA_AQUI;SslMode=Required;";

    // Obter todos os usuários
    [HttpGet]
    public async Task<IActionResult> GetUsuarios()
    {
        var usuarios = new List<object>();
        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = new MySqlCommand("SELECT * FROM usuarios", connection);
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                usuarios.Add(new
                {
                    Id = reader["id"],
                    Nome = reader["nome"],
                    Email = reader["email"],
                    Telefone = reader["telefone"],
                    Cpf = reader["cpf"],
                    TipoUsuario = reader["tipo_usuario"]
                });
            }
        }
        return Ok(usuarios);
    }

    // Adicionar um novo usuário
    [HttpPost]
    public async Task<IActionResult> AddUsuario([FromBody] Usuario usuario)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = new MySqlCommand("INSERT INTO usuarios (nome, email, telefone, cpf, tipo_usuario) VALUES (@Nome, @Email, @Telefone, @Cpf, @TipoUsuario)", connection);
            command.Parameters.AddWithValue("@Nome", usuario.Nome);
            command.Parameters.AddWithValue("@Email", usuario.Email);
            command.Parameters.AddWithValue("@Telefone", usuario.Telefone);
            command.Parameters.AddWithValue("@Cpf", usuario.Cpf);
            command.Parameters.AddWithValue("@TipoUsuario", usuario.TipoUsuario);
            await command.ExecuteNonQueryAsync();
        }
        return CreatedAtAction(nameof(GetUsuarios), new { id = usuario.Id }, usuario);
    }
}

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Cpf { get; set; }
    public string TipoUsuario { get; set; }
}