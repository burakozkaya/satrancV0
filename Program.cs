namespace satrancV0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[,] satrancT = new string[8, 8];
            satrancTahtasiOlustur(satrancT);
            satrancTahtasi(satrancT);
            string cPiece;
            string move;
            while (true)
            {
                cPiece = Console.ReadLine().ToUpper();
                move = Console.ReadLine().ToUpper();
                tasHareketi(satrancT, cPiece, move);
                satrancTahtasi(satrancT);
            }
        }
        static bool tasHareketi(string[,] satrancT, string cPiece, string move)
        {
            var tempXCP = (char)(((int)Convert.ToChar(cPiece.Substring(0, 1))) - 65);
            var tempYCP = 7 - (int.Parse(cPiece.Substring(1, 1)) - 1);
            var tempXM = (char)(((int)Convert.ToChar(move.Substring(0, 1))) - 65);
            var tempYM = 7 - (int.Parse(move.Substring(1, 1)) - 1);
            bool piyon = true;
            if ((tempYM >= 8 && tempYM >= 0) || (tempXM >= 8 && tempXM >= 0) || (tempXCP >= 8 && tempXCP >= 0) || (tempYCP >= 8 && tempYCP >= 0))
                return false;
            switch (satrancT[tempYCP, tempXCP])
            {
                case "P":
                    if (Math.Abs(tempYCP - tempYM) <= 2 && piyon)
                    {
                        Move(tempXCP, tempYCP, tempXM, tempYM, satrancT);
                        piyon = false;
                        return true;
                    }
                    else if (Math.Abs(tempYCP - tempYM) <= 1 && Math.Abs(tempYCP - tempYM) != 0)
                    {
                        Move(tempXCP, tempYCP, tempXM, tempYM, satrancT);
                        return true;
                    }
                    else
                        return false;
                    break;
                case "K":
                    if (tempXCP == tempXM || tempYCP == tempYM)
                    {
                        Move(tempXCP, tempYCP, tempXM, tempYM, satrancT);
                        return true;
                    }
                    else
                        return false;
                    break;
                case "F":
                    if (Math.Abs(tempYCP - tempYM) == 4 && Math.Abs(tempXCP - tempXM) == 4)
                    {
                        Move(tempXCP, tempYCP, tempXM, tempYM, satrancT);
                        return true;
                    }
                    else
                        return false;
                    break;
                case "A":
                    if (Math.Abs(tempYCP - tempYM) == 2 && Math.Abs(tempXM - tempXCP) == 2)
                    {
                        Move(tempXCP, tempYCP, tempXM, tempYM, satrancT);
                        return true;
                    }
                    else
                        return false;
                    break;
                case "V":
                    if (Math.Abs(tempYCP - tempXM) == 4 && Math.Abs(tempXCP - tempYM) == 4 || Math.Abs(tempYCP - tempYM) == 2 && Math.Abs(tempXM - tempXCP) == 2)
                    {
                        Move(tempXCP, tempYCP, tempXM, tempYM, satrancT);
                        return true;
                    }
                    else
                        return false;
                    break;
                case "Ş":
                    if (Math.Abs(tempYCP - tempYM) == 1 || Math.Abs(tempXCP - tempXM) == 1)
                    {
                        Move(tempXCP, tempYCP, tempXM, tempYM, satrancT);
                        return true;
                    }
                    else
                        return false;
                    break;
            }
            return true;
        }
        static void satrancTahtasiOlustur(string[,] satrancTahtasi)
        {

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i == 1 || i == 6)
                    {
                        satrancTahtasi[i, j] = "P";
                    }
                    else if ((i == 0 || i == 7))
                    {
                        if ((j == 0 || j == 7))
                            satrancTahtasi[i, j] = "K";
                        else if (j == 1 || j == 6)
                            satrancTahtasi[i, j] = "A";
                        else if (j == 2 || j == 5)
                            satrancTahtasi[i, j] = "F";
                        else if (j == 3)
                            satrancTahtasi[i, j] = "V";
                        else
                            satrancTahtasi[i, j] = "Ş";

                    }

                    else
                    {
                        satrancTahtasi[i, j] = "*";
                    }
                }
            }
        }
        static void satrancTahtasi(string[,] satrancTahtasi)
        {

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(satrancTahtasi[i, j]);
                }
                Console.WriteLine();
            }
        }
        private static void Move(int tempXCP, int tempYCP, int tempXM, int tempYM, string[,] satrancT)
        {
            satrancT[tempYM, tempXM] = satrancT[tempYCP, tempXCP];
            satrancT[tempYCP, tempXCP] = "*";
        }
    }
}
