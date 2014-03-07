public class PerformanceMeter
{
	public static MeasuringResult MeasureMethodDuration(Action action, int numberOfRuns)
	{
		var result = new MeasuringResult();

		for (int i = 0; i < numberOfRuns; i++)
		{
			var timer = Stopwatch.StartNew();
			action();
			result.AddResult(timer.ElapsedMilliseconds);
		}

		return result;
	}
}

public class MeasuringResult
{
	private readonly IList<long> runTimes;
	public IEnumerable<long> RunTimes
	{
		get { return runTimes; }
	}

	public int ResultsCount
	{
		get { return RunTimes.Count(); }
	}

	public double AverageTime
	{
		get { return RunTimes.Average(); }
	}

	public MeasuringResult()
	{
		runTimes = new List<long>();
	}

	public void AddResult(long miliseconds)
	{
		runTimes.Add(miliseconds);
	}

	public string GetResultString()
	{
		var sb = new StringBuilder();
		sb.Append(string.Join(", ", RunTimes));
		sb.AppendLine();
		sb.AppendFormat("> Average time: {0:0.##} s", AverageTime / 1000);

		return sb.ToString();
	}
}