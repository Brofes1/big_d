class big_d
{
    public double mantissa;
    public int number;

    public big_d()
    {
        this.mantissa = 0;
        this.number = 0;
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
        
        //what? how did you get here
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

    static void add(big_d num1, big_d num2)
    {
        if (0 == 0)
        {

        }
    }
}