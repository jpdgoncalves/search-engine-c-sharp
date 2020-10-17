using System;
using System.IO;
using System.Collections.Generic;

namespace Searchengine
{
    class IndexFile
    {
        private string path;
        private string postingsPath;

        public IndexFile(string path)
        {
            this.path = path;
            this.postingsPath = $"{path}\\postings";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(this.postingsPath);
            }
        }

        public Dictionary<string, int> ReadTermDictionary()
        {
            return null;
        }

        public void WriteTermDictionary(Dictionary<string, int> termDictionary)
        {
            using (StreamWriter sw = new StreamWriter($"{this.path}\\index.txt", false))
            {
                foreach (var entry in termDictionary)
                {
                    sw.WriteLine($"{entry.Key}:{entry.Value}");
                }
            }
        }

        public Dictionary<int, List<long>> ReadPostingsDictionary()
        {
            return null;
        }

        public void WritePostingsDictionary(Dictionary<int, List<long>> postingsDictionary)
        {
            foreach (var entry in postingsDictionary) {
                this.WritePostingList(entry.Key, entry.Value);
            }
        }

        public List<long> ReadPostingList(int id)
        {
            return null;
        }

        public void WritePostingList(int id, List<long> postingList)
        {
            using (StreamWriter sw = new StreamWriter($"{this.postingsPath}\\{id}.txt"))
            {
                sw.WriteLine($"{id}:{String.Join(";", postingList)}");
            }
        }
    }


    class Indexer
    {
        private int termCounter;
        private Dictionary<string, int> termDictionary;
        private Dictionary<int, List<long>> postingsDictionary;

        public Indexer()
        {
            this.termCounter = 0;
            this.termDictionary = new Dictionary<string, int>();
            this.postingsDictionary = new Dictionary<int, List<long>>();
        }

        public void index(Token token)
        {
            var termDictionary = this.termDictionary;
            var postingsDictionary = this.postingsDictionary;

            int postingsID;

            string term = token.Term;
            long docID = token.DocID;

            if (!termDictionary.ContainsKey(term))
            {
                termDictionary.Add(term, this.termCounter);
                this.termCounter++;
            }

            postingsID = termDictionary[term];

            if (!postingsDictionary.ContainsKey(postingsID))
            {
                postingsDictionary.Add(postingsID, new List<long>());
            }

            postingsDictionary[postingsID].Add(docID);
        }

        public void SaveTo(IndexFile indexFile)
        {
            indexFile.WriteTermDictionary(this.termDictionary);
            indexFile.WritePostingsDictionary(this.postingsDictionary);
        }
    }
}