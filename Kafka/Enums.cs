using System;

namespace Kafka
{
    public enum Manufacturers { GE, LplusG }
    public enum Models { FocusAZ, I210, kV2c }

    public class Enums
    {
        public static T GetRandomEnum<T>()
        {
            Array values = Enum.GetValues(typeof(T));
            Random random = new Random();
            return (T)values.GetValue(random.Next(values.Length));
        }
    }
}