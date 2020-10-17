using System.Collections.Generic;
using System.Text.RegularExpressions;

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
        private static Regex characterFilter = new Regex(@"[-:;,.?!()\s]+");
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

            while (corpus.Count > 0) {
                string term = corpus.Dequeue();
                long docID = this.document.DocID;

                //Replace all whitespaces, commas etc with nothing
                term = Tokeniser.characterFilter.Replace(term, "");
                //Lowercase terms
                term = term.ToLower();
                //Filter out terms with less than or equal to 3 characters
                if(term.Length <= 3) {
                    continue;
                }

                return new Token(docID, term);
            }

            return null;
        }
    }
}