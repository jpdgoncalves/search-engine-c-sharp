using System;

namespace Searchengine
{
    class Program
    {

        public static void Main(string[] args)
        {

            if(args.Length != 1) {
                Console.WriteLine("usage: Program.exe <filepath>");
                Environment.Exit(0);
            }

            string filename = args[0];
            Document document;
            Token token;
            Tokeniser tokeniser = new Tokeniser();
            DocumentReader documentReader = new DocumentReader(filename);

            while ((document = documentReader.ReadDocument()) != null)
            {
                tokeniser.Document = document;
                while((token = tokeniser.GetToken()) != null) {
                    Console.WriteLine(token);
                }   
            }

        }

    }

}