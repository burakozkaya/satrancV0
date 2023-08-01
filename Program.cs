namespace satrancV0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string[,,] satrancT = new string[8, 8, 3];
            satrancTahtasiOlustur(satrancT);
            satrancTahtasi(satrancT);
            bool sah = true;
            string cPiece;
            string move;
            int count = 1;
            while (true)
            {
                Console.WriteLine(count % 2 == 1 ? "Player 1'in sırası" : "Player 2'nin sırası");
                cPiece = Console.ReadLine().ToUpper();
                if (!validPlayerCheck(count, satrancT, cPiece))
                {
                    Console.WriteLine("Hatalı oyuncu taşı");
                    continue;
                }
                move = Console.ReadLine().ToUpper();
                if (!validEatCheck(count, satrancT, cPiece, move))
                {
                    Console.WriteLine("Kendi taşını yiyemezsin");
                    continue;
                }

                if (!tasHareketi(satrancT, cPiece, move, ref sah))
                {
                    Console.WriteLine("Hatalı taş hareketi");
                    continue;
                }
                if (!sah)
                    break;
                count++;
                satrancTahtasi(satrancT);
            }
            satrancTahtasi(satrancT);
            Console.WriteLine(count % 2 == 1 ? "Player 1 kazandı" : "Player 2 kazandı");
        }
        static bool tasHareketi(string[,,] satrancT, string cPiece, string move,ref bool sah)
        {
            var tempXCP = xConverter(cPiece);
            var tempYCP = yConverter(cPiece);
            var tempXM = xConverter(move);
            var tempYM = yConverter(move);
            int temp = 0;
            if ((tempYM >= 8 && tempYM >= 0) || (tempXM >= 8 && tempXM >= 0) || (tempXCP >= 8 && tempXCP >= 0) || (tempYCP >= 8 && tempYCP >= 0))
                return false;
            return validTasHareket(satrancT, tempYCP, tempXCP, tempYM, tempXM,ref sah);
        }
        static bool validTasHareket(string[,,] satrancT, int tempYCP, int tempXCP, int tempYM, int tempXM, ref bool sah)
        {
            int i;
            int final;
            switch (satrancT[tempYCP, tempXCP, 0])
            {
                case "P":
                    return piyonCheck(tempXCP, tempXM, tempYCP, tempYM, satrancT);
                    break;
                case "K":
                    return kaleCheck(tempXCP, tempXM, tempYCP, tempYM, satrancT);
                    break;
                case "F":
                    return filCheck(tempYCP, tempYM, tempXCP, tempXM, satrancT);
                    break;
                case "A":
                    return atCheck(tempYCP, tempYM, tempXCP, tempXM, satrancT);
                    break;
                case "V":
                    return vezirCheck(tempYCP, tempYM, tempXCP, tempXM, satrancT);
                    break;
                case "Ş":
                    return sahCheck(tempYCP, tempYM, tempXCP, tempXM, satrancT, ref sah);
                    break;
                default: return false;
            }
        }
        static void satrancTahtasiOlustur(string[,,] satrancTahtasi)
        {

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i == 1 || i == 6)
                    {
                        if (i == 1)
                        {
                            satrancTahtasi[i, j, 0] = "P";
                            satrancTahtasi[i, j, 1] = "P2";
                            satrancTahtasi[i, j, 2] = "FM";
                        }
                        else
                        {

                            satrancTahtasi[i, j, 0] = "P";
                            satrancTahtasi[i, j, 1] = "P1";
                            satrancTahtasi[i, j, 2] = "FM";
                        }

                    }
                    else if ((i == 0 || i == 7))
                    {
                        if (i == 0)
                        {
                            satrancTahtasi[i, j, 1] = "P2";
                            satrancTahtasi[i, j, 2] = "FM";
                        }
                        if (i == 7)
                        {
                            satrancTahtasi[i, j, 1] = "P1";
                            satrancTahtasi[i, j, 2] = "FM";
                        }
                        if ((j == 0 || j == 7))
                            satrancTahtasi[i, j, 0] = "K";
                        else if (j == 1 || j == 6)
                            satrancTahtasi[i, j, 0] = "A";
                        else if (j == 2 || j == 5)
                            satrancTahtasi[i, j, 0] = "F";
                        else if (j == 3)
                            satrancTahtasi[i, j, 0] = "V";
                        else
                            satrancTahtasi[i, j, 0] = "Ş";

                    }

                    else
                    {
                        satrancTahtasi[i, j, 0] = "*";
                        satrancTahtasi[i, j, 1] = "NPC";
                    }
                }
            }
        }
        static void satrancTahtasi(string[,,] satrancTahtasi)
        {

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(satrancTahtasi[i, j, 0]);
                }
                Console.WriteLine();
            }
        }
        static void Move(int tempXCP, int tempYCP, int tempXM, int tempYM, string[,,] satrancT)
        {
            satrancT[tempYM, tempXM, 0] = satrancT[tempYCP, tempXCP, 0];
            satrancT[tempYCP, tempXCP, 0] = "*";
            satrancT[tempYM, tempXM, 1] = satrancT[tempYCP, tempXCP, 1];
            satrancT[tempYCP, tempXCP, 1] = "NPC";
        }
        static bool validPlayerCheck(int count, string[,,] satrancT, string cPiece)
        {
            var tempXCP = xConverter(cPiece);
            var tempYCP = yConverter(cPiece);
            if (count % 2 == 1)
            {
                if (satrancT[tempYCP, tempXCP, 1] == "P1")
                    return true;
                else
                    return false;
            }
            else
            {
                if (satrancT[tempYCP, tempXCP, 1] == "P2")
                    return true;
                else
                    return false;
            }


        }
        static bool kaleCheck(int tempXCP, int tempXM, int tempYCP, int tempYM, string[,,] satrancT)
        {
            int i;
            int final;

            if (tempXCP == tempXM || tempYCP == tempYM)
            {
                Move(tempXCP, tempYCP, tempXM, tempYM, satrancT);
                return true;

            }
            else
            {

                return false;
            }
        }
        static bool filCheck(int tempYCP, int tempYM, int tempXCP, int tempXM, string[,,] satrancT)
        {

            if (Math.Abs((tempYCP - tempYM)) == Math.Abs(tempXCP - tempXM))
            {
                Move(tempXCP, tempYCP, tempXM, tempYM, satrancT);
                return true;
            }
            else
                return false;
        }
        static bool atCheck(int tempYCP, int tempYM, int tempXCP, int tempXM, string[,,] satrancT)
        {
            if (Math.Abs(tempYCP - tempYM) == 2 && Math.Abs(tempXM - tempXCP) == 1)
            {
                Move(tempXCP, tempYCP, tempXM, tempYM, satrancT);
                return true;
            }
            else
                return false;
        }
        public static bool vezirCheck(int tempYCP, int tempYM, int tempXCP, int tempXM, string[,,] satrancT)
        {
            if (filCheck(tempYCP, tempYM, tempXCP, tempXM, satrancT) || kaleCheck(tempXCP, tempXM, tempYCP, tempYM, satrancT))
            {
                Move(tempXCP, tempYCP, tempXM, tempYM, satrancT);
                return true;
            }
            else
                return false;
        }

        public static bool sahCheck(int tempYCP, int tempYM, int tempXCP, int tempXM, string[,,] satrancT, ref bool sah)
        {
            if (Math.Abs(tempYCP - tempYM) == 1 || Math.Abs(tempXCP - tempXM) == 1)
            {
                sah = false;
                Move(tempXCP, tempYCP, tempXM, tempYM, satrancT);
                return true;
            }
            else
                return false;
        }
        static bool piyonCheck(int tempYCP, int tempYM, int tempXCP, int tempXM, string[,,] satrancT)
        {
            if (Math.Abs(tempYCP - tempYM) <= 2 && satrancT[tempYCP, tempXCP, 2] == "FM")
            {
                Move(tempXCP, tempYCP, tempXM, tempYM, satrancT);
                satrancT[tempYCP, tempXCP, 2] = "NFM";
                return true;
            }
            else if ((Math.Abs(tempYCP - tempYM) <= 1 && (Math.Abs(tempYCP - tempYM) != 0) || (Math.Abs(tempXCP - tempXM) == 1 && satrancT[tempYM, tempXM, 0] == "P")))
            {
                Move(tempXCP, tempYCP, tempXM, tempYM, satrancT);
                return true;
            }
            else
                return false;
        }
        static bool validEatCheck(int count, string[,,] satrancT, string cPiece, string move)
        {
            var tempXCP = xConverter(cPiece);
            var tempYCP = yConverter(cPiece);
            var tempXM = xConverter(move);
            var tempYM = yConverter(move);
            if (satrancT[tempYCP, tempXCP, 1] == satrancT[tempYM, tempXM, 1])
                return false;
            else
                return true;
        }
        static int xConverter(string cPiece)
        {
            return (char)(((int)Convert.ToChar(cPiece.Substring(0, 1))) - 65);
        }
        static int yConverter(string cPiece)
        {
            return 7 - (int.Parse(cPiece.Substring(1, 1)) - 1);
        }
    }
}
