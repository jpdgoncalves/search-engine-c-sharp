
namespace SearchEngine.Documents
{
    public class Document
    {

        private string title;
        private string date;
        private string body;
        private long docID;

        public string Title
        {
            get { return title; }
        }

        public string Date
        {
            get { return date; }
        }

        public string Body
        {
            get { return body; }
        }

        public long DocID
        {
            get { return docID; }
        }

        public Document(string title, string date, string body, long docID)
        {
            this.title = title;
            this.date = date;
            this.body = body;
            this.docID = docID;
        }

        public override string ToString()
        {
            return $"{title} {date} {body}";
        }

    }
}