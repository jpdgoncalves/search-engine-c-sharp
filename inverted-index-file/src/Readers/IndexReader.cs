using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SearchEngine.Readers
{
    public class IndexReader
    {
        // The second parameter specifies the encoder to throw on invalid characters
        private static UTF8Encoding encoding = new UTF8Encoding(true, true);
        private string path;
        private StreamReader postingsListsFile;
        private Dictionary<string, VocabularyEntry> vocabulary;

        public IndexReader(string filepath)
        {
            this.path = filepath;
            this.postingsListsFile = File.OpenText($"{filepath}\\postings_list.txt");
            this.vocabulary = new Dictionary<string, VocabularyEntry>();

            this.populateVocabulary();
        }

        private void populateVocabulary()
        {
            using (StreamReader reader = File.OpenText($"{this.path}\\vocabulary.txt"))
            {
                string line;
                
                while((line = reader.ReadLine()) != null) {
                    string[] parts = line.Split(':');
                    string term = parts[0];
                    int occurences = int.Parse(parts[1]);
                    int byteOffset = int.Parse(parts[2]);
                    VocabularyEntry entry = new VocabularyEntry(occurences, byteOffset);

                    this.vocabulary.Add(term, entry);
                }
            }
        }

        public VocabularyEntry GetVocabularyEntry(string term) {
            return this.vocabulary[term];
        }

        public string GetPostingsListLine(int byteOffset) {
            this.postingsListsFile.BaseStream.Seek(byteOffset, SeekOrigin.Begin);
            this.postingsListsFile.DiscardBufferedData();

            return this.postingsListsFile.ReadLine();
        }

    }

    public class VocabularyEntry
    {

        private int occurences;
        private int byteOffset;

        public VocabularyEntry(int occurences, int byteOffset)
        {
            this.occurences = occurences;
            this.byteOffset = byteOffset;
        }

        public int Occurences
        {
            get
            {
                return this.occurences;
            }
        }

        public int ByteOffset
        {
            get
            {
                return this.byteOffset;
            }
        }

        public override string ToString()
        {
            return $"VocabularyEntry({this.occurences},{this.byteOffset})";
        }

    }
}