```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
13th Gen Intel Core i3-1315U 1.20GHz, 1 CPU, 8 logical and 6 physical cores
.NET SDK 10.0.203
  [Host] : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3
  Dry    : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3

Job=Dry  IterationCount=1  LaunchCount=1  
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1  
Error=NA  

```
| Method                | Mean     | Allocated |
|---------------------- |---------:|----------:|
| &#39;Map flat POCO → DTO&#39; | 13.13 ms |   8.33 KB |
