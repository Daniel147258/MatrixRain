using MatrixRain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixRain;

class MatrixRain
{
    private char[] znaky;
    private Character rozmer;
    private int poziciaRiadku;
    private bool directionUp;
    private int poziciaStlpca;
    private bool skoncil;
    private int dlzka;
    private Enum color;
    public MatrixRain(bool directionUp, Enum color, Character rozmerNadobudanychHodnot)
    {
        Random rand = new Random();
        this.directionUp = directionUp;
        this.rozmer = rozmerNadobudanychHodnot;
        this.color = color;
        this.poziciaRiadku = 0;
        if (this.directionUp) 
            this.poziciaRiadku = Console.WindowHeight - 1;

        this.poziciaStlpca = rand.Next(0, Console.WindowWidth);
        this.dlzka = rand.Next(3, Console.WindowHeight);
        this.znaky = new char[this.dlzka];
        this.skoncil = false;
    }
    public void NaplnPole()
    {
        this.znaky = new char[this.dlzka];
        Random rand = new Random();
        for (int i = 0; i < this.znaky.Length; i++)
        {
            int hodnota = 0;
            switch (this.rozmer)
            {
                case Character.AlphaNumeric:
                    hodnota = rand.Next(48, 123);
                    while ((hodnota >= 58 && hodnota <= 64) || (hodnota >= 91 && hodnota <= 96))
                    {
                        hodnota = rand.Next(48, 123);
                    }
                    this.znaky[i] = (char)hodnota;
                    break;
                case Character.Alpha:
                    hodnota = rand.Next(65, 123);
                    while ((hodnota >= 91 && hodnota <= 96))
                    {
                        hodnota = rand.Next(65, 123);
                    }
                    this.znaky[i] = (char)hodnota;
                    break;
                case Character.Numeric:
                    this.znaky[i] = (char)rand.Next(48, 58);
                    break;
                default:
                    break;
            }
        }
    }

    public void Vykresli()
    {
        int pocitadlo = 0;
        Console.ForegroundColor = (ConsoleColor)this.color;
        if (!this.directionUp)
        {
            this.NaplnPole();
            foreach (char ch in this.znaky)
            {
                if (pocitadlo + 1 == this.znaky.Length)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                if (this.poziciaRiadku + pocitadlo < Console.WindowHeight - 1)
                {
                    Console.SetCursorPosition(this.poziciaStlpca, this.poziciaRiadku + pocitadlo);
                    Console.WriteLine(ch);
                    Console.ForegroundColor = (ConsoleColor)this.color;
                    pocitadlo++;
                }
                else
                {
                    this.dlzka--;
                    break;
                }
            }
        }
        else
        {
            this.NaplnPole();
            foreach (char ch in this.znaky)
            {
                if (pocitadlo + 1 == this.znaky.Length)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                if (this.poziciaRiadku - pocitadlo > -1)
                {
                    Console.SetCursorPosition(this.poziciaStlpca, this.poziciaRiadku - pocitadlo);
                    Console.WriteLine(ch);
                    Console.ForegroundColor = (ConsoleColor)this.color;
                    pocitadlo++;
                }
                else
                {
                    this.dlzka--;
                    break;
                }
            }
        }
    }

    public void SimulujPad()
    {
        if (!this.directionUp)
        {
            if(this.poziciaRiadku >= Console.WindowHeight)
            {
                this.skoncil = true;
            }
                this.Vykresli();
            if (this.poziciaRiadku < Console.WindowHeight)
            {
                Console.SetCursorPosition(this.poziciaStlpca, this.poziciaRiadku);
                Console.Write(" ");
            }
            this.poziciaRiadku++;
        }
        else
        {
            if (this.poziciaRiadku < 0)
            {
                this.skoncil = true;
            }
            this.Vykresli();
            if (this.poziciaRiadku > -1)
            {
                Console.SetCursorPosition(this.poziciaStlpca, this.poziciaRiadku);
                Console.Write(" ");
            }
            this.poziciaRiadku--;

        }
    }

    public bool GetSkoncil()
    {
        return this.skoncil;
    }
}
