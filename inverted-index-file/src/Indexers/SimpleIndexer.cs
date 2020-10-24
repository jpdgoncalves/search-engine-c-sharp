using System.Collections.Generic;

using SearchEngine.Tokenisers;
using SearchEngine.Writers;

namespace SearchEngine.Indexers
{
    public class SimpleIndexer {
        private Dictionary<string, Dictionary<long, int>> index;

        public SimpleIndexer() {
            this.index = new Dictionary<string, Dictionary<long, int>>();
        }

        public void indexToken(Token token) {
            string term = token.Term;
            long docID = token.DocID;

            var index = this.index;
            var postingsList = index.ContainsKey(term) ? index[term] : new Dictionary<long, int>();
            var posting = postingsList.ContainsKey(docID) ? postingsList[docID] : 0;

            postingsList[docID] = posting + 1;
            index[term] = postingsList;
        }

        public void writeIndex(SimpleIndexWriter writer) {
            writer.WriteIndex(this.index);
        }
    }
}