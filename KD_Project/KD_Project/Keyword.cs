using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD_Project
{
    
    class Section
    {
        public int section = 0;
        public Keywords words = new Keywords();
        Dictionary<Link, int> links = new Dictionary<Link, int>();

        public Section(Keywords w, int s, Dictionary<Link, int> dic)
        {
            words = w;
            section = s;
            links = dic;
        }
    }

    class Link
    {
        public string url = "";
        public string text = "";

        public Link(string u, string t)
        {
            url = u;
            text = t;
        }
    }
    class Word
    {
        public string word = "";
        public List<int> occurances = new List<int>();
        public int count = 0;

        public Word(string w, int o)
        {
            word = w;
            //occurances = new List<int>();
            occurances.Add(o);
            count = 1;
        }
    }

    class Keywords
    {
        public List<Word> words = new List<Word>();
        
        public bool addOcc(string word, int index)
        {
            /*if (wlist.Contains(word))
            {
                words.
            }
            else
            {
                wlist.Add(word);
                words.Add(new Word(word));
            }*/
            int i = words.FindLastIndex(s => s.word == word);
            if (i != -1)
            {
                words[i].occurances.Add(index);
                words[i].count = words[i].occurances.Count;
                return true;
            }
            else
            {
                words.Add(new Word(word, index));
                return false;
            }
        }
    }
}
