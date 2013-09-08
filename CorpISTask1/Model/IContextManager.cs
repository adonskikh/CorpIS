using System;
namespace CorpISTask1.Model
{
    interface IContextManager : IDisposable
    {
        CorpISContext Context { get; }
    }
}
