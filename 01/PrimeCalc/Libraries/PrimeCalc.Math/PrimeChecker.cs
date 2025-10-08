namespace PrimeCalc.Math;

using static System.Math;

public class PrimeChecker {
    public static bool IsPrime(int number) {
        if (number % 2 == 0) {
            return false;
        }
        
        for (int i = 3; i < Sqrt(number); i += 2) {
            if (number % i == 0) {
                return false;
            }  
        }

        return true;
    }
}
