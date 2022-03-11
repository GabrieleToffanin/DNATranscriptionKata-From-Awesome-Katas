//That's the first dna sequence, furthermore because the dna is double-stranded we have the "antisense" sequence too
using Newtonsoft.Json;
using DNATranscription;

string dnaSequence = "TTAGGGCATG".ToUpper();

string? antisenseDNA;

DNATransformationUtils.TransformAndReverseString(dnaSequence, out antisenseDNA);

string? firstRnaSequence;
string? secondRnaSequence;

DNATransformationUtils.ConvertToRNASequence(dnaSequence, out firstRnaSequence);
DNATransformationUtils.ConvertToRNASequence(antisenseDNA, out secondRnaSequence);

Dictionary<string, string> codonsCollection;
Dictionary<string, string> peptidesCollection;

DNATransformationUtils.ConvertJsonToDictionary("codons", out codonsCollection);
DNATransformationUtils.ConvertJsonToDictionary("peptides", out peptidesCollection);



//Remember AUG is the first codon 

List<string> proteinChain;
DNATransformationUtils.FindProteinChain(firstRnaSequence , codonsCollection, peptidesCollection, out proteinChain);

foreach (var item in proteinChain) Console.WriteLine(item);





