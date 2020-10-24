
namespace Searchengine.Tokenisers
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
}