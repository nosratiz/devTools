using System;

namespace DevTools.Application.Templates.Dto
{
    public class GroupTemplateDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public DateTime CreateDate { get; set; }
    }
}