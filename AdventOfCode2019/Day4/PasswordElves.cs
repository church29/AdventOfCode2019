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
            var count = GetPasswordCount(134792, 675810, false);

            Console.WriteLine("Day 4: Problem 1 Answer: " + count);
            count = GetPasswordCount(134792, 675810, true);
            Console.WriteLine("Day 4: Problem 2 Answer: " + count);
        }

        public static int GetPasswordCount(int start, int end, Boolean additionalCriteria)
        {

            var validPasswords = Enumerable.Range(start, end - start).Where(password => IsValidPassword(password, additionalCriteria)).ToList();
            return validPasswords.Count();
        }

        public static Boolean IsValidPassword(int password, Boolean additionalCriteria)
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

            if (additionalCriteria && !DoesPasswordHaveAdjacentMatchPair(passwordByDigit))
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

        public static Boolean DoesPasswordHaveAdjacentMatchPair(List<int> password)
        {
            if (password.Count <= 1)
            {
                return false;
            }

            if (password[0].Equals(password[1]) && (password.Count() == 2 || !password[1].Equals(password[2])))
            {
                return true;
            }

            if (password[0].Equals(password[1]) && password.Count() > 2 && password[1].Equals(password[2]))
            {
                var firstDigit = password[0];
                while (firstDigit == password[0] && password.Count() > 1)
                {
                    password = password.Skip(1).ToList();
                }
                    
                return DoesPasswordHaveAdjacentMatchPair(password);
            }

            return DoesPasswordHaveAdjacentMatchPair(password.Skip(1).ToList());

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
