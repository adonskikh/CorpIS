using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorpISTask1.Model
{
    public class ContextManager : CorpISTask1.Model.IContextManager
    {
        private CorpISContext _context;

        public CorpISContext Context
        {
            get { return _context ?? (_context = new CorpISContext()); }
        }

        public void Dispose()
        {
            if(_context != null)
                _context.Dispose();
        }
    }
}
