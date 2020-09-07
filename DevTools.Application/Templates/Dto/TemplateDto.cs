using System;

namespace DevTools.Application.Templates.Dto
{
    public class TemplateDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public DateTime CreateDate { get; set; }
    }
}