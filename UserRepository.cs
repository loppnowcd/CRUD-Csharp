// UserRepository.cs
using Microsoft.Data.Sqlite; // Necessário para interagir com o SQLite
using System; // Para o Console.WriteLine e Exception
using System.Collections.Generic; // Para usar List<Usuario>

public class UserRepository
{
    // A string de conexão para o banco de dados SQLite.
    // Garante que o UserRepository saiba onde encontrar o arquivo do DB.
    private const string ConnectionString = "Data Source=meu_crud.db;";

    /// <summary>
    /// Adiciona um novo usuário ao banco de dados SQLite.
    /// </summary>
    /// <param name="usuario">O objeto Usuario contendo os dados a serem salvos.</param>
    /// <returns>True se o usuário foi adicionado com sucesso, False caso contrário.</returns>
    public bool AddUser(Usuario usuario)
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            try
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO Usuarios (Nome, Email, Fone, Endereco, CPF, TipoDeUsuario)
                    VALUES (@Nome, @Email, @Fone, @Endereco, @CPF, @TipoDeUsuario);
                ";

                command.Parameters.AddWithValue("@Nome", usuario.Nome);
                command.Parameters.AddWithValue("@Email", usuario.Email);
                command.Parameters.AddWithValue("@Fone", usuario.Fone);
                command.Parameters.AddWithValue("@Endereco", usuario.Endereco);
                command.Parameters.AddWithValue("@CPF", usuario.CPF);
                command.Parameters.AddWithValue("@TipoDeUsuario", usuario.TipoDeUsuario);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine($"Usuário '{usuario.Nome}' adicionado com sucesso ao banco de dados.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Nenhum usuário foi adicionado. Pode ter ocorrido um problema.");
                    return false;
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Erro do SQLite ao adicionar usuário: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro inesperado ao adicionar usuário: {ex.Message}");
                return false;
            }
        }
    }

    /// <summary>
    /// Consulta todos os usuários do banco de dados.
    /// </summary>
    /// <returns>Uma lista de objetos Usuario, ou uma lista vazia se nenhum usuário for encontrado.</returns>
    public List<Usuario> GetAllUsers()
    {
        var users = new List<Usuario>(); // Lista para armazenar os usuários encontrados
        using (var connection = new SqliteConnection(ConnectionString))
        {
            try
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT Id, Nome, Email, Fone, Endereco, CPF, TipoDeUsuario FROM Usuarios;";

                // ExecuteReader é usado para comandos que retornam dados (SELECT).
                using (var reader = command.ExecuteReader())
                {
                    // Lê cada linha retornada pela consulta
                    while (reader.Read())
                    {
                        users.Add(new Usuario
                        {
                            // Converte os dados do banco para o tipo correto em C#
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Nome = reader.GetString(reader.GetOrdinal("Nome")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Fone = reader.GetString(reader.GetOrdinal("Fone")),
                            Endereco = reader.GetString(reader.GetOrdinal("Endereco")),
                            CPF = reader.GetString(reader.GetOrdinal("CPF")),
                            TipoDeUsuario = reader.GetString(reader.GetOrdinal("TipoDeUsuario"))
                        });
                    }
                }
                Console.WriteLine($"Consulta de usuários: {users.Count} usuários encontrados.");
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Erro do SQLite ao consultar usuários: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro inesperado ao consultar usuários: {ex.Message}");
            }
        }
        return users; // Retorna a lista de usuários (pode ser vazia)
    }

    /// <summary>
    /// Altera os dados de um usuário existente no banco de dados.
    /// </summary>
    /// <param name="usuario">O objeto Usuario com os dados atualizados. O Id deve ser o do usuário a ser alterado.</param>
    /// <returns>True se o usuário foi alterado com sucesso, False caso contrário.</returns>
    public bool UpdateUser(Usuario usuario)
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            try
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                    UPDATE Usuarios
                    SET Nome = @Nome, Email = @Email, Fone = @Fone, Endereco = @Endereco, CPF = @CPF, TipoDeUsuario = @TipoDeUsuario
                    WHERE Id = @Id;
                ";

                command.Parameters.AddWithValue("@Nome", usuario.Nome);
                command.Parameters.AddWithValue("@Email", usuario.Email);
                command.Parameters.AddWithValue("@Fone", usuario.Fone);
                command.Parameters.AddWithValue("@Endereco", usuario.Endereco);
                command.Parameters.AddWithValue("@CPF", usuario.CPF);
                command.Parameters.AddWithValue("@TipoDeUsuario", usuario.TipoDeUsuario);
                command.Parameters.AddWithValue("@Id", usuario.Id); // Importante: usar o Id para identificar o registro a ser alterado

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine($"Usuário com Id {usuario.Id} alterado com sucesso.");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Nenhum usuário com Id {usuario.Id} foi encontrado para alteração.");
                    return false;
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Erro do SQLite ao alterar usuário: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro inesperado ao alterar usuário: {ex.Message}");
                return false;
            }
        }
    }

    /// <summary>
    /// Deleta um usuário do banco de dados pelo seu Id.
    /// </summary>
    /// <param name="id">O Id do usuário a ser deletado.</param>
    /// <returns>True se o usuário foi deletado com sucesso, False caso contrário.</returns>
    public bool DeleteUser(int id)
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            try
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Usuarios WHERE Id = @Id;";
                command.Parameters.AddWithValue("@Id", id);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine($"Usuário com Id {id} deletado com sucesso.");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Nenhum usuário com Id {id} foi encontrado para exclusão.");
                    return false;
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Erro do SQLite ao deletar usuário: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro inesperado ao deletar usuário: {ex.Message}");
                return false;
            }
        }
    }
}
