using System;
using System.Collections.Generic;
using System.Linq;
using static System.Environment;

namespace AdventOfCode2019.Day4
{
    public class PasswordElves
    {
        public static void Day4()
        {
            var filePath = CurrentDirectory.ToString() + "/Day3/resources/input.txt";
            var instructions = System.IO.File.ReadAllLines(filePath);
            var count = GetPasswordCount(134792, 675810);

            Console.WriteLine("Day 1: Problem 1 Answer: " + count);
        }

        public static int GetPasswordCount(int start, int end)
        {

           return Enumerable.Range(start, end + 1).Where(password => IsValidPassword(password)).Count();
        }

        public static Boolean IsValidPassword(int password)
        {
            if (password.ToString().Length != 6)
            {
                return false;
            }

            var passwordByDigit = GetPasswordByDigit(password);

            if (!DoesPasswordHaveAdjacentMatches(passwordByDigit))
            {
                return false;
            }

            if (!DoesPasswordGrowNumerically(passwordByDigit))
            {
                return false;
            }



            return true;
        }

        public static Boolean DoesPasswordHaveAdjacentMatches(List<int> password)
        {
           if (password.Count <= 1)
           {
               return false;
           }

           if (password[0].Equals(password[1]))
           {
                return true;
           }

            return DoesPasswordHaveAdjacentMatches(password.Skip(1).ToList());
                      
        }

        public static Boolean DoesPasswordGrowNumerically(List<int> password)
        {
            if (password.Count <= 1)
            {
                return true;
            }

            if (password[0] > password[1])
            {
                return false;
            }

            return DoesPasswordGrowNumerically(password.Skip(1).ToList());

        }

        public static List<int> GetPasswordByDigit(int password) =>
            password.ToString()
            .ToList()
            .Select(ch => int.Parse(ch.ToString()))
            .ToList();
    }
}
