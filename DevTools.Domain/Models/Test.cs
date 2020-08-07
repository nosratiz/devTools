using System;
using DevTools.Common.Enum;

namespace DevTools.Domain.Models
{
    public class Test
    {
        public Guid Id { get; set; }
        public Guid GroupTestId { get; set; }

        public string Name { get; set; }
        public string Url { get; set; }
        public string Body { get; set; }
        public string Header { get; set; }
        public string MessageExpect { get; set; }

        public HttpMethod HttpMethod { get; set; }
        public ContentType ContentType { get; set; }

        public int StatusCodeExpect { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual GroupTest GroupTest { get; set; }
    }
}