// See https://aka.ms/new-console-template for more information
// Implicit Usings Project Setting can be used to reduce the amount of imports/usings that are needed to run a prg 

using HashDictionary.Impl;

IDictionary<string, int> TestIndexerAndAdd() {
    var cityInfo = new HashDictionary<string, int>();

    cityInfo["Hagenberg"] = 2_100;
    cityInfo["Linz"]      = 100_000;
    cityInfo["Linz"]      = 200_000;
    cityInfo.Add("Wien", 1_700_000);
    try {
        cityInfo.Add("Wien", 1_000_000);
    }
    catch (ArgumentException e) {
        Console.WriteLine(e.GetType().Name + e.Message);
    } // throws ArgumentException
    
    Console.WriteLine($"dict[\"Hagenberg\"] = {cityInfo["Hagenberg"]}");
    Console.WriteLine($"dict[\"Linz\"]      = {cityInfo["Linz"]}");
    Console.WriteLine($"dict[\"Wien\"]      = {cityInfo["Wien"]}");
    try {
        Console.WriteLine($"dict[\"Graz\"]      = {cityInfo["Graz"]}"); // does not exist
    }
    catch (KeyNotFoundException e) {
        Console.WriteLine(e.GetType().Name + e.Message);
    }

    Console.WriteLine("\n");
    return cityInfo;
}

void PrintDictionary<TK, TV>(IDictionary<TK, TV> dict) {
    Console.WriteLine("PrintDictionary:");
    foreach (KeyValuePair<TK, TV> entry in dict) {
        Console.WriteLine($"-> {entry.Key}: {entry.Value}");
    }
}

var cityInfo = TestIndexerAndAdd();
PrintDictionary(cityInfo);