using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SearchEngine.Writers
{
    public class SimpleIndexWriter
    {
        private string path;

        public SimpleIndexWriter(string path)
        {
            this.path = path;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public string Path
        {
            get
            {
                return this.path;
            }
        }

        public void WriteIndex(Dictionary<string, Dictionary<long, int>> index)
        {
            using (StreamWriter vocabularyWriter = File.CreateText(this.path + "\\vocabulary.txt"))
            using (StreamWriter postingsListWriter = File.CreateText(this.path + "\\postings_list.txt"))
            {
                int byteOffset = 0;
                foreach (var entry in index)
                {
                    string term = entry.Key;
                    var postingsList = entry.Value;
                    int occurences = 0;
                    StringBuilder postingsListLineBuilder = new StringBuilder();
                    string postingsListLine;

                    foreach(var posting in postingsList) {
                        long docID = posting.Key;
                        int count = posting.Value;

                        postingsListLineBuilder.Append($"{docID}:{count};");
                        occurences += count;
                    }

                    postingsListLineBuilder.Append("\n");
                    postingsListLine = postingsListLineBuilder.ToString();
                    
                    postingsListWriter.Write(postingsListLine);
                    vocabularyWriter.WriteLine($"{term}:{occurences}:{byteOffset}");
                    
                    byteOffset += Encoding.UTF8.GetByteCount(postingsListLine);
                }
            }
        }
    }
}