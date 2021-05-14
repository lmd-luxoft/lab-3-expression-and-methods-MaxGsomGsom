using System;
using System.Linq;

namespace XO
{
    class Program
    {
        private static string PlayerName1, PlayerName2;
        private static char[] cells = Enumerable.Repeat(Symbols.Empty, 9).ToArray();

        private static void show_cells()
        {
            Console.Clear();

            Console.WriteLine(
$@"Числа клеток:
-1-|-2-|-3-
-4-|-5-|-6-
-7-|-8-|-9-
Текущая ситуация (---пустой):
-{cells[0]}-|-{cells[1]}-|-{cells[2]}-
-{cells[3]}-|-{cells[4]}-|-{cells[5]}-
-{cells[6]}-|-{cells[7]}-|-{cells[8]}-"
            );        
        }

        private static void make_move(char symbol)
        {
            var cell = readCell($"{nameFromSymbol(symbol)}, введите номер ячейки,сделайте свой ход:");
            while (cell > 8 || cell < 0 || cells[cell] == Symbols.O || cells[cell] == Symbols.X)
            {
                cell = readCell("Введите номер правильного ( 1-9 ) или пустой ( --- ) клетки , чтобы сделать ход:");
                Console.WriteLine();
            }
            cells[cell] = symbol;

        }
        private static char check()
        {
            if (checkDiagonals())
                return cell(1, 1);

            for (int i = 0; i < 3; i++)
                if (checkLine(i))
                    return cell(i, 0);
                else if (checkLine(i, true))
                    return cell(0, i);
            return Symbols.Empty;
        }

        private static void result(char winnerChar)
        {
            var winner = nameFromSymbol(winnerChar);
            var loser = winner == PlayerName1 ? PlayerName2 : PlayerName1;
            Console.WriteLine($"{winner} вы выиграли поздравляем {loser} а вы проиграли...");
        }

        public static void Main()
        {
            do
            {
                Console.Write("Введите имя первого игрока : ");
                PlayerName1 = Console.ReadLine();

                Console.Write("Введите имя второго игрока: ");
                PlayerName2 = Console.ReadLine();
                Console.WriteLine();
            } while (PlayerName1 == PlayerName2);

            show_cells();
            for (int move = 1; move <= 9; move++)
            {
                make_move(move % 2 != 0 ? Symbols.X : Symbols.O);
                show_cells();

                if (move < 5) continue;
                var win = check();
                if (win != Symbols.Empty)
                {
                    result(win);
                    break;
                }

            }
        }

        private static char cell(int row, int col, bool switchDimensions = false)
        {
            if (switchDimensions)
                return cells[col * 3 + row];
            return cells[row * 3 + col];
        }

        private static bool checkLine(int i, bool switchDimensions = false)
        {
            return cell(i, 0, switchDimensions) == cell(i, 1, switchDimensions)
                   && cell(i, 1, switchDimensions) == cell(i, 2, switchDimensions);
        }

        private static bool checkDiagonals()
        {
            var mid = cell(1, 1);
            return cell(0, 2) == mid && mid == cell(2, 0)
                   || cell(0, 0) == mid && mid == cell(2, 2);
        }

        private static string nameFromSymbol(char symbol)
            => symbol == Symbols.X ? PlayerName1 : PlayerName2;

        private static int readCell(string output)
        {
            string raw_cell;
            int cell;
            do
            {
                Console.Write(output);
                raw_cell = Console.ReadLine();
            }
            while (!int.TryParse(raw_cell, out cell));
            return cell - 1;
        }
    }
}
