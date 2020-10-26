using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bigram
{
    public class Program
    {
        // A bigram or digram is a sequence of two adjacent elements from a string of tokens, which are typically letters, syllables, or words.
        // https://en.wikipedia.org/wiki/Bigram
        public static void Main(string[] args)
        {
            string text = TextFromArgs(args);
            string[] tokens = Tokenize(text);

            Dictionary<Tuple<string, string>, int> bigram = new Dictionary<Tuple<string, string>, int>();

            for(int i =1; i< tokens.Length; i++)
            {                
                var key = Tuple.Create(tokens[i - 1].ToLower(), tokens[i].ToLower());
                if(bigram.ContainsKey(key))
                {
                    bigram[key]++;
                } else
                {
                    bigram.Add(key, 1);
                }
            }

            foreach (KeyValuePair<Tuple<string, string>, int> kvp in bigram.OrderByDescending(kvp => kvp.Value))
            {
                Console.WriteLine($"\"{kvp.Key.Item1} {kvp.Key.Item2}\": {kvp.Value}");

            }

        }

        private static string[] NonWordTokens = new string[] { " ", "\t", "\r", "\n", ".", "?", "!", ",", ":", ";", "\""  };

        private static string[] Tokenize(string text)
        {
            // TODO: words that contain hypens (eg part-time), single quotes that preserve contractions, should numbers be ignored? 
            return text.Split(NonWordTokens, StringSplitOptions.RemoveEmptyEntries);
        }

        private static string TextFromArgs(string[] args)
        {            
            if (args.Length > 0)
            {
                if (File.Exists(args[0]))
                {
                    string filepath = args[0];
                    return File.ReadAllText(filepath);
                }
                return args[0];
            } else
            {
                Console.WriteLine("First arguement must be a file or text.");
                return string.Empty;
            }
            
        }
    }
}
