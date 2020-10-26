using System;

using SearchEngine.Documents;
using SearchEngine.Indexers;
using SearchEngine.Readers;
using SearchEngine.Tokenisers;
using SearchEngine.Writers;

namespace SearchEngine {
    
    public class Program {

        public static void Main(string[] args) {
            string filepath = "t8.shakespeare.txt";
            string indexpath = "index";

            Document document;
            Token token;

            DocumentReader reader = new DocumentReader(filepath);
            ITokeniser tokeniser = new TokeniserFilter(new SimpleTokeniser());
            SimpleIndexer indexer = new SimpleIndexer();
            SimpleIndexWriter writer = new SimpleIndexWriter(indexpath);

            while((document = reader.ReadDocument()) != null) {
                tokeniser.SetDocument(document);

                while((token = tokeniser.GetToken()) != null) {
                    indexer.indexToken(token);
                }
            }

            indexer.writeIndex(writer);
            IndexReader indexReader = new IndexReader(indexpath);
            VocabularyEntry entry = indexReader.GetVocabularyEntry("william");
            string line = indexReader.GetPostingsListLine(entry.ByteOffset);

            Console.WriteLine(entry);
            Console.WriteLine(line);
        }
    }
}