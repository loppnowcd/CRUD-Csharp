// Importa as bibliotecas necessárias
using System;
using MySql.Data.MySqlClient;
using DotNetEnv; // Biblioteca para carregar variáveis do .env

class Program
{
    static void Main()
    {
        // Carrega as variáveis do arquivo .env
        Env.Load();

        // Obtém a senha do banco de dados a partir da variável de ambiente
        string? password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? "";

        // Verifica se a senha foi encontrada
        if (string.IsNullOrEmpty(password))
        {
            Console.WriteLine("❌ Erro: A senha do banco de dados não está definida no .env.");
            return;
        }

        // String de conexão com o banco MySQL no Aiven
        string connectionString = "Server=mysql-cdloppnow-crud-database.e.aivencloud.com;Port=23771;Database=defaultdb;User=avnadmin;Password=" + password + ";SslMode=Required;";

        try
        {
            // Cria e abre a conexão com o banco de dados
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                Console.WriteLine("✅ Conexão bem-sucedida com o banco de dados!");
            } // A conexão será fechada automaticamente ao sair do bloco "using"
        }
        catch (Exception ex)
        {
            // Captura erros e exibe detalhes
            Console.WriteLine("❌ Erro ao conectar!");
            Console.WriteLine("Detalhes: " + ex.ToString());
        }
    }
}
