using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Searchengine.DocumentReaders
{
    class Document
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

    class DocumentReader
    {
        private static long docIDCounter = 0;
        private StreamReader streamReader;
        private ReaderState readerState;
        private enum ReaderState
        {
            READY,
            READ_DATE,
            READ_TITLE,
            READ_BODY,
            DONE
        }

        public DocumentReader(string path)
        {
            this.streamReader = File.OpenText(path);
            this.readerState = ReaderState.READY;
        }

        public Document ReadDocument()
        {
            string date = null, title = null, body = null;
            Document document = null;

            if (this.readerState == ReaderState.READY)
            {
                date = this.ReadDate();
            }

            if (this.readerState == ReaderState.READ_DATE)
            {
                title = this.ReadTitle();
            }

            if (this.readerState == ReaderState.READ_TITLE)
            {
                body = this.ReadBody();
            }

            if (this.readerState == ReaderState.READ_BODY)
            {
                this.readerState = ReaderState.READY;
                document = new Document(title, date, body, DocumentReader.docIDCounter);
                DocumentReader.docIDCounter++;
            }

            if (this.readerState == ReaderState.DONE && this.streamReader != null) {
                this.streamReader.Close();
                this.streamReader = null;
            }

            return document;
        }

        private string ReadDate()
        {
            string date = null;
            string dateRegex = @"^[0-9]{4}";
            string line;

            while ((line = this.streamReader.ReadLine()) != null)
            {
                if (Regex.IsMatch(line, dateRegex))
                {
                    date = line.Trim();
                    break;
                }
            }

            if (date != null)
            {
                this.readerState = ReaderState.READ_DATE;
            }
            else
            {
                this.readerState = ReaderState.DONE;
            }

            return date;
        }

        private string ReadTitle()
        {
            string title = null;
            string line;

            while ((line = this.streamReader.ReadLine()) != null)
            {
                if (line.Length != 0 && !line.Equals("\n"))
                {
                    title = line.Trim();
                    break;
                }
            }

            if (title != null)
            {
                this.readerState = ReaderState.READ_TITLE;
            }
            else
            {
                this.readerState = ReaderState.DONE;
            }

            return title;
        }

        private string ReadBody()
        {
            string body = null, line;
            string bodyEnd = "THE END";
            StringBuilder sb = new StringBuilder();

            while ((line = this.streamReader.ReadLine()) != null)
            {
                if (line.Contains(bodyEnd))
                {
                    body = sb.ToString();
                    break;
                }
                if (line.Length != 0 && !line.Equals("\n"))
                {
                    sb.AppendLine(line);
                }
            }

            if (body != null)
            {
                this.readerState = ReaderState.READ_BODY;
            }
            else
            {
                this.readerState = ReaderState.DONE;
            }

            return body;
        }

    }

}