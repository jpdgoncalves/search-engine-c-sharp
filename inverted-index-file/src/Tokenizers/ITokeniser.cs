using SearchEngine.Documents;

namespace SearchEngine.Tokenisers
{
    interface ITokeniser
    {
        void SetDocument(Document document);   
        Token GetToken();
    }
}