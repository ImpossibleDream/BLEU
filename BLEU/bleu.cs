using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGRAM;
using System.Text.RegularExpressions;

namespace BLEU
{
    class bleu
    {
        static void Main(string[] args)
        {
            NGram NG = new NGram();

            Console.WriteLine("candidate sentence:");
            NG.candidate = Console.ReadLine();
            Console.WriteLine("reference sentence:");
            NG.reference = Console.ReadLine();
            //NG.n = int.Parse(Console.ReadLine());
            NG.n = 4;

            String[] word_c = Regex.Split(NG.candidate, " ");
            String[] word_r = Regex.Split(NG.reference, " ");

            double bp,BLEU;
            double p= 0;
            double clip = 0; 

            //bp
            if (word_c.Length > word_r.Length)
                bp = 1;
            else
                bp = Math.Exp(1 - (word_c.Length / word_r.Length));

            //exp
            if (NG.n >= 1 && NG.n <= NG.candidate.Length)
            {
                for (int n = 1; n <= NG.n; n++)
                {
                    Console.Write("P{0}=", n);
                    int count_of_c = 0; //candidate sentence's count of n-gram
                    int count_of_r = 0; //reference sentence's count of n-gram

                    int count_in_c = 0;
                    int max_ref_count = 0;

                    foreach (String ngram_c in NG.ngrams(NG.candidate, n))
                    {
                        //Console.WriteLine(ngram_c);
                        count_of_c++;
                        //Console.WriteLine(ngram_c[ngram_c.Length-1]);
                        foreach (String ngram_r in NG.ngrams(NG.reference, n))
                        {
                            for (int i=0;i<ngram_c.Length;i++)
                            {
                                for (int j=0;j<ngram_r.Length;j++)
                                {
                                    if (ngram_c[i]==ngram_r[j])
                                    {
                                        count_in_c++;
                                    }
                                }
                            }
                        }
                        //Console.WriteLine(count_in_c);

                    }
                    //Console.WriteLine(count_of_c);

                    foreach (String ngram_r in NG.ngrams (NG.reference ,n))
                    {
                        //Console.WriteLine(ngram_r);
                        count_of_r++;
                        foreach (String ngram_c in NG.ngrams(NG.candidate, n))
                        {
                            for (int i = 0; i < ngram_r.Length; i++)
                            {
                                for (int j = 0; j < ngram_c.Length; j++)
                                {
                                    if (ngram_r[i] == ngram_c[j])
                                    {
                                        max_ref_count++;
                                    }
                                }
                            }
                        }
                    }
                    //Console.WriteLine(count_of_r);
                    //Console.WriteLine("\n");

                    //Console.WriteLine(count_in_c);

                    //p += (1 /n) * Math.Log(P(count_of_c, count_of_r,max_ref_count));
                    clip = Math.Min(count_in_c, max_ref_count);
                    p += (1 / n) * Math.Log(clip/count_of_c);
                    Console.WriteLine(p);
                }

                //BLEU
                BLEU = bp * Math.Exp(p);
                Console.Write("The result of BLEU is:");
                Console.WriteLine(BLEU);
            }

            Console.ReadLine();
        }

    }
}