using MatrixRain;
using System;

class Program
{
    static void Main(String[] args)
    {
        // prezentuje help
        if (args[4] != null && args[4] == "help")
        {
            Console.WriteLine("1: directionUp(PRVY PARAMETER) – určuje smer, či bude kód „padať“ smerom " +
                "zdola nahor („opačná gravitácia“).\n");
            Console.WriteLine("2: color(DRUHY PARAMETER) – farba vykresľovaných znakov typu ConsoleColor\n");
            Console.WriteLine("3: delay-speed(TRETI PARAMETER) – spomalenie rýchlosti vykresľovania kódu " +
                "v type celého čísla. Určuje počet milisekúnd\n");
            Console.WriteLine("4: characters – určuje množinu znakov, z ktorých sa budú generovať znaky padajúcich kódov. " +
                "Možné hodnoty budú:\r\no Alpha – budú sa generovať abecedné (alfa) znaky od ‘A’ po ‘Z’," +
                "\r\no Numeric – budú sa generovať numerické znaky od ‘0’ po ‘9’," +
                "\r\no AlphaNumeric – budú sa generovať alfanumerické znaky od ‘0’ (nula) po ‘Z’ (písmeno Z).\n");
            Console.WriteLine("Program sa dá ukončiť stiskom ľubovoľnej klávesnice");
            System.Threading.Thread.Sleep(10000);
            Console.Clear();
        }

        Console.CursorVisible = false;
        bool ok = true;
        List<MatrixRain.MatrixRain> list = new List<MatrixRain.MatrixRain>();
        int delaySpeed = 1;
        bool directionUp = false;

        // slúži na zisťovanie parametra direction-up
        if (args[0] == null)
        {
            Console.WriteLine("Argument príkazu smeru pohybu sa nepodarilo " +
                "načitať preto bude použitá prednastavená hodnota");
            ok = false;
        }
        else
        {
            switch(args[0])
            {
                case "true":
                    directionUp = true;
                    break;
                case "false":
                    break;
                default:
                    Console.WriteLine("Argument príkazu smeru pohybu sa nepodarilo " +
                "načitať preto bude použitá prednastavená hodnota");
                    ok = false;
                    break;

            }
        }

        // slúži na zisťovanie parametra delaySpeed
        if (args[1] != null)
        {
            try
            {
                delaySpeed = Math.Abs(int.Parse(args[1]));
            }
            catch
            {
                Console.WriteLine("Zadaný parameter delaySpeed sa nepodarilo skonvertovať na celé číslo preto sa použije" +
                    " prednastavená rýchlosť");
                ok  = false;
            }
        }

        // slúži na zisťovanie parametra color
        if (!Enum.TryParse(args[2], true, out ConsoleColor foregroundColor) || args[2] == null)
        {
            Console.WriteLine("Nepodarilo sa získať farbu preto sa použije prednastavená farba");
            foregroundColor = ConsoleColor.Green;
            ok = false;
        }

        // slúži na zisťovanie parametra Character
        if (!Enum.TryParse(args[3], true, out Character character) || args[2] == null)
        {
            Console.WriteLine("Nepodarilo sa získať interval znakov preto sa použije prednastavený interval");
            character = Character.AlphaNumeric;
            ok = false;
        }

        // slúži ako výstup pre použivaťeľa, keď bol parameter zle zadaný tak sa výpíše
        if (!ok) 
        {
            System.Threading.Thread.Sleep(5000);
        }
        Console.Clear();

        // tu je už kod ktorý prezentuje "MatrixRain"
        while (!Console.KeyAvailable)
        {  
            MatrixRain.MatrixRain r = new MatrixRain.MatrixRain(directionUp, foregroundColor, character);
            if (list.Count <= 36)
            {
                list.Add(r);
            }
            foreach (var a in list)
            {
                if (a.GetSkoncil())
                {
                    list.Remove(a);
                    break;
                }
                else
                {
                    a.SimulujPad();
                }
            }
            System.Threading.Thread.Sleep(delaySpeed);
        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.Clear();
        Console.CursorVisible = true;
    }
}

