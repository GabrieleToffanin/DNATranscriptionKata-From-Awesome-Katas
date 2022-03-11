//That's the first dna sequence, furthermore because the dna is double-stranded we have the "antisense" sequence too
using Newtonsoft.Json;

string dnaSequence = "TTAGGGCATG".ToUpper();

Dictionary<char, char> antisenseConverter = new Dictionary<char, char>()
{
    {'A', 'T' },
    {'T', 'A' },
    {'C', 'G' },
    {'G', 'C' }
};

string? antisenseDNA;

TransformAndReverseString(dnaSequence, antisenseConverter, out antisenseDNA);


Dictionary<char, char> converterToRNA = new Dictionary<char, char>()
{
    { 'A', 'U' },
    { 'T', 'A' },
    { 'C', 'G' },
    { 'G', 'C' },
};

string firstRnaSequence;
string secondRnaSequence;

ConvertToRNASequence(dnaSequence, converterToRNA, out firstRnaSequence);
ConvertToRNASequence(antisenseDNA, converterToRNA, out secondRnaSequence);

Dictionary<string, string> codonsCollection;
Dictionary<string, string> peptidesCollection;

ConvertJsonToDictionary("codons", out codonsCollection);
ConvertJsonToDictionary("peptides", out peptidesCollection);



//Remember AUG is the first codon 

List<string> proteinChain;
FindProteinChain(("ggaugcccaaauaa").ToUpper(), codonsCollection, peptidesCollection, out proteinChain);

foreach (var item in proteinChain) Console.WriteLine(item);



/// <summary>
/// <c>TransformAndReverseString</c> Transforms and reverse a DNA sequence into his antisense part, that's due to his double-strandedProperty 
/// <param name="toReverse">Actual Starting sequence from where getting the reversed and transformed sequence, passed by reference with <code>ref</code> keyword</param>
/// <param name="transformerPattern">The set, preferebly in a Dictionary for fastest indexing access, used for converting nucleotides, passed with<code>in</code> keyword for not being able to modify it during method execution></param>
/// <param name="transformedAndReversed">The actual result we want to retrive during the tranformation, passed with <code>out</code> keyword that let's us making a variable outside the method and getting it "out" from inside the method</param>
/// <example>For example the String "ttatttgggcatcc" would be transformed into "CTACGGGTTTATT"</example>
/// </summary>
static void TransformAndReverseString(in string toReverse, in Dictionary<char, char> transformerPattern, out string? transformedAndReversed)
{
    string temp = "";

    foreach (char item in toReverse)
    {
        temp += transformerPattern[item].ToString();
    }

    transformedAndReversed = "";

    for(int i = (temp.Length - 1); i >= 0; i--)
    {
        transformedAndReversed += temp[i];
    }
}

static void ConvertToRNASequence(in string dnaSequence, in Dictionary<char, char> transfomerPattern, out string rnaSequence)
{
    string temp = "";

    foreach (var item in dnaSequence)
    {
        temp += transfomerPattern[item].ToString();
    }
    rnaSequence = "";
    for (int i = temp.Length - 1; i >= 0; i--)
    {
        
        rnaSequence += temp[i];
    }
}

static void ConvertJsonToDictionary(string filePath, out Dictionary<string, string> codonsCollection)
{
    var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(@$"{filePath}.json"));
    codonsCollection = new Dictionary<string,string>();

    foreach(var item in result)
    {
        codonsCollection.Add(item.Key, item.Value);
    }
}

static void FindProteinChain(in string rnaSequence, in Dictionary<string, string> codonsCollection, in Dictionary<string, string> peptidesCollection, out List<string> proteinChain)
{
    proteinChain = new List<string>();
    int i = 0;
    bool chainFound = false;
    bool chainStarted = false;
    while (!chainFound)
    {
        if (codonsCollection.ContainsKey(rnaSequence[i..(i + 3)]))
        {
            
            if (codonsCollection[rnaSequence[i..(i + 3)]].Equals("Met"))
            {
                proteinChain.Add(peptidesCollection[codonsCollection[rnaSequence[i..(i + 3)]].ToLower()]);
                chainStarted = true;
                
            }
            else if (codonsCollection[rnaSequence[i..(i+3)]].Equals("Stop"))
            {
                chainFound = true;
            }
            else if (chainStarted == true)
            {
                proteinChain.Add(peptidesCollection[codonsCollection[rnaSequence[i..(i + 3)]].ToLower()]);
                
            }
            
        }
        if(chainStarted) i += 3;
        else i++;
        
        
    }
}

