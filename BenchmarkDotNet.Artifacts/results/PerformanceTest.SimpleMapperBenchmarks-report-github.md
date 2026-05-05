```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
13th Gen Intel Core i3-1315U 1.20GHz, 1 CPU, 8 logical and 6 physical cores
.NET SDK 10.0.203
  [Host]     : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3
  Dry        : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3


```
| Method                                                 | IterationCount | LaunchCount | RunStrategy | UnrollFactor | WarmupCount | Mean            | Allocated |
|------------------------------------------------------- |--------------- |------------ |------------ |------------- |------------ |----------------:|----------:|
| &#39;Map flat POCO → DTO&#39;                                  | Default        | Default     | Default     | 16           | Default     |        274.4 ns |   1.05 KB |
| &#39;Map nested graph (order + customer + lines)&#39;          | Default        | Default     | Default     | 16           | Default     |      1,734.8 ns |   4.05 KB |
| &#39;Map List&lt;PersonSource&gt; → List&lt;PersonDto&gt; (256 items)&#39; | Default        | Default     | Default     | 16           | Default     |     78,024.9 ns | 228.14 KB |
| &#39;Map into existing destination instance&#39;               | Default        | Default     | Default     | 16           | Default     |        750.0 ns |   1.84 KB |
| &#39;Map flat POCO → DTO&#39;                                  | 1              | 1           | ColdStart   | 1            | 1           | 17,412,900.0 ns |   8.33 KB |
| &#39;Map nested graph (order + customer + lines)&#39;          | 1              | 1           | ColdStart   | 1            | 1           | 23,157,600.0 ns |  26.85 KB |
| &#39;Map List&lt;PersonSource&gt; → List&lt;PersonDto&gt; (256 items)&#39; | 1              | 1           | ColdStart   | 1            | 1           | 18,139,500.0 ns | 228.41 KB |
| &#39;Map into existing destination instance&#39;               | 1              | 1           | ColdStart   | 1            | 1           |  4,357,400.0 ns |  19.92 KB |
