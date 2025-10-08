/**
 * Create Project using dotnet new <template> --output <projectName>
 * Build Project using dotnet build
 */

using PrimeCalc.Math;

const int FROM = 1;
const int TO = 100;

var prime = new List<int>();

for (int i = FROM; i <= TO; i++) {
    if (PrimeChecker.IsPrime(i)) {
        prime.Add(i);
    }
}

Console.WriteLine($"Prime Numbers in [{FROM} - {TO}] = " + string.Join(",", prime));