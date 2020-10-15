using System.Collections.Generic;

namespace Searchengine
{
    class Token
    {
        public string Term { get; set; }
        public long DocID { get; set; }

        public Token(long docID, string term)
        {
            this.Term = term;
            this.DocID = docID;
        }

        public override string ToString()
        {
            return $"Token: ({DocID},{Term})";
        }
    }

    class Tokeniser
    {
        private Queue<string> documentCorpus;
        private Document document;
        public Document Document {
            get {
                return this.document;
            }
            set {
                this.document = value;
                this.documentCorpus = new Queue<string>(value.ToString().Split(' '));
            }
        }

        public Token GetToken() {
            Queue<string> corpus = this.documentCorpus;

            if(corpus.Count > 0) {
                string term = corpus.Dequeue();
                long docID = this.document.DocID;
                return new Token(docID, term);
            } else {
                return null;
            }
        }
    }
}