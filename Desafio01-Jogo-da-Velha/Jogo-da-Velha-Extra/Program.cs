namespace Jogo_da_Velha_Extra
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
                Console.WriteLine("[Bem vindo!]\n[Neste modo o computador realizará a primeira jogada.]\n[Gostaria que o jogo fosse iniciado com 'X' ou 'O'?]");
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
                                
                string computerPlayer = currentPlayer;
                while (true) // Loop principal das jogadas
                {
                    currentPlayer = isPlayerXTurn ? "X" : "O";
                    int move;

                    if (currentPlayer == computerPlayer && count == 0)
                    {
                        // Primeira jogada do computador                        
                        move = GetRandomFirstMove();
                    }
                    else if (currentPlayer == computerPlayer)
                    {
                        // Jogadas do computador
                        move = GetComputerMove(board, computerPlayer);
                    }
                    else
                    {
                        Console.WriteLine($"[Você é o 'Jogador {currentPlayer}'.]\n[Escolha um número entre 1 e 9.]");
                        string playerMove = Convert.ToString(Console.ReadLine());
                        if (!int.TryParse(playerMove, out move) || move < 1 || move > 9 || !IsMoveValid(board, move))
                        {
                            Console.WriteLine("\n[Posição inválida ou ocupada.]");
                            continue;
                        }
                    }

                    //Chamada o método para atualizar o tabuleiro
                    UpdateBoard(board, move, currentPlayer);
                    count++;
                    isPlayerXTurn = !isPlayerXTurn; // Alterna o turno dos jogadores
                    Console.Clear();
                    PrintBoard(board);

                    // Condições de vitória ou empate satisfeitas
                    winner = CheckWinner(board);
                    if (winner != " ")
                    {
                        Console.WriteLine($"       [Jogador {winner} venceu!]");
                        break;
                    }

                    if (count == 9)
                    {
                        Console.WriteLine("       [Deu velha! / Empate!]");
                        break;
                    }
                }

                // Ações após o fim da partida
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
                    playing = true; // Reinicia a partida                                       
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

        // Método para obter a primeira jogada do computador
        static int GetRandomFirstMove()
        {
            int[] firstMoves = { 1, 3, 7, 9 };
            Random random = new Random();
            return firstMoves[random.Next(firstMoves.Length)];
        }

        // Método para verificar se uma jogada é válida
        static bool IsMoveValid(string[,] board, int position)
        {
            int row = (position - 1) / 3;
            int col = (position - 1) % 3;

            return board[row, col] == " ";
        }

        // Método para atualizar o tabuleiro com as jogadas
        static void UpdateBoard(string[,] board, int position, string currentPlayer)
        {
            if (position >= 1 && position <= 9)
            {
                int row = (position - 1) / 3;
                int col = (position - 1) % 3;
                board[row, col] = currentPlayer;
            }
            else
            {
                Console.WriteLine("\n[Posição inválida ou ocupada.]");
            }
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

        // Método para obter a jogada do computador
        static int GetComputerMove(string[,] board, string computerPlayer)
        {
            int[] availableMoves = new int[9];
            int availableMoveCount = 0;

            for (int i = 1; i <= 9; i++)
            {
                if (IsMoveValid(board, i))
                {
                    availableMoves[availableMoveCount] = i;
                    availableMoveCount++;
                }
            }

            int winningMove = FindWinningMove(board, computerPlayer);
            if (winningMove != -1)
            {
                return winningMove;
            }

            int playerWinningMove = FindWinningMove(board, computerPlayer == "X" ? "O" : "X");
            if (playerWinningMove != -1)
            {
                return playerWinningMove;
            }

            // Tenta ocupar o centro
            if (IsMoveValid(board, 5))
            {
                return 5;
            }

            // Tenta ocupar um canto diagonal
            int[] corners = { 1, 3, 7, 9 };            
            foreach (int corner in corners)
            {
                if (IsMoveValid(board, corner))
                {
                    return corner;
                }
            }

            // Tenta ocupar uma borda vertical ou horizontal
            int[] edges = { 2, 4, 6, 8 };            
            foreach (int edge in edges)
            {
                if (IsMoveValid(board, edge))
                {
                    return edge;
                }
            }
            return -1; // Indica a ausência de jogadas possíveis
        }

        // Método para encontrar uma jogada que leve à vitória
        static int FindWinningMove(string[,] board, string currentPlayer)
        {
            for (int i = 1; i <= 9; i++)
            {
                if (IsMoveValid(board, i))
                {
                    // Elabora uma cópia temporária do tabuleiro para analisar as possibilidades
                    string[,] tempBoard = new string[3, 3];
                    Array.Copy(board, tempBoard, board.Length);

                    int row = (i - 1) / 3;
                    int col = (i - 1) % 3;
                    tempBoard[row, col] = currentPlayer;

                    // Verifique a possibilidade desta jogada leva à vitória
                    if (CheckWinner(tempBoard) == currentPlayer)
                    {
                        return i;
                    }
                }
            }
            return -1; // Indica a ausência jogada que leve à vitória
        }        
    }
}