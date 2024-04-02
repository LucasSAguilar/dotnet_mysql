using MySql.Data.MySqlClient;

class Program
{
    static void Main()
    {

        Console.WriteLine("Escreva o que deseja fazer: ");
        Console.WriteLine("1: Coletar dados");
        Console.WriteLine("2: Enviar dados");
        String respostaUsuario = Console.ReadLine() ?? "";


        MySqlConnection conn = abrirConexao();
        
        switch (respostaUsuario)
        {
            case "1":
                coletarDados(conn);
                break;
            case "2":
                inserirDados(conn);
                break;

        }

    }

    static MySqlConnection abrirConexao()
    {
        /*
           String para conexão:

         * | Server = servidor que será usado 
         * | Database = Nome do banco de dados 
         * | Uid = Usuário de acesso 
         * | Pwd = Senha do usuário
        */

        String string_conexao = "Server=localhost;Database=teste_dotnet;Uid=root;Pwd=1234";
        MySqlConnection conn = new MySqlConnection(string_conexao);
        return conn;
    }


    static void inserirDados(MySqlConnection conn)
    {
        Console.Clear();


        Console.WriteLine("Qual seu usuário?");
        String nome = Console.ReadLine() ?? "";

        Console.WriteLine("Qual sua senha?");
        String senha = Console.ReadLine() ?? "";

        String sql_comando = $"INSERT INTO usuarios (nome, senha) VALUES (@nome, @senha)";
        
        try
        {
            conn.Open();
            Console.WriteLine("Conexão aberta");
            MySqlCommand comando = new MySqlCommand(sql_comando, conn);
            comando.Parameters.AddWithValue("@nome", nome);
            comando.Parameters.AddWithValue("@senha", senha);

            int linhasAlteradas = comando.ExecuteNonQuery();
            if ( linhasAlteradas > 0 )
            {
                Console.WriteLine("Dados enviados com sucesso");
            }

        } catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        } finally { 
            conn.Close();
        }
    }

    static void coletarDados(MySqlConnection conn)
    {
        Console.Clear();
        // Comando que será usado no MySQL:

        String sql_comando = "SELECT * FROM usuarios";
        try
        {
            conn.Open();
            Console.WriteLine("Conexão aberta");
            MySqlCommand comando = new MySqlCommand(sql_comando, conn);
            MySqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                /* Colocamos entre () por ordem no banco de dados:
                   O primeiro dado é o 'Nome', então para acessar é o (0),
                   O segundo dado é a 'Senha', então para acessar é o (1),
                   Se tivesse um terceiro dado, seria (2)
                */

                Console.WriteLine($"Nome: {reader.GetString(0)}, Senha: {reader.GetString(1)}");
                Console.WriteLine("___________________________________");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            conn.Close();
            Console.WriteLine("Conexão finalizada");
        };
    }
}

