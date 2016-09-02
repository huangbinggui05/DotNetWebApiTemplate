using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseBoardWebApi.Infrastructure.Example
{
    public interface IStateFactory<TModel, TState>
    {
        TState Create(TModel model);
    }
}