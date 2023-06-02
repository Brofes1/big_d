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
        this.exponent = exponent;

        if (mantissa != 0)
        {
            this.exponent += Floor(Log2(Abs(mantissa)));
            this.mantissa /= Math.Pow(2, this.exponent - exponent);
            this.mantissa = Math.Pow(Abs(this.mantissa), Log2(baseNum) + 1);
            this.exponent *= Log2(baseNum);
        }
        else
            this.exponent = 0;

        this.cleanup();
    }

    #region Tools
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
 
    public big_d clone()
    {
        return new big_d(this.mantissa, this.exponent, 2);
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

    public static bool operator >(big_d num1, big_d num2)
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

    public static bool operator <(big_d num1, big_d num2)
    {
        return num2 > num1;
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
    #endregion

    #region Addition
    public static big_d operator +(big_d num1, big_d num2)
    {
        big_d addNum1 = clone(num1);
        big_d addNum2 = clone(num2);

        difference = Abs(addNum1.exponent - addNum2.exponent);
        if (addNum1 > exponentAdd(addNum2, -32))
        {
            return addNum1;
        }
        if (addNum2 > exponentAdd(addNum1, -32))
        {
            return addNum2;
        }
        if (addNum1 > addNum2)
        {
            addNum2.mantissa /= Math.Pow(2, difference);
            addNum1.mantissa += addNum2.mantissa;
            return addNum1.cleanup();
        }
        if (addNum1 > addNum2)
        {
            addNum1.mantissa /= Math.Pow(2, difference);
            addNum2.mantissa += addNum1.mantissa;
            return addNum2.cleanup();
        }
        else
            return exponentAdd(addNum1, 1);
    }

    public static big_d operator +(big_d num1, double num2)
    {
        big_d addNum2 = num1.clone();

        num2 /= Math.Pow(2, addNum2.exponent);
        addNum2.mantissa += num2;
        return addNum2.cleanup();
    }

    public static big_d operator +(double num1, big_d num2)
    {
        big_d addNum1 = num2.clone();

        num1 /= Math.Pow(2, addNum1.exponent);
        addNum1.mantissa += num1;
        return addNum1.cleanup();
    }

    public static big_d operator +(big_d num1, float num2)
    {
        big_d addNum1 = num1.clone();

        if (addNum1.exponent > Log2(num2) + 8)
        {
            num2 /= (float)Math.Pow(2, addNum1.exponent);
            addNum1.mantissa += num2;
            return addNum1.cleanup();
        }
        else
            return addNum1;
    }

    public static big_d operator +(decimal num1, big_d num2)
    {
        big_d addNum2 = num2.clone();

        if (addNum2.exponent > Log2((double)num1) + 8)
        {
            num1 /= (decimal)Math.Pow(2, addNum2.exponent);
            addNum2.mantissa += (double)num1;
            return addNum2.cleanup();
        }
        else
            return addNum2;
    }

    public static big_d operator +(big_d num1, decimal num2)
    {
        big_d addNum1 = num1.clone();

        if (addNum1.exponent > Log2((double)num2) + 8)
        {
            num2 /= (decimal)Math.Pow(2, addNum1.exponent);
            addNum1.mantissa += (double)num2;
            return addNum1.cleanup();
        }
        else
            return addNum1;
    }

    public static big_d operator +(float num1, big_d num2)
    {
        big_d addNum2 = num2.clone();

        if (addNum2.exponent > Log2(num1) + 8)
        {
            num1 /= (float)Math.Pow(2, addNum2.exponent);
            addNum2.mantissa += num1;
            return addNum2.cleanup();
        }
        else
            return addNum2;

    }

    public static big_d operator +(big_d num1, sbyte num2)
    {
        big_d addNum1 = num1.clone();

        if (addNum1.exponent > Log2(num2) + 8)
        {
            num2 /= (sbyte)Math.Pow(2, addNum1.exponent);
            addNum1.mantissa += num2;
            return addNum1.cleanup();
        }
        else
            return addNum1;
    }

    public static big_d operator +(sbyte num1, big_d num2)
    {
        big_d addNum2 = num2.clone();

        if (addNum2.exponent > Log2(num1) + 8)
        {
            num1 /= (sbyte)Math.Pow(2, addNum2.exponent);
            addNum2.mantissa += num1;
            return addNum2.cleanup();
        }
        else
            return addNum2;

    }

    public static big_d operator +(big_d num1, byte num2)
    {
        big_d addNum1 = num1.clone();

        if (addNum1.exponent > Log2(num2) + 8)
        {
            num2 /= (byte)Math.Pow(2, addNum1.exponent);
            addNum1.mantissa += num2;
            return addNum1.cleanup();
        }
        else
            return addNum1;
    }

    public static big_d operator +(byte num1, big_d num2)
    {
        big_d addNum2 = num2.clone();

        if (addNum2.exponent > Log2(num1) + 8)
        {
            num1 /= (byte)Math.Pow(2, addNum2.exponent);
            addNum2.mantissa += num1;
            return addNum2.cleanup();
        }
        else
            return addNum2;

    }

    public static big_d operator +(big_d num1, short num2)
    {
        big_d addNum1 = num1.clone();

        if (addNum1.exponent > Log2(num2) + 16)
        {
            num2 /= (short)Math.Pow(2, addNum1.exponent);
            addNum1.mantissa += num2;
            return addNum1.cleanup();
        }
        else
            return addNum1;
    }

    public static big_d operator +(short num1, big_d num2)
    {
        big_d addNum2 = num2.clone();

        if (addNum2.exponent > Log2(num1) + 16)
        {
            num1 /= (short)Math.Pow(2, addNum2.exponent);
            addNum2.mantissa += num1;
            return addNum2.cleanup();
        }
        else
            return addNum2;

    }

    public static big_d operator +(big_d num1, ushort num2)
    {
        big_d addNum1 = num1.clone();

        if (addNum1.exponent > Log2(num2) + 16)
        {
            num2 /= (ushort)Math.Pow(2, addNum1.exponent);
            addNum1.mantissa += num2;
            return addNum1.cleanup();
        }
        else
            return addNum1;
    }

    public static big_d operator +(ushort num1, big_d num2)
    {
        big_d addNum2 = num2.clone();

        if (addNum2.exponent > Log2(num1) + 16)
        {
            num1 /= (ushort)Math.Pow(2, addNum2.exponent);
            addNum2.mantissa += num1;
            return addNum2.cleanup();
        }
        else
            return addNum2;

    }

    public static big_d operator +(big_d num1, int num2)
    {
        big_d addNum1 = num1.clone();

        if (addNum1.exponent > Log2(num2) + 32)
        {
            num2 /= (int)Math.Pow(2, addNum1.exponent);
            addNum1.mantissa += num2;
            return addNum1.cleanup();
        }
        else
            return addNum1;
    }

    public static big_d operator +(int num1, big_d num2)
    {
        big_d addNum2 = num2.clone();

        if (addNum2.exponent > Log2(num1) + 32)
        {
            num1 /= (int)Math.Pow(2, addNum2.exponent);
        addNum2.mantissa += num1;
        return addNum2.cleanup();
        }
        else
            return addNum2;

    }

    public static big_d operator +(big_d num1, uint num2)
    {
        big_d addNum1 = num1.clone();

        if (addNum1.exponent > Log2(num2) + 32)
        {
            num2 /= (uint)Math.Pow(2, addNum1.exponent);
            addNum1.mantissa += num2;
            return addNum1.cleanup();
        }
        else
            return addNum1;
    }

    public static big_d operator +(uint num1, big_d num2)
    {
        big_d addNum2 = num2.clone();

        if (addNum2.exponent > Log2(num1) + 32)
        {
            num1 /= (uint)Math.Pow(2, addNum2.exponent);
            addNum2.mantissa += num1;
            return addNum2.cleanup();
        }
        else
            return addNum2;

    }

    public static big_d operator +(big_d num1, long num2)
    {
        big_d addNum1 = num1.clone();

        if (addNum1.exponent > Log2(num2) + 64)
        {
            num2 /= (long)Math.Pow(2, addNum1.exponent);
            addNum1.mantissa += num2;
            return addNum1.cleanup();
        }
        else
            return addNum1;
    }

    public static big_d operator +(long num1, big_d num2)
    {
        big_d addNum2 = num2.clone();

        if (addNum2.exponent > Log2(num1) + 64)
        {
            num1 /= (long)Math.Pow(2, addNum2.exponent);
            addNum2.mantissa += num1;
            return addNum2.cleanup();
        }
        else
            return addNum2;

    }

    public static big_d operator +(big_d num1, ulong num2)
    {
        big_d addNum1 = num1.clone();

        if (addNum1.exponent > Log2(num2) + 64)
        {
            num2 /= (ulong)Math.Pow(2, addNum1.exponent);
            addNum1.mantissa += num2;
            return addNum1.cleanup();
        }
        else
            return addNum1;
    }

    public static big_d operator +(ulong num1, big_d num2)
    {
        big_d addNum2 = num2.clone();

        if (addNum2.exponent > Log2(num1) + 64)
        {
            num1 /= (ulong)Math.Pow(2, addNum2.exponent);
            addNum2.mantissa += num1;
            return addNum2.cleanup();
        }
        else
            return addNum2;

    }
    #endregion

    #region Multiplication
    public static big_d operator *(big_d num1, big_d num2)
    {
        big_d multiplyNum1 = clone(num1);
        big_d multiplyNum2 = clone(num2);

        multiplyNum1.mantissa *= multiplyNum2.mantissa;
        multiplyNum1.exponent += multiplyNum2.exponent;
        multiplyNum1.cleanup();

        return multiplyNum1;
    }

    public static big_d operator *(big_d num1, double num2)
    {
        big_d multiplyNum1 = clone(num1);

        multiplyNum1.exponent += Log2(num2);
        multiplyNum1.mantissa *= Math.Pow(2, Log2(num2 % 1));
        multiplyNum1.cleanup();

        return multiplyNum1;
    }

    public static big_d operator *(double num1, big_d num2)
    {
        big_d multiplyNum1 = clone(num2);

        multiplyNum1.exponent += Log2(num1);
        multiplyNum1.mantissa *= Math.Pow(2, Log2(num1 % 1));
        multiplyNum1.cleanup();

        return multiplyNum1;
    }

    public static big_d operator *(big_d num1, float num2)
    {
        big_d multiplyNum1 = clone(num1);

        multiplyNum1.exponent += Log2(num2);
        multiplyNum1.mantissa *= Math.Pow(2, Log2(num2 % 1));
        multiplyNum1.cleanup();

        return multiplyNum1;
    }

    public static big_d operator *(float num1, big_d num2)
    {
        big_d multiplyNum1 = clone(num2);

        multiplyNum1.exponent += Log2(num1);
        multiplyNum1.mantissa *= Math.Pow(2, Log2(num1 % 1));
        multiplyNum1.cleanup();

        return multiplyNum1;
    }

    public static big_d operator *(big_d num1, decimal num2)
    {
        big_d multiplyNum1 = clone(num1);

        multiplyNum1.exponent += Log2((double)num2);
        multiplyNum1.mantissa *= Math.Pow(2, Log2((double)num2) % 1);
        multiplyNum1.cleanup();

        return multiplyNum1;
    }

    public static big_d operator *(decimal num1, big_d num2)
    {
        big_d multiplyNum1 = clone(num2);

        multiplyNum1.exponent += Log2((double)num1);
        multiplyNum1.mantissa *= Math.Pow(2, Log2((double)num1) % 1);
        multiplyNum1.cleanup();

        return multiplyNum1;
    }

    public static big_d operator *(big_d num1, sbyte num2)
    {
        big_d multiplyNum1 = clone(num1);

        multiplyNum1.mantissa *= num2;
        multiplyNum1.cleanup();

        return multiplyNum1;
    }

    public static big_d operator *(sbyte num1, big_d num2)
    {
        big_d multiplyNum2 = clone(num2);

        multiplyNum2.mantissa *= num1;
        multiplyNum2.cleanup();

        return multiplyNum2;
    }

    public static big_d operator *(big_d num1, byte num2)
    {
        big_d multiplyNum1 = clone(num1);

        multiplyNum1.mantissa *= num2;
        multiplyNum1.cleanup();

        return multiplyNum1;
    }

    public static big_d operator *(byte num1, big_d num2)
    {
        big_d multiplyNum2 = clone(num2);

        multiplyNum2.mantissa *= num1;
        multiplyNum2.cleanup();

        return multiplyNum2;
    }

    public static big_d operator *(big_d num1, short num2)
    {
        big_d multiplyNum1 = clone(num1);

        multiplyNum1.mantissa *= num2;
        multiplyNum1.cleanup();

        return multiplyNum1;
    }

    public static big_d operator *(short num1, big_d num2)
    {
        big_d multiplyNum2 = clone(num2);

        multiplyNum2.mantissa *= num1;
        multiplyNum2.cleanup();

        return multiplyNum2;
    }

    public static big_d operator *(big_d num1, ushort num2)
    {
        big_d multiplyNum1 = clone(num1);

        multiplyNum1.mantissa *= num2;
        multiplyNum1.cleanup();

        return multiplyNum1;
    }

    public static big_d operator *(ushort num1, big_d num2)
    {
        big_d multiplyNum2 = clone(num2);

        multiplyNum2.mantissa *= num1;
        multiplyNum2.cleanup();

        return multiplyNum2;
    }

    public static big_d operator *(big_d num1, int num2)
    {
        big_d multiplyNum1 = clone(num1);

        multiplyNum1.mantissa *= num2;
        multiplyNum1.cleanup();

        return multiplyNum1;
    }

    public static big_d operator *(int num1, big_d num2)
    {
        big_d multiplyNum2 = clone(num2);

        multiplyNum2.mantissa *= num1;
        multiplyNum2.cleanup();

        return multiplyNum2;
    }

    public static big_d operator *(big_d num1, uint num2)
    {
        big_d multiplyNum1 = clone(num1);

        multiplyNum1.mantissa *= num2;
        multiplyNum1.cleanup();

        return multiplyNum1;
    }

    public static big_d operator *(uint num1, big_d num2)
    {
        big_d multiplyNum2 = clone(num2);

        multiplyNum2.mantissa *= num1;
        multiplyNum2.cleanup();

        return multiplyNum2;
    }

    public static big_d operator *(big_d num1, long num2)
    {
        big_d multiplyNum1 = clone(num1);

        multiplyNum1.mantissa *= num2;
        multiplyNum1.cleanup();

        return multiplyNum1;
    }

    public static big_d operator *(long num1, big_d num2)
    {
        big_d multiplyNum2 = clone(num2);

        multiplyNum2.mantissa *= num1;
        multiplyNum2.cleanup();

        return multiplyNum2;
    }

    public static big_d operator *(big_d num1, ulong num2)
    {
        big_d multiplyNum1 = clone(num1);

        multiplyNum1.mantissa *= num2;
        multiplyNum1.cleanup();

        return multiplyNum1;
    }

    public static big_d operator *(ulong num1, big_d num2)
    {
        big_d multiplyNum2 = clone(num2);

        multiplyNum2.mantissa *= num1;
        multiplyNum2.cleanup();

        return multiplyNum2;
    }
    #endregion

    #region Subtraction
    public static big_d operator -(big_d num1, big_d num2) => num1 + (num2 * -1);

    public static big_d operator -(big_d num1, double num2) => num1 + (num2 * -1);

    public static big_d operator -(double num1, big_d num2) => num1 + (num2 * -1);

    public static big_d operator -(big_d num1, float num2) => num1 + (num2 * -1);

    public static big_d operator -(float num1, big_d num2) => num1 + (num2 * -1);

    public static big_d operator -(big_d num1, decimal num2) => num1 + (num2 * -1);

    public static big_d operator -(decimal num1, big_d num2) => num1 + (num2 * -1);

    public static big_d operator -(big_d num1, sbyte num2) => num1 + (num2 * -1);

    public static big_d operator -(sbyte num1, big_d num2) => num1 + (num2 * -1);

    public static big_d operator -(big_d num1, byte num2) => num1 + (num2 * -1);

    public static big_d operator -(byte num1, big_d num2) => num1 + (num2 * -1);

    public static big_d operator -(big_d num1, short num2) => num1 + (num2 * -1);

    public static big_d operator -(short num1, big_d num2) => num1 + (num2 * -1);

    public static big_d operator -(big_d num1, ushort num2) => num1 + (num2 * -1);

    public static big_d operator -(ushort num1, big_d num2) => num1 + (num2 * -1);

    public static big_d operator -(big_d num1, int num2) => num1 + (num2 * -1);

    public static big_d operator -(int num1, big_d num2) => num1 + (num2 * -1);

    public static big_d operator -(big_d num1, uint num2) => num1 + (num2 * -1);

    public static big_d operator -(uint num1, big_d num2) => num1 + (num2 * -1);

    public static big_d operator -(big_d num1, long num2) => num1 + (num2 * -1);

    public static big_d operator -(ulong num1, big_d num2) => num1 + (num2 * -1);

    public static big_d operator -(big_d num1, nint num2) => num1 + (num2 * -1);

    public static big_d operator -(nint num1, big_d num2) => num1 + (num2 * -1);
    #endregion

    #region Power
    public static big_d Pow(big_d num1, double power)
    {
        big_d powNum1 = clone(num1);

        powNum1.mantissa = Math.Pow(powNum1.mantissa, power);
        powNum1.exponent *= power;
        return powNum1.cleanup();
    }
    #endregion

    #region Division
    public static big_d operator /(big_d num1, big_d num2) => num1 * Pow(num2, -1);
    #endregion

    public override string ToString()
    {
        this.cleanup();
        if (this.exponent + Log2(Abs(this.mantissa)) < Log2(1000))
            return Round(Math.Pow(2, this.exponent) * this.mantissa, 9) + "";
        else if (this.exponent < 1000000000 * Log10(2))
        {
            output = "";
            bitCrap = Log10(2) * this.exponent;
            shift = (int)Round(mLog10(Abs(this.mantissa) * Math.Pow(10, bitCrap % 1)), 9);
            output += Round(this.mantissa * Math.Pow(10, bitCrap % 1) / Math.Pow(10, shift), 3);
            output += "e";
            output += Floor(this.exponent * Log10(2) + shift);
            return output;
        }
        else
            return "e" + Round(this.exponent * Log10(2) / Math.Pow(10, Log10(this.exponent * Log10(2))), 4) + "e" + Log10(this.exponent * Log10(2));
    }
}

class main
{ 
    public static void Main(string[] args)
    {
        big_d i = new big_d(1, 2);
		big_d j = new big_d(-1, 4);
        big_d m = new big_d();
        int integer = 5;
        Console.WriteLine(i);
        Console.WriteLine(j);
        Console.WriteLine(i + j);
        Console.WriteLine(i * j);
        Console.WriteLine(m);
        Console.WriteLine(big_d.Pow(i, 5));
        Console.WriteLine(i / new big_d(1, 2, 2));
        Console.WriteLine(new big_d(1.5625, 6, 2));

        big_d x = new big_d(1, 2);
        big_d y = new big_d(1, 1);
        big_d z = new big_d(1, 3);
        big_d zz = new big_d(1, 4);
        Console.WriteLine("\n" + y);
        Console.WriteLine(x);
        Console.WriteLine(z);
        Console.WriteLine(zz);
        Console.WriteLine(new big_d(1, 64) + new big_d(1,16));
        Console.WriteLine(new big_d(1, Math.Pow(10, 100)));
    }
}
