using System;
using System.Collections.Generic;

namespace homework_service.Controllers.DTOs.Requests
{
    public class GroupRequest
    {
        public string Name { get; set; }
        public IEnumerable<Guid> students { get; set; }
        public IEnumerable<Guid> teachers { get; set; }
    }
}
