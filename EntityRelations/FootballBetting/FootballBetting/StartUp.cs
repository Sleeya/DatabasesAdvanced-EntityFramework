
using System;
using System.Linq;

namespace MatrirowsChessProblem
{
    class Program
    {
        class Point
        {
            public int rows { get; set; }
            public int columns { get; set; }

            public void MoveUp()
            {
                rows--;
            }
            public void MoveDown()
            {
                rows++;
            }
            public void MoveRight()
            {
                columns++;
            }
            public void MoveLeft()
            {
                columns--;
            }

        }



        static void Main(string[] args)
        {

            int[] dimensions = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
               .Select(int.Parse).ToArray();

            char[,] table = new char[dimensions[0], dimensions[1]];
            for (int rows = 0; rows < table.GetLength(0); rows++)
            {
                char[] input = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(char.Parse).ToArray();

                for (int columns = 0; columns < table.GetLength(1); columns++)
                {
                    table[rows, columns] = input[columns];
                }
            }
            Console.WriteLine();
            Point start = Find(table, 'S');


            Print(table, table.GetLength(1));
            Console.WriteLine("End is at: " + CompleteMaze(start, table, table.GetLength(1)).rows + ", " + CompleteMaze(start, table, table.GetLength(1)).columns + Environment.NewLine);
            Print(table, table.GetLength(1));

        }




        private static Point Find(char[,] table, char sym)
        {
            Point point = new Point();
            for (int rows = 0; rows < table.GetLength(0); rows++)
            {
                for (int columns = 0; columns < table.GetLength(1); columns++)
                {
                    if (table[rows, columns] == sym)
                    {
                        point = new Point() { rows = rows, columns = columns };
                        return point;
                    }
                }
            }
            return point;
        }




        private static void Initialize(char[,] table, int N, char v)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    table[i, j] = '0';
                }
            }
        }





        private static Point CompleteMaze(Point start, char[,] table, int length)
        {

            if (table[start.rows, start.columns] == 'E')
            {
                return start;
            }
            else
            {
                if (start.rows != length - 1)
                    if (table[start.rows + 1, start.rows] != 'W' && table[start.rows + 1, start.rows] == '0')
                    {
                        start.MoveDown();
                        if (table[start.rows, start.columns] == 'E')
                        {
                            return start;
                        }
                        table[start.rows, start.columns] = 'V';
                        //Print(table, length);
                        return CompleteMaze(start, table, length);
                    }
                if (start.columns != length - 1)
                    if (table[start.columns + 1, start.rows] != 'W' && table[start.columns + 1, start.rows] == '0')
                    {
                        start.MoveRight();
                        if (table[start.rows, start.columns] == 'E')
                        {
                            return start;
                        }
                        table[start.rows, start.columns] = '>';
                        // Print(table, length);
                        return CompleteMaze(start, table, length);
                    }
                if (start.rows != 0)
                    if (table[start.columns, start.rows - 1] != 'W' && table[start.columns, start.rows - 1] == '0')
                    {
                        start.MoveUp();
                        if (table[start.rows, start.columns] == 'E')
                        {
                            return start;
                        }
                        table[start.rows, start.columns] = '^';
                        // Print(table, length);
                        return CompleteMaze(start, table, length);
                    }
                if (start.columns != 0)
                    if (table[start.columns - 1, start.rows] != 'W' && table[start.columns - 1, start.rows] == '0')
                    {
                        start.MoveLeft();
                        if (table[start.rows, start.columns] == 'E')
                        {
                            return start;
                        }
                        table[start.rows, start.columns] = '<';
                        // Print(table, length);
                        return CompleteMaze(start, table, length);
                    }
            }
            return start;
        }





        private static void Print(char[,] table, int N)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write(string.Format("{0} ", table[i, j]));
                }
                Console.Write(Environment.NewLine);
            }
            Console.WriteLine("\n-------------------");
        }
    }
}