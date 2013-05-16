﻿using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Please enter integer number:");
        Console.WriteLine(LastDiggitEnglish(int.Parse(Console.ReadLine())));
    }
    static string LastDiggitEnglish(int number)
    {
        number %= 10;
        switch (number)
        {
            case 0: return "Zero";
            case 1: return "One";
            case 2: return "Two";
            case 3: return "Three";
            case 4: return "Four";
            case 5: return "Five";
            case 6: return "Six";
            case 7: return "Seven";
            case 8: return "Eight";
            case 9: return "Nine";
            default: return "Please enter positive integer.";
        }
        
    }
}

