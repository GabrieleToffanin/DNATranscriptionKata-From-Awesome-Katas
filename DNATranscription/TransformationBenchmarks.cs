using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATranscription
{
    [MemoryDiagnoser]
    public class TransformationBenchmarks
    {
        private const string dnaSequence = "AGGACGGGCTAACTCCGCTCGTCACAAAGCGCAATGCAGCTATGGCAGATGTTCATGCCG";
        string? testingString;

        [Benchmark(Baseline = true)]
        public void TestDNAToRNATransformMethodSpeed()
        {
            DNATransformationUtils.ConvertToRNASequence(dnaSequence, out testingString);
        }
    }
}
