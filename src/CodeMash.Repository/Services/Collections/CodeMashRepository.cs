﻿using System;
using CodeMash.Interfaces.Client;
using CodeMash.Interfaces.Database.Repository;
using CodeMash.Models;

namespace CodeMash.Repository
{
    public partial class CodeMashRepository<T> : IRepository<T> where T : IEntity
    {
        public ICodeMashClient Client { get; }
        
        public CodeMashRepository(ICodeMashClient client)
        {
            Client = client;
        }

        public IRepository<T> WithCollection(string collectionName)
        {
            throw new NotImplementedException();
        }
    }
}