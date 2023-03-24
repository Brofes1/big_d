using System;
using System.Security.Cryptography.X509Certificates;
using static System.Math;

class big_d
{
    public double mantissa;
    public double exponent;
    public static double bitCrap = 0;
    public static string output = "";
    public static double difference;

    public big_d(double mantissa = 0, double exponent = 0)
    {
        this.mantissa = mantissa;
        this.exponent = exponent;

        this.exponent += Floor(Log2(mantissa));
        this.mantissa /= Pow(2, this.exponent - exponent);
    }

    public string write(int decimals = 3) //Note: may need to be fixed due to double-digit numbers
    {
        output = "";
        bitCrap = Log10(2) * this.exponent;
        output += Round(this.mantissa * Pow(10, bitCrap % 1), decimals);
        output += "e";
        output += Floor(this.exponent * Log10(2));
        return output;
    }

    public static string write(big_d toWrite, int decimals = 3)
    {
        output = "";
        bitCrap = Log10(2) * toWrite.exponent;
        output += Round(toWrite.mantissa * Pow(10, bitCrap % 1), decimals);
        output += "e";
        output += Floor(toWrite.exponent * Log10(2));
        return output;
    }

    public static big_d clone(big_d Obj)
    {
        return new big_d(Obj.mantissa, Obj.exponent);
    }

    public big_d cleanup()
    {
        bitCrap = Floor(Log2(this.mantissa));
        this.exponent += bitCrap;
        this.mantissa /= Pow(2, bitCrap);
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
        big_d addE = clone(num1);
        
        addE.exponent += add;
        return addE;
    }

    public static big_d mantissaAdd(big_d num1, int add)
    {
        big_d addM = clone(num1);
        
        addM.mantissa += add;
        return addM;
    }

    public static big_d add(big_d num1, big_d num2)
    {
        big_d addNum1 = clone(num1);
        big_d addNum2 = clone(num2);
        
        difference = Abs(addNum1.exponent - addNum2.exponent);
        if (compareLess(addNum1, exponentAdd(addNum2, -32)))
        {
            return addNum1;
        } exponentAdd(addNum2, 32);
        if (compareLess(addNum2, exponentAdd(addNum1, -32)))
        {
            return addNum2;
        } exponentAdd(addNum1, 32);
        if (compareGreater(addNum1, addNum2))
        {
            addNum2.mantissa /= Pow(2, difference);
            addNum1.mantissa += addNum2.mantissa;
            return addNum1.cleanup();
        }
        else if (compareLess(addNum1, addNum2))
        {
            addNum1.mantissa /= Pow(2, difference);
            addNum2.mantissa += addNum1.mantissa;
            return addNum2.cleanup();
        }
        else
            return exponentAdd(addNum1, 1);
    }

    public static big_d multiply(big_d num1, big_d num2)
    {
        big_d multiplyNum1 = clone(num1);
        big_d multiplyNum2 = clone(num2);

        multiplyNum1.mantissa *= multiplyNum2.mantissa;
        multiplyNum1.exponent += multiplyNum2.exponent;
        multiplyNum1.cleanup();

        return multiplyNum1;
    }
}

class main
{ 
    public static void Main(string[] args)
    {
        big_d i = new big_d(1, 2);
		big_d j = new big_d(-1, 4);
        big_d k = new big_d();
        big_d l = new big_d();
        big_d m = new big_d();
        k = big_d.add(i, j);
        l = big_d.multiply(i, j);
        Console.WriteLine(i.write());
        Console.WriteLine(j.write());
        Console.WriteLine(k.write());
        Console.WriteLine(l.write());
        Console.WriteLine(m.write());
        Console.WriteLine(big_d.add(i, m));
    }
}
