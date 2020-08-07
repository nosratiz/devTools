using System;
using System.Text.RegularExpressions;

namespace DevTools.Domain.Models
{
    public class Template
    {
        public Guid Id { get; set; }
        public Guid GroupTemplateId { get; set; }

        public string Name { get; set; }
        public string Content { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual GroupTemplate GroupTemplate { get; set; }
    }
}