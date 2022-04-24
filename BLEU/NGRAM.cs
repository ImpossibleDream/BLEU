using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace NGRAM
{
    class NGram
    {
        public String candidate; //candidate sentence
        public String reference; //reference sentence
        public int n;  //value of n in N-Gram

        public List<String> ngrams(String os, int n)
        {
            List<String> ngrams = new List<String>();
            String[] word = Regex.Split(os, @" ");
            for (int i = 0; i < word.Length - n + 1; i++)
            {
                ngrams.Add(after(word, i, i + n));
            }
            return ngrams;
        }
        public static String after(String[] words, int start, int end)
        {
            StringBuilder StringB = new StringBuilder();
            for (int i = start; i < end; i++)
            {
                StringB.Append((i > start ? " " : "") + words[i]);
            }
            return StringB.ToString();
        }

    }
}
