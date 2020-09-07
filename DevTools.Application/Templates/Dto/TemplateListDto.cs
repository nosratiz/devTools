using System;

namespace DevTools.Application.Templates.Dto
{
    public class TemplateListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string GroupName { get; set; }

        public string FullName { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}