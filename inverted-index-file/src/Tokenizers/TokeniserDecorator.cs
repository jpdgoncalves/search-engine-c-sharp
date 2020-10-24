using SearchEngine.Documents;

namespace SearchEngine.Tokenisers {

    public abstract class TokeniserDecorator : ITokeniser {

        private ITokeniser tokeniser;
        public TokeniserDecorator(ITokeniser tokeniser) {
            this.tokeniser = tokeniser;
        }

        public virtual void SetDocument(Document document) {
            this.tokeniser.SetDocument(document);
        }

        public virtual Token GetToken() {
            return this.tokeniser.GetToken();
        }

    }

}