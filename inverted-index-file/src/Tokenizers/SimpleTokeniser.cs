using System.Collections.Generic;

using SearchEngine.Documents;

namespace SearchEngine.Tokenisers
{
    class SimpleTokeniser : ITokeniser {

        private Document document;
        private Queue<string> documentCorpus = new Queue<string>();
        public void SetDocument(Document document) {
            this.document = document;
            this.documentCorpus = new Queue<string>(document.ToString().Split(' '));
        }   
        public Token GetToken() {
            Token token = null;

            if(this.documentCorpus.Count > 0) {
                string term = this.documentCorpus.Dequeue();
                token = new Token(document.DocID, term);
            }

            return token;
        }
    }
}