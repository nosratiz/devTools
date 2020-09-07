using System;
using System.Collections.Generic;

namespace DevTools.Application.UserApplications.Dto
{
    public class UserApplicationDto
    {
        public Guid Id { get; set; }

        public string SecretCode { get; set; }
        public List<string> RestrictIp { get; set; }
        public int UsageCount { get; set; }

        public DateTime CreateDate { get; set; }
    }
}