using MySql.Data.MySqlClient;

class Program
{
    static void Main()
    {
        /*
           String para conexão:

         * | Server = servidor que será usado 
         * | Database = Nome do banco de dados 
         * | Uid = Usuário de acesso 
         * | Pwd = Senha do usuário
        */
        String string_conexao = "Server=localhost;Database=teste_dotnet;Uid=root;Pwd=1234";

                

        // Comando que será usado no MySQL:

            String sql_comando = "SELECT * FROM usuarios";

        // ___________________________________________

            MySqlConnection conn = new MySqlConnection(string_conexao);
        
        try {
            conn.Open();
            Console.WriteLine("Conexão aberta");
            MySqlCommand comando = new MySqlCommand(sql_comando, conn);
            MySqlDataReader reader = comando.ExecuteReader();

            while (reader.Read()) {
                // Colocamos entre () por ordem no banco de dados

                /*
                    O primeiro dado é o 'Nome', então para acessar é o (0),
                    O segundo dado é a 'Senha', então para acessar é o (1),
                    Se tivesse um terceiro dado, seria (2)
                 */

                Console.WriteLine($"Nome: {reader.GetString(0)}, Senha: {reader.GetString(1)}");
                Console.WriteLine("___________________________________");
            }

        } catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        } finally {
            conn.Close();
            Console.WriteLine("Conexão finalizada");
        };
    }
}

/*
 * 
 * Banco de dados usado: 
 
    CREATE DATABASE teste_dotnet;

    USE teste_dotnet;

    CREATE TABLES usuarios(
	    nome varchar(255),
	    senha varchar(255)
    );

    INSERT INTO usuarios (nome, senha) VALUES 
        ("Lucas", "1234"),
        ("Joao", "5678");
 
 */