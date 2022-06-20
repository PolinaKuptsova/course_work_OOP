using System;
public class Glasses : SetComponents
{
    public int size;
    public int [] sizes;
    public string color;
    public string [] colors;
    public string glassesType;
    public string [] types;

    public Glasses()
    {
        this.sizes = new int[]{1,2};
        this.colors = new string[]{"black", "white", "pink", "blue"};
        this.types = new string[]{"normal", "for short-sighted person", "for long-sighted person"};

    }

    public Glasses(string setQuality, string movieThemes, bool isRecyclablePackage) : base(setQuality, movieThemes, isRecyclablePackage)
    {
    }

    public void SizeDescription()
    {
        Console.WriteLine("Size #1 - is for kids (4 - 13 yo)\r\nSize #2 - is for adults (from 13 yo)");
    }
    public void TypeDescription()
    {
        Console.WriteLine("Types:\r\n-normal\r\n-for short-sighted person\r\n-for long-sighted person");
    }
    public void GetColors()
    {
        Console.WriteLine("Colors:\r\n-black\r\n-white\r\n-pink\r\n-blue");
    }

    public override string ToString()
    {
        return string.Format($"size: #{size}, color {color}, type {glassesType}");
    }

    public Glasses ChooseGlasses(Glasses glasses)
    {
        Console.WriteLine("Please choose glasses size");
        glasses.SizeDescription();
        string size = Console.ReadLine();
        foreach (int s in glasses.sizes)
        {
            if (int.Parse(size) == s)
            {
                glasses.size = int.Parse(size);
                Console.WriteLine("Please choose glasses color");
                glasses.GetColors();
                string color = Console.ReadLine();
                foreach (string c in glasses.colors)
                {
                    if (color == c)
                    {
                        glasses.color = color;
                        Console.WriteLine("Please choose glasses type");
                        glasses.TypeDescription();
                        string type = Console.ReadLine();
                        foreach (string t in glasses.types)
                        {
                            if (type == t)
                            {
                                glasses.glassesType = type;
                                return glasses;
                            }
                        }
                        throw new Exception("No such type! Try again later.");
                    }
                }
                throw new Exception("No such color! Try again later.");
            }
        }
        throw new Exception("No such size! Try again later.");
    }
}