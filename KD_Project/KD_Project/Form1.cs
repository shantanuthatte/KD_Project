using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Iveonik.Stemmers;

namespace KD_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static void TestStemmer(IStemmer stemmer, params string[] words)
        {
            Console.WriteLine("Stemmer: " + stemmer);
            foreach (string word in words)
            {
                stemmer.Stem(word);
            }
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            char[] stripChars = { ';', ',','.', '-', '_', '^', '(', ')', '[', ']', '\n', '\t', '\r','"'};
           
            string text = System.IO.File.ReadAllText("Computer.txt");
            string text1 = System.IO.File.ReadAllText("stopword.txt");
            text=text.ToLower();

            List<string> wordlist = text.Split(stripChars).ToList();
            List<string> stopwordlist = text1.Split(' ').ToList();
            
         
           IEnumerable<string> differenceQuery =  wordlist.Except(stopwordlist);
            foreach (string df in differenceQuery)
                textBox1.AppendText(df);
            
            /*
                  //wordList= wordList.Except(stopwordlist);
            string stopwordlist1 = string.Format(@"\s?\b(?:{0})\b\s?", stopwordlist);
            tags = Regex.Replace(text, text1, "", RegexOptions.IgnoreCase);
                  List<string> wordList = tags.Split(' ').ToList();
             


            foreach (string s1 in wordList)
            {

                textBox1.AppendText(s1);
            }
            foreach (string s in wordList)
            {
                if (!text1.Contains(s.ToLowerInvariant()))
                    tags += s + ",";
            }

            tags = tags.TrimEnd(',');

*/
           // TestStemmer(new EnglishStemmer(), fwordlist.ToArray<string>());
           // TestStemmer(new EnglishStemmer(), wordList.);      
            
            
       /*
           // List<string> wordList = text.Split(' ').ToList();

            string[] stripChars = { ";", ",", ".", "-", "_", "^", "(", ")", "[", "]","\n", "\t", "\r"};

            foreach (string character in stripChars)
            {   
                text = text.Replace(character, "");
            }

            // Split on spaces into a List of strings
            List<string> wordList = text.Split(' ').ToList();

            // Define and remove stopwords
            string[] stopwords = new string[] { "and", "the", "she", "for", "this", "you", "but" };
            foreach (string word in stopwords)
            {
                // While there's still an instance of a stopword in the wordList, remove it.
                // If we don't use a while loop on this each call to Remove simply removes a single
                // instance of the stopword from our wordList, and we can't call Replace on the
                // entire string (as opposed to the individual words in the string) as it's
                // too indiscriminate (i.e. removing 'and' will turn words like 'bandage' into 'bdage'!)
                while (wordList.Contains(word))
                {
                    wordList.Remove(word);
                }
            }
 
            
           
             

            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            // Loop over all over the words in our wordList...
            foreach (string word in wordList)
            {
                // If the length of the word is at least three letters...
                if (word.Length >= 3)
                {
                    // ...check if the dictionary already has the word.
                    if (dictionary.ContainsKey(word))
                    {
                        // If we already have the word in the dictionary, increment the count of how many times it appears
                        dictionary[word]++;
                    }
                    else
                    {
                        // Otherwise, if it's a new word then add it to the dictionary with an initial count of 1
                        dictionary[word] = 1;
                    }

                } // End of word length check

            } // End of loop over each word in our input

            // Create a dictionary sorted by value (i.e. how many times a word occurs)
            var sortedDict = (from entry in dictionary orderby entry.Value descending select entry).ToDictionary(pair => pair.Key, pair => pair.Value);

            // Loop through the sorted dictionary and output the top 10 most frequently occurring words
            int count = 1;
            Console.WriteLine("---- Most Frequent Terms in the File: " +  " ----");
            Console.WriteLine();
            foreach (KeyValuePair<string, int> pair in sortedDict)
            {
                // Output the most frequently occurring words and the associated word counts
                textBox1.AppendText(count + "\t" + pair.Key + "\t" + pair.Value+"\n");
                Console.WriteLine(count + "\t" + pair.Key + "\t" + pair.Value);
                count++;

                // Only display the top 10 words then break out of the loop!
                /*
                if (count > 10)
                {
                    break;
                }
                 
            }

            // Wait for the user to press a key before exiting
           // Console.ReadKey();
        */
 
            
        }
        public string[] GetKeywords(string text) { 
        string[] stop = {"about","after","all","also","an","and","another","any","are","as","at","be","because","been","before","being","between","both","but","by","came","can","come","could","did","do","does","each","else","for","from","get","got","has","had","he","have","her","here","him","himself","his","how","if","in","into","is","it","its","just","like","make","many","me","might","more","most","much","must","my","never","now","of","on","only","or","other","our","out","over","re","said","same","see","should","since","so","some","still","such","take","than","that","the","their","them","then","there","these","they","this","those","through","to","too","under","up","use","very","want","was","way","we","well","were","what","when","where","which","while","who","will","with","would","you","your","a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z","$","£","1","2","3","4","5","6","7","8","9","0"};
        char[] splitChars = {' ', '\''};

        string[] words = text.Split(splitChars);
        
        return words;
        }
            /*
        var keywordCount = (from keyword in words
        group keyword by keyword into g
        select new { Keyword = g.Key, Count = g.Count() });
        return keywordCount.OrderByDescending(k => k.Count).Select(k =>k.Keyword).Take(5).ToArray();
             */
        
        public static string[] GetSearchWords(string text)
        {
            string pattern = @"\S+";
            Regex re = new Regex(pattern,RegexOptions.IgnoreCase);

            MatchCollection matches = re.Matches(text);
            string[] words = new string[matches.Count];
            for (int i = 0; i < matches.Count; i++)
            {
                words[i] = matches[i].Value;
            }
            return words;
        }

        public static string Keywords(string text) {
            string[] stop = { "about", "after", "all", "also", "an", "and", "another", "any", "are", "as", "at", "be", "because", "been", "before", "being", "between", "both", "but", "by", "came", "can", "come", "could", "did", "do", "does", "each", "else", "for", "from", "get", "got", "has", "had", "he", "have", "her", "here", "him", "himself", "his", "how", "if", "in", "into", "is", "it", "its", "just", "like", "make", "many", "me", "might", "more", "most", "much", "must", "my", "never", "now", "of", "on", "only", "or", "other", "our", "out", "over", "re", "said", "same", "see", "should", "since", "so", "some", "still", "such", "take", "than", "that", "the", "their", "them", "then", "there", "these", "they", "this", "those", "through", "to", "too", "under", "up", "use", "very", "want", "was", "way", "we", "well", "were", "what", "when", "where", "which", "while", "who", "will", "with", "would", "you", "your", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "$", "£", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            var result = text.Split(stop,stop.Count(),StringSplitOptions.None).Aggregate((first, second) => first + second);
            return result;
        }

    }
}
