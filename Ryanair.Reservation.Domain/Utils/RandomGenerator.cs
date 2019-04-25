using System;
using System.Text;

namespace Ryanair.Reservation.Domain.Utils
{
    public static class RandomGenerator
    {
        // Generate a random number between two numbers    
        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        // Generate a random string with a given size    
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        // Generate a random password    
        public static string RandomReservationNumber(int min, int max, int stringSize)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(stringSize, false));
            builder.Append(RandomNumber(min, max));            
            return builder.ToString();
        }
    }
}
