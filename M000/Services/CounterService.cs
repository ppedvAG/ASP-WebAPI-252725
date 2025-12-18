namespace M000.Services;

public class CounterService
{
	public Dictionary<string, int> CounterDict { get; } = [];

	public void AddEntry(string key)
	{
		if (!CounterDict.ContainsKey(key))
			CounterDict[key] = 0;
		CounterDict[key]++;

		Console.WriteLine($"Methode {key} wurde aufgerufen");
	}

	public void PrintCounter(string key)
	{
		Console.WriteLine($"Methode {key} wurde {(CounterDict.ContainsKey(key) ? CounterDict[key] : 0)} mal aufgerufen");
	}
}