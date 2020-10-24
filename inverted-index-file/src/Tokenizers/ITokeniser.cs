using SearchEngine.Documents;

namespace SearchEngine.Tokenisers
{
    public interface ITokeniser
    {
        void SetDocument(Document document);   
        Token GetToken();
    }
}