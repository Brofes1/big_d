using static System.Math;

class big_d
{
    private static byte sign;
    private static double bitCrap = 0;
    private static string output = "";
    private static double difference;
    private static int shift;

    public double mantissa;
    public double exponent;

    public big_d(double mantissa = 0, double exponent = 0, int baseNum = 10) //Basenum will be used for adding the ability to do b10 and other bases
    {
        this.mantissa = mantissa;
        this.exponent = exponent * Floor(Log2(baseNum));
        this.mantissa *= Log2(baseNum) / Floor(Log2(baseNum));

        if (baseNum != 2)
        {
            Console.WriteLine("m    " + this.mantissa);
            Console.WriteLine("e    " + this.exponent);
        }

        if (mantissa != 0)
        {
            this.exponent += Floor(Log2(Abs(mantissa)));
            this.mantissa /= Math.Pow(2, this.exponent - exponent);
        }
        else
            this.exponent = 0;

        this.cleanup();
    }

    private static double mLogB(double mantissa, byte baseNum)
    {
        sign = (byte)(mantissa / Abs(mantissa));

        if (mantissa == 0)
            return 0;
        if (sign == 255)
            return (Log(Abs(mantissa)) / Log(baseNum)) * -1;
        else
            return (Log(Abs(mantissa)) / Log(baseNum));
    }

    private static double mLog2(double mantissa)
    {
        return mLogB(mantissa, 2);
    }

    private static double mLog10(double mantissa)
    {
        return mLogB(mantissa, 10);
    }

    public static big_d clone(big_d Obj)
    {
        return new big_d(Obj.mantissa, Obj.exponent, 2);
    }

    public big_d cleanup()
    {
        if (this.mantissa != 0)
        {
            bitCrap = Floor(Log2(Abs(this.mantissa)));
            this.exponent += bitCrap;
            this.mantissa /= Math.Pow(2, bitCrap);
        }

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

    public static big_d operator +(big_d num1, big_d num2)
    {
        big_d addNum1 = clone(num1);
        big_d addNum2 = clone(num2);

        difference = Abs(addNum1.exponent - addNum2.exponent);
        if (compareLess(addNum1, exponentAdd(addNum2, -32)))
        {
            return addNum1;
        }
        if (compareLess(addNum2, exponentAdd(addNum1, -32)))
        {
            return addNum2;
        }
        if (compareGreater(addNum1, addNum2))
        {
            addNum2.mantissa /= Math.Pow(2, difference);
            addNum1.mantissa += addNum2.mantissa;
            return addNum1.cleanup();
        }
        if (compareLess(addNum1, addNum2))
        {
            addNum1.mantissa /= Math.Pow(2, difference);
            addNum2.mantissa += addNum1.mantissa;
            return addNum2.cleanup();
        }
        else
            return exponentAdd(addNum1, 1);
    }

    public static big_d operator *(big_d num1, big_d num2)
    {
        big_d multiplyNum1 = clone(num1);
        big_d multiplyNum2 = clone(num2);

        multiplyNum1.mantissa *= multiplyNum2.mantissa;
        multiplyNum1.exponent += multiplyNum2.exponent;
        multiplyNum1.cleanup();

        return multiplyNum1;
    }

    public static big_d operator *(big_d num1, int num2)
    {
        big_d multiplyNum1 = clone(num1);

        multiplyNum1.mantissa *= num2;
        multiplyNum1.cleanup();

        return multiplyNum1;
    }

    public static big_d operator -(big_d num1, big_d num2) => num1 + (num2 * -1);

    public static big_d Pow(big_d num1, double power)
    {
        big_d powNum1 = clone(num1);

        powNum1.mantissa = Math.Pow(powNum1.mantissa, power);
        powNum1.exponent *= power;
        return powNum1.cleanup();
    }

    public static big_d operator /(big_d num1, big_d num2) => num1 * Pow(num2, -1);

    public override string ToString()
    {
        output = "";
        bitCrap = Log10(2) * this.exponent;
        shift = (int)Round(mLog10(this.mantissa * Math.Pow(10, bitCrap % 1)), 9);
        output += Round(this.mantissa * Math.Pow(10, bitCrap % 1) / Math.Pow(10, shift * -1), 9);
        output += "e";
        output += Floor(this.exponent * Log10(2) + (shift * -1));
        return output;
    }
}

class main
{ 
    public static void Main(string[] args)
    {
        big_d i = new big_d(1, 2, 2);
		big_d j = new big_d(-1, 4, 2);
        big_d m = new big_d();
        Console.WriteLine(i);
        Console.WriteLine(j);
        Console.WriteLine(i + j);
        Console.WriteLine(i * j);
        Console.WriteLine(m);
        Console.WriteLine(big_d.Pow(i, 5));
        Console.WriteLine(i / new big_d(1, 2, 2));

        big_d x = new big_d(1, 2);
        big_d y = new big_d(1, 1);
        Console.WriteLine("\n" + x);
        Console.WriteLine(y);
        Console.WriteLine(x + y);
        Console.WriteLine("\n" + (new big_d(-1, 2, 2) / new big_d(1, 1, 2)));
    }
}
