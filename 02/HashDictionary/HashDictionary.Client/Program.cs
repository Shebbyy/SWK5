// See https://aka.ms/new-console-template for more information
// Implicit Usings Project Setting can be used to reduce the amount of imports/usings that are needed to run a prg 

using HashDictionary.Impl;

IDictionary<String, int> TestIndexerAndAdd() {
    var cityInfo = new HashDictionary<String, int>();

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

    return cityInfo;
}

TestIndexerAndAdd();