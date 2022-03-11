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

        [Benchmark(Baseline = true)]
        public void TestDNAToRNATransformMethodSpeed()
        {
            string? testingString;
            DNATransformationUtils.ConvertToRNASequence(dnaSequence, out testingString);
        }
    }
}
