using System;

namespace Searchengine
{
    class Program
    {

        public static void Main(string[] args)
        {

            if(args.Length != 2) {
                Console.WriteLine("usage: Program.exe <inputpath> <indexpath>");
                Environment.Exit(0);
            }

            string filename = args[0];
            string indexpath = args[1];

            Document document;
            Token token;
            Tokeniser tokeniser = new Tokeniser();
            DocumentReader documentReader = new DocumentReader(filename);
            IndexFile indexFile = new IndexFile(indexpath);
            Indexer indexer = new Indexer();


            while ((document = documentReader.ReadDocument()) != null)
            {
                tokeniser.Document = document;
                while((token = tokeniser.GetToken()) != null) {
                    //Console.WriteLine(token);
                    indexer.index(token);
                }   
            }

            indexer.SaveTo(indexFile);

        }

    }

}