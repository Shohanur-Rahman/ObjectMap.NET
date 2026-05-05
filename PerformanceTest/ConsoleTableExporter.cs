using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;

namespace PerformanceTest;

/// <summary>
/// Prints a fixed-width table to the console so columns stay aligned (same values as the markdown summary).
/// Custom <see cref="IExporter.ExportToLog"/> is not invoked by BenchmarkDotNet; output is emitted from <see cref="ExportToFiles"/>.
/// </summary>
internal sealed class ConsoleTableExporter : IExporter
{
	public string Name => nameof(ConsoleTableExporter);

	public IEnumerable<string> ExportToFiles(Summary summary, ILogger logger)
	{
		PrintTable(summary, logger);
		return Array.Empty<string>();
	}

	public void ExportToLog(Summary summary, ILogger logger)
	{
		// Host runner does not call ExportToLog for added exporters.
	}

	private static void PrintTable(Summary summary, ILogger logger)
	{
		var table = summary.Table;
		var visible = table.Columns
			.Select((col, index) => (col, index))
			.Where(x => x.col.NeedToShow)
			.ToArray();

		if (visible.Length == 0 || table.FullContent.Length == 0)
			return;

		var headers = visible
			.Select(x => MapHeaderName(x.col.Header.Trim()))
			.ToArray();
		var colWidths = new int[visible.Length];
		for (var c = 0; c < visible.Length; c++)
		{
			var idx = visible[c].index;
			var maxContent = table.FullContent.Max(row => row[idx].Trim().Length);
			colWidths[c] = Math.Max(headers[c].Length, maxContent);
		}

		static string Pad(string s, int w, SummaryTable.SummaryTableColumn.TextJustification justify) =>
			justify == SummaryTable.SummaryTableColumn.TextJustification.Right ? s.PadLeft(w) : s.PadRight(w);

		logger.WriteLine();
		PrintBorder(logger, '┌', '┬', '┐', colWidths);
		var headerLine = string.Join(" │ ", headers.Select((h, i) => Pad(h, colWidths[i], visible[i].col.Justify)));
		logger.WriteLine($"│ {headerLine} │");
		PrintBorder(logger, '├', '┼', '┤', colWidths);

		foreach (var row in table.FullContent)
		{
			var cells = visible.Select((v, i) =>
				Pad(row[v.index].Trim(), colWidths[i], v.col.Justify)).ToArray();
			logger.WriteLine($"│ {string.Join(" │ ", cells)} │");
		}

		PrintBorder(logger, '└', '┴', '┘', colWidths);
		logger.WriteLine();
	}

	private static void PrintBorder(ILogger logger, char left, char mid, char right, int[] colWidths)
	{
		var parts = colWidths.Select(w => new string('─', w + 2));
		logger.WriteLine($"{left}{string.Join(mid.ToString(), parts)}{right}");
	}

	private static string MapHeaderName(string header) =>
		header switch
		{
			"Mean" => "Mean (Time)",
			"Allocated" => "Allocated (Memory)",
			_ => header,
		};
}
