using Searchengine;
using Searchengine.DocumentReaders;

namespace Searchengine.Tokenisers
{
    interface ITokeniser
    {
        void SetDocument(Document document);   
        Token GetToken();
    }
}