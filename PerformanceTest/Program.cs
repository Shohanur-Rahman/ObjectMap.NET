using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace PerformanceTest;

internal static class Program
{
	private static void Main(string[] args) =>
		BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(
			args,
			ManualConfig.Create(DefaultConfig.Instance)
				.WithOptions(ConfigOptions.DisableOptimizationsValidator)
				.AddExporter(new ConsoleTableExporter()));
}
