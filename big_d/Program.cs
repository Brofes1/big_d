using static System.Math;
class big_d
{
    public double mantissa;
    public double exponent;
    public static double bitShit = 0;
    public static string output = "";

    public big_d(double mantissa, double exponent)
    {
        this.mantissa = mantissa;
        this.exponent = exponent;

        this.exponent += Floor(Log2(mantissa));
        this.mantissa /= Pow(2, this.exponent - exponent);
        Console.WriteLine(this.write());
    }

    public string write(int decimals = 3)
    {
        //Console.WriteLine(this.mantissa + ", " + this.exponent);
        output = "";
        bitShit = Ceiling(Log2(Pow(10, 10) / this.mantissa));
        output += Round(this.mantissa * Pow(2, bitShit) / Pow(10, 10), decimals);
        output += "e";
        output += Ceiling(this.exponent * Log10(2));
        return output;
    }
}

class main
{ 
    public static void Main(string[] args)
    {
        big_d i = new big_d(1, 301);
    }
}

/*
using System;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

class big_d
{
    public double exponent;
    public double mantissa;
    public static int difference;
	public static string output = "";
    public static int bitShit = 0;

    public big_d(int number, double mantissa)
    {
        try
        {
            bitShit = (int)Math.Floor(Math.Log2(number));
        }
        catch
        {
            bitShit = 0;
        }
        this.exponent = mantissa;
        this.mantissa = number;
        this.exponent = bitShit + mantissa;
        this.mantissa = this.mantissa / Math.Pow(2, 29 - bitShit); //32 (Length of Int32), -2 (Safety bits), -1 (Closest pos)
        //Console.WriteLine("{0}, {1}", this.number, this.mantissa);
        Console.WriteLine(this.write());
    }

    public string write(int decimals = 3)
    {
        if (Math.Log10(this.mantissa / Math.Pow(2, 29 - this.exponent)) <= decimals)
        {
            output += this.mantissa / Math.Pow(2, 29 - this.exponent);
            return output;
        }
        else if ((Math.Log10(this.mantissa / Math.Pow(2, 29 - this.exponent)) - 1) > decimals && (Math.Log10(this.mantissa / Math.Pow(2, 29 - this.exponent)) - 1) <= Math.Pow(10, 7))
        {
            output += Math.Pow(10, (this.mantissa / Math.Pow(2, 29)) - 1);
            output += "e";
            output += Math.Floor(this.exponent * Math.Log10(2));
            return output;
        }
        else
            return "die!";
    }

    public static string write(big_d toWrite, int decimals = 3)
    {
        if ((Math.Log(toWrite.mantissa / Math.Pow(2, 29 - toWrite.exponent)) - 1) <= decimals)
        {
            output += toWrite.mantissa / Math.Pow(2, 29 - toWrite.exponent);
            return output;
        }
        else
            return "crush!";
    }

    public static bool compareGreater(big_d num1, big_d num2) // (1, 0)
    {
        if (num1.exponent < num2.exponent)
            return false;
        else if (num1.exponent > num2.exponent)
            return true;
        else if (num1.mantissa < num2.mantissa)
            return false;
        else if (num1.mantissa > num2.mantissa)
            return true;
        
        //Equal values
        return false;
    }

    public static bool compareLess(big_d num1, big_d num2)
    {
        return compareGreater(num2, num1);
    }

    public static big_d mantissaAdd(big_d num1, double add)
    {
        num1.exponent += add;
        return num1;
    }

    public static big_d numberAdd(big_d num1, int add)
    {
        num1.mantissa += add;
        return num1;
    }

    public static big_d add(big_d num1, big_d num2)
    {
        if (big_d.compareLess(num1, mantissaAdd(num2, -32)))
            return num2;
        else if (big_d.compareLess(num2, mantissaAdd(num1, -32)))
            return num1;

        //Absolute Value: Math.Abs(double);
        difference = (int)Math.Abs(num1.exponent - num2.exponent);

		//Modifying mantissa is not needed due to lack of using it
        if (compareGreater(num1, num2))
        {
            num2.mantissa = num2.mantissa * Math.Pow(2, difference);
			num1.mantissa += num2.mantissa;
			return num1;
        }
		else if (compareLess(num1, num2))
		{
			num1.mantissa = num1.mantissa * Math.Pow(2, difference);
			num2.mantissa += num1.mantissa;
			return num2;
		}
		else
		{
			num1.exponent += 1;
			return num1;
		}
    }
}

class main
{
    public static void Main(string[] args)
    {
        big_d i = new big_d(8, 90);
    }
}
*/