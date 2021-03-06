﻿using System.Net.Http;
using CaseBoardWebApi.Controllers.Example;
using CaseBoardWebApi.Models.Example;

namespace CaseBoardWebApi.Infrastructure.Example
{
    public class IssueLinkFactory : LinkFactory<IssueController>
    {
        private const string Prefix = "http://webapibook.net/profile#";
        
        public new class Rels : LinkFactory.Rels {
            public const string IssueProcessor = Prefix + "issue-processor-";
            public const string Open = Prefix + Actions.Open;
            public const string Close = Prefix + Actions.Close;
            public const string Transition = Prefix + Actions.Transition;
            public const string SearchQuery = Prefix + "search";
            public const string Issues = Prefix + "issues";
            public const string Issue = Prefix + "issue";
        }
        
        public class Actions {
            public const string Open="open";
            public const string Close="close";
            public const string Transition="transition";
        }
        
        public IssueLinkFactory(HttpRequestMessage request)
            : base(request)
        {
        }
   
        public Link Transition(string id)
        {
            return GetLink<IssueProcessorController>(Rels.Transition, id, Actions.Transition);
        }
        
        public Link Open(string id)
        {
            return GetLink<IssueProcessorController>(Rels.Open, id, Actions.Open);
        }
        
        public Link Close(string id)
        {
            return GetLink<IssueProcessorController>(Rels.Close, id, Actions.Close);
        }
    }
}