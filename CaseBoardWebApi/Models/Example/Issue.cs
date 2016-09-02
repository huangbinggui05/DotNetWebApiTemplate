using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseBoardWebApi.Models.Example
{
    public class Issue
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IssueStatus Status { get; set; }
    }
}