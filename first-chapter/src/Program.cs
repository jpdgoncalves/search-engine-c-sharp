using System;

namespace Searchengine
{
    class Program
    {

        public static void Main(string[] args)
        {

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