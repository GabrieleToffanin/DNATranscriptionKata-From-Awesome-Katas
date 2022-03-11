using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATranscription
{
    public static partial class DNATransformationUtils
    {

        private static Dictionary<char, char> antisenseConverter = new Dictionary<char, char>()
        {
            {'A', 'T' },
            {'T', 'A' },
            {'C', 'G' },
            {'G', 'C' }
        };
        private static Dictionary<char, char> converterToRNA = new Dictionary<char, char>()
        {
            { 'A', 'U' },
            { 'T', 'A' },
            { 'C', 'G' },
            { 'G', 'C' },
        };
        /// <summary>
        /// <c>TransformAndReverseString</c> Transforms and reverse a DNA sequence into his antisense part, that's due to his double-strandedProperty 
        /// </summary>
        /// <param name="toReverse">Actual Starting sequence from where getting the reversed and transformed sequence, passed by reference with <code>ref</code> keyword</param>
        /// <param name="transformedAndReversed">The actual result we want to retrive during the tranformation, passed with <code>out</code> keyword that let's us making a variable outside the method and getting it "out" from inside the method</param>
        /// <example>For example the String "ttatttgggcatcc" would be transformed into "CTACGGGTTTATT"</example>

        public static void TransformAndReverseString(in string toReverse, out string? transformedAndReversed)
        {
            string temp = "";

            foreach (char item in toReverse)
            {
                temp += antisenseConverter[item].ToString();
            }

            transformedAndReversed = "";

            for (int i = (temp.Length - 1); i >= 0; i--)
            {
                transformedAndReversed += temp[i];
            }
        }

        /// <summary>
        /// Can get both of the double-stranded dna sequence, this method transforms it in an RNA sequence with
        /// </summary>
        /// <param name="dnaSequence"></param>
        /// <param name="rnaSequence"></param>
        public static void ConvertToRNASequence(in string? dnaSequence,  out string? rnaSequence)
        {
            string temp = "";
            if (dnaSequence is not null)
            {
                foreach (var item in dnaSequence)
                {
                    temp += converterToRNA[item].ToString();
                }
            }
            rnaSequence = "";
            for (int i = temp.Length - 1; i >= 0; i--)
            {

                rnaSequence += temp[i];
            }
        }

        /// <summary>
        /// Simple method for transfering Json-Data into a <string,string> Dictionary
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="codonsCollection"></param>
        public static void ConvertJsonToDictionary(string filePath, out Dictionary<string, string> codonsCollection)
        {
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(@$"{filePath}.json"));

            codonsCollection = new Dictionary<string, string>();
            if(result is not null)
            {
                foreach (var item in result)
                {
                    codonsCollection.Add(item.Key, item.Value);
                }
            }
        }


        /// <summary>
        /// Find the protein chain that can be obtained by a RNA filament (the possible Ones)
        /// </summary>
        /// <param name="rnaSequence"></param>
        /// <param name="codonsCollection"></param>
        /// <param name="peptidesCollection"></param>
        /// <param name="proteinChain"></param>
        public static void FindProteinChain(in string? rnaSequence, 
                                            in Dictionary<string, string> codonsCollection, 
                                            in Dictionary<string, string> peptidesCollection, 
                                            out List<string> proteinChain)
        {
            proteinChain = new List<string>();
            int i = 0;
            bool chainFound = false;
            bool chainStarted = false;

            while (!chainFound && rnaSequence?.Length >= 3)
            {
                if (codonsCollection.ContainsKey(rnaSequence[i..(i + 3)]))
                {

                    if (codonsCollection[rnaSequence[i..(i + 3)]].Equals("Met"))
                    {
                        proteinChain.Add(peptidesCollection[codonsCollection[rnaSequence[i..(i + 3)]].ToLower()]);
                        chainStarted = true;

                    }
                    else if (codonsCollection[rnaSequence[i..(i + 3)]].Equals("Stop"))
                    {
                        chainFound = true;
                    }
                    else if (chainStarted == true)
                    {
                        proteinChain.Add(peptidesCollection[codonsCollection[rnaSequence[i..(i + 3)]].ToLower()]);

                    }

                }
                if (chainStarted) i += 3;
                else i++;
            }
           
        }
    }
}
