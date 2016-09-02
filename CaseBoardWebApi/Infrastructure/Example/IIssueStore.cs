﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using CaseBoardWebApi.Models.Example;

namespace CaseBoardWebApi.Infrastructure.Example
{
    public interface IIssueStore
    {
        Task<IEnumerable<Issue>> FindAsync();
        Task<Issue> FindAsync(string issueId);
        Task<IEnumerable<Issue>> FindAsyncQuery(string searchText);
        Task UpdateAsync(Issue issue);
        Task DeleteAsync(string issueId);
        Task CreateAsync(Issue issue);
    }
}
