using Microsoft.Data.Sqlite; // Necessário para interagir com o SQLite
using System;
using System.IO; // Para verificar se o arquivo do banco de dados existe

public class DatabaseInitializer
{
    // Define o nome do arquivo do seu banco de dados SQLite
    // Ele será criado na mesma pasta onde sua aplicação for executada
    private const string DbFileName = "meu_crud.db";

    // A string de conexão informa ao C# onde encontrar o banco de dados
    private static string ConnectionString = $"Data Source={DbFileName};";

    /// <summary>
    /// Inicializa o banco de dados, criando-o se não existir
    /// e garantindo que as tabelas necessárias estejam presentes
    /// </summary>
    public static void InitializeDatabase()
    {
        // Verifica se o arquivo do banco de dados já existe.
        // Se não existir, ele será criado automaticamente na primeira conexão.
        if (!File.Exists(DbFileName))
        {
            Console.WriteLine($"O arquivo do banco de dados '{DbFileName}' não foi encontrado. Criando novo banco de dados...");
        }
        else
        {
            Console.WriteLine($"O banco de dados '{DbFileName}' já existe.");
        }

        // 'using' garante que a conexão será fechada e liberada automaticamente
        // mesmo que ocorra um erro.
        using (var connection = new SqliteConnection(ConnectionString))
        {
            try
            {
                // Abre a conexão com o banco de dados.
                connection.Open();
                Console.WriteLine("Conexão com o banco de dados SQLite estabelecida com sucesso.");

                // --- COMANDO SQL PARA CRIAR A TABELA 'USUARIOS' ---
                // O 'IF NOT EXISTS' garante que a tabela só será criada se ainda não existir.
                var createUsuariosTableCommand = connection.CreateCommand();
                createUsuariosTableCommand.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Usuarios (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nome TEXT NOT NULL,
                        Email TEXT NOT NULL UNIQUE,
                        Fone TEXT NOT NULL,
                        Endereco TEXT NOT NULL,
                        CPF TEXT NOT NULL UNIQUE,
                        TipoDeUsuario TEXT NOT NULL
                    );
                ";
                createUsuariosTableCommand.ExecuteNonQuery(); // Executa o comando SQL
                Console.WriteLine("Tabela 'Usuarios' verificada/criada com sucesso.");

                // --- ADICIONE AQUI OS COMANDOS PARA SUAS OUTRAS 4 OU 5 TABELAS ---
                // Exemplo para uma futura tabela 'Produtos':
                // var createProdutosTableCommand = connection.CreateCommand();
                // createProdutosTableCommand.CommandText = @"
                //     CREATE TABLE IF NOT EXISTS Produtos (
                //         Id INTEGER PRIMARY KEY AUTOINCREMENT,
                //         Nome TEXT NOT NULL,
                //         Preco REAL NOT NULL,
                //         Estoque INTEGER NOT NULL DEFAULT 0
                //     );
                // ";
                // createProdutosTableCommand.ExecuteNonQuery();
                // Console.WriteLine("Tabela 'Produtos' verificada/criada com sucesso.");


                Console.WriteLine("Processo de inicialização do banco de dados concluído.");
            }
            catch (SqliteException ex)
            {
                // Captura e exibe qualquer erro específico do SQLite
                Console.WriteLine($"Erro ao inicializar o banco de dados: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro inesperado: {ex.Message}");
            }
        }
    }
}


public class Program
{
    public static void Main(string[] args)
    {
        // Chama o método para inicializar o banco de dados e as tabelas
        DatabaseInitializer.InitializeDatabase();

        // A partir daqui, você pode continuar com o restante da sua lógica
        // da aplicação (ex: iniciar seu servidor web, mostrar o formulário, etc.).
        Console.WriteLine("\nPressione qualquer tecla para sair...");
        Console.ReadKey();
    }
}
*/
