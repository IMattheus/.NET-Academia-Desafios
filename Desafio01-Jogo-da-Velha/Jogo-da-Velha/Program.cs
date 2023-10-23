namespace Jogo_da_Velha
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool playing = true;
            while (playing)
            {
                string[,] board = new string[3, 3] { { " ", " ", " " }, { " ", " ", " " }, { " ", " ", " " } };
                int count = 0;
                string winner = " "; // Inicialmente representa ausência de vencedor
                bool isPlayerXTurn = true;
                string currentPlayer = isPlayerXTurn ? "X" : "O"; // Operador ternário para alternar o turno 

                // Método para imprimir o tabuleiro
                static void PrintBoard(string[,] board)
                {
                    Console.WriteLine();
                    Console.WriteLine("          [Jogo da Velha]\n");
                    Console.WriteLine("            [1] [2] [3]\n");
                    Console.WriteLine($"       [1]   {board[0, 0]} | {board[0, 1]} | {board[0, 2]}");
                    Console.WriteLine("            ---|---|---");
                    Console.WriteLine($"       [4]   {board[1, 0]} | {board[1, 1]} | {board[1, 2]}");
                    Console.WriteLine("            ---|---|---");
                    Console.WriteLine($"       [7]   {board[2, 0]} | {board[2, 1]} | {board[2, 2]}");
                    Console.WriteLine();
                }
                PrintBoard(board); // Chama o método para imprimir o tabuleiro

                // Escolhendo o primeiro jogador
                Console.WriteLine("[Bem vindo!]\n[Gostaria de começar com 'X' ou 'O'?]");
                currentPlayer = Convert.ToString(Console.ReadLine().ToUpper());
                while (currentPlayer != "X" && currentPlayer != "O")
                {
                    Console.WriteLine("\n[Operação inválida.]\n[Digite 'X' ou 'O'.]");
                    currentPlayer = Convert.ToString(Console.ReadLine().ToUpper());
                }
                if (currentPlayer == "X") 
                { 
                    isPlayerXTurn = true; 
                }
                else if (currentPlayer == "O") 
                { 
                    isPlayerXTurn = false; 
                }
                Console.Clear();
                PrintBoard(board);

                // Loop principal das jogadas
                while (true)
                {
                    currentPlayer = isPlayerXTurn ? "X" : "O";

                    Console.WriteLine($"[Jogador {currentPlayer}, escolha um número entre 1 e 9.]");
                    string playerMove = Convert.ToString(Console.ReadLine());

                    //Chamada o método para atualizar o tabuleiro
                    if (UpdateBoard(board, playerMove, currentPlayer))
                    {
                        count++;
                        isPlayerXTurn = !isPlayerXTurn; // Alternar o turno dos jogadores
                    }
                    else
                    {
                        Console.WriteLine("\n[Posição inválida ou já ocupada.]");
                        continue;
                    }
                    Console.Clear();
                    PrintBoard(board);

                    // Chama o método com a condição de vitória ou empate
                    winner = CheckWinner(board);
                    if (winner != " ")
                    {
                        Console.WriteLine($"        [Jogador {winner} venceu!]");
                        break;
                    }

                    if (count == 9)
                    {
                        Console.WriteLine("       [Deu velha! / Empate!]");
                        break;
                    }
                }
                
                // Ações após o término do jogo
                Console.WriteLine("\n[Deseja jogar novamente?]\n[Digite 'S' para iniciar uma nova partida.]\n[Digite 'N' para encerrar o programa.]");
                string choice = Convert.ToString(Console.ReadLine().ToUpper());
                while (choice != "S" && choice != "N")
                {
                    Console.WriteLine("\n[Operação inválida.]\n[Digite 'S' para iniciar uma nova partida.]\n[Digite 'N' para encerrar o programa.]");
                    choice = Convert.ToString(Console.ReadLine().ToUpper());
                }
                if (choice == "S")
                {
                    Console.Clear();
                    playing = true; // Reinicia o jogo
                }
                else if (choice == "N")
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("          [Jogo da Velha]\n");
                    Console.WriteLine("            [1] [2] [3]\n");
                    Console.WriteLine($"       [1]   [Obrigado]  ");
                    Console.WriteLine("            ---|---|---");
                    Console.WriteLine($"       [4]     [por]  ");
                    Console.WriteLine("            ---|---|---");
                    Console.WriteLine($"       [7]    [jogar!]  \n");
                    playing = false; // Encerra o programa
                }

            }
        }

        // Método para atualizar o tabuleiro com as jogadas
        static bool UpdateBoard(string[,] board, string position, string currentPlayer)
        {
            switch (position)
            {
                case "1":
                    if (board[0, 0] == " ")
                    { // Preenche espaço vazio com a jogada
                        board[0, 0] = currentPlayer;
                        return true;
                    }
                    break;
                case "2":
                    if (board[0, 1] == " ")
                    {
                        board[0, 1] = currentPlayer;
                        return true;
                    }
                    break;
                case "3":
                    if (board[0, 2] == " ")
                    {
                        board[0, 2] = currentPlayer;
                        return true;
                    }
                    break;
                case "4":
                    if (board[1, 0] == " ")
                    {
                        board[1, 0] = currentPlayer;
                        return true;
                    }
                    break;
                case "5":
                    if (board[1, 1] == " ")
                    {
                        board[1, 1] = currentPlayer;
                        return true;
                    }
                    break;
                case "6":
                    if (board[1, 2] == " ")
                    {
                        board[1, 2] = currentPlayer;
                        return true;
                    }
                    break;
                case "7":
                    if (board[2, 0] == " ")
                    {
                        board[2, 0] = currentPlayer;
                        return true;
                    }
                    break;
                case "8":
                    if (board[2, 1] == " ")
                    {
                        board[2, 1] = currentPlayer;
                        return true;
                    }
                    break;
                case "9":
                    if (board[2, 2] == " ")
                    {
                        board[2, 2] = currentPlayer;
                        return true;
                    }
                    break;
            }
            return false; // Jogada inválida
        }

        // Método para verificar condição de vitória
        static string CheckWinner(string[,] board)
        {
            // Verificando as linhas
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] && board[i, 0] != " ")
                {
                    return board[i, 0];
                }
            }

            // Verificando as colunas
            for (int j = 0; j < 3; j++)
            {
                if (board[0, j] == board[1, j] && board[1, j] == board[2, j] && board[0, j] != " ")
                {
                    return board[0, j];
                }
            }

            // Verificando as diagonais
            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[0, 0] != " ")
            {
                return board[0, 0];
            }
            if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && board[0, 2] != " ")
            {
                return board[0, 2];
            }

            return " "; // Representa jogo em andamento ou empate após a verificação confirmar ausência de vencedor
        }
    }
}