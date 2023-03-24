using System;
using static System.Math;

class big_d
{
    public double mantissa;
    public double exponent;
    public static double bitShit = 0;
    public static string output = "";
    public static double difference;

    public big_d(double mantissa, double exponent)
    {
        this.mantissa = mantissa;
        this.exponent = exponent;

        this.exponent += Floor(Log2(mantissa));
        this.mantissa /= Pow(2, this.exponent - exponent);
        Console.WriteLine(this.write());
    }

    public string write(int decimals = 3) //Note: may need to be fixed due to double-digit numbers
    {
        output = "";
        bitShit = Log10(2) * this.exponent;
        output += Round(this.mantissa * Pow(10, bitShit % 1), decimals);
        output += "e";
        output += Floor(this.exponent * Log10(2));
        return output;
    }

    public static string write(big_d toWrite, int decimals = 3)
    {
        output = "";
        bitShit = Log10(2) * toWrite.exponent;
        output += Round(toWrite.mantissa * Pow(10, bitShit % 1), decimals);
        output += "e";
        output += Floor(toWrite.exponent * Log10(2));
        return output;
    }

    public big_d cleanup()
    {
        bitShit = Floor(Log2(this.mantissa));
        this.exponent += bitShit;
        this.mantissa /= Pow(2, bitShit);
		return this;
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

    public static big_d exponentAdd(big_d num1, double add)
    {
        num1.exponent += add;
        return num1;
    }

    public static big_d mantissaAdd(big_d num1, int add)
    {
        num1.mantissa += add;
        return num1;
    }

    public static big_d add(big_d num1, big_d num2)
    {
        difference = Abs(num1.exponent - num2.exponent);
        if (compareLess(num1, exponentAdd(num2, -32)))
            return num1;
        else if (compareLess(num2, exponentAdd(num1, -32)))
            return num2;
        else if (compareGreater(num1, num2))
        {
            num2.mantissa /= Pow(2, difference);
            num1.mantissa += num2.mantissa;
            return num1.cleanup();
        }
        else if (compareLess(num1, num2))
        {
            num1.mantissa /= Pow(2, difference);
            num2.mantissa += num1.mantissa;
            return num2.cleanup();
        }
        else
            return mantissaAdd(num1, 1);
    }
}

class main
{ 
    public static void Main(string[] args)
    {
        big_d i = new big_d(1, 1024);
		big_d j = new big_d(1, 1024);
		Console.WriteLine(big_d.write(big_d.add(i ,j)));
    }
}
