using System;

class big_d
{
    public double mantissa;
    public int number;
    public static int difference;
	public static double output;
    public static big_d bigOutput = new big_d(0, 0); //nothing could possibly go wrong with this code
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
        this.mantissa = mantissa;
        this.number = number;
        this.mantissa = bitShit + mantissa;
        this.number = this.number << 29 - bitShit; //32 (Length of Int32), -2 (Safety bits), -1 (Closest pos)
        //Console.WriteLine("{0}, {1}", this.number, this.mantissa);
        Console.WriteLine(this.write());
    }

    string write(int decimals = 3)
    {
        if ((Math.Log(this.number / Math.Pow(2, 29 - this.mantissa)) - 1) <= decimals)
        {
            output = this.number / Math.Pow(2, 29 - this.mantissa);
            return "" + output;
        }
        else 
            return "die";
    }

    static bool compareGreater(big_d num1, big_d num2) // (1, 0)
    {
        if (num1.mantissa < num2.mantissa)
            return false;
        else if (num1.mantissa > num2.mantissa)
            return true;
        else if (num1.number < num2.number)
            return false;
        else if (num1.number > num2.number)
            return true;
        
        //Equal values
        return false;
    }

    static bool compareLess(big_d num1, big_d num2)
    {
        return compareGreater(num2, num1);
    }

    static big_d mantissaAdd(big_d num1, double add)
    {
        num1.mantissa += add;
        return num1;
    }

    static big_d numberAdd(big_d num1, int add)
    {
        num1.number += add;
        return num1;
    }

    static big_d add(big_d num1, big_d num2)
    {
        if (big_d.compareLess(num1, mantissaAdd(num2, -32)))
            return num2;
        else if (big_d.compareLess(num2, mantissaAdd(num1, -32)))
            return num1;

        //Absolute Value: Math.Abs(double);
        difference = (int)Math.Abs(num1.mantissa - num2.mantissa);

		//Modifying mantissa is not needed due to lack of using it
        if (compareGreater(num1, num2))
        {
            num2.number = num2.number >> difference;
			num1.number += num2.number;
			return num1;
        }
		else if (compareLess(num1, num2))
		{
			num1.number = num1.number >> difference;
			num2.number += num1.number;
			return num2;
		}
		else
		{
			num1.mantissa += 1;
			return num1;
		}
    }
}

class main
{
    public static void Main(string[] args)
    {
        big_d i = new big_d(7, 2);
    }
}