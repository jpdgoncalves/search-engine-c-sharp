using System.Text.RegularExpressions;

namespace SearchEngine.Tokenisers
{

    class TokeniserFilter : TokeniserDecorator
    {
        private static Regex characterFilter = new Regex(@"[-:;,.?!()\s]+");
        
        public TokeniserFilter(ITokeniser tokeniser) : base(tokeniser) { }

        public override Token GetToken()
        {
            Token token = null;

            while((token = base.GetToken()) != null) {

                string term = token.Term;

                term = TokeniserFilter.characterFilter.Replace(term, "");
                term = term.ToLower();

                if(term.Length <= 3) {
                    continue;
                }
                
                token.Term = term;
                return token;
            }

            return null;
        }

    }

}