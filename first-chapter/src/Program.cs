using System;

namespace Searchengine
{
    class Program
    {

        public static void Main(string[] args)
        {

            if(args.Length != 1) {
                Console.WriteLine("usage: Program.exe <filepath>");
            }

            string filename = args[0];
            Document document;
            DocumentReader documentReader = new DocumentReader(filename);

            while ((document = documentReader.ReadDocument()) != null)
            {
                Console.WriteLine(document.Title);
            }

        }

    }

}