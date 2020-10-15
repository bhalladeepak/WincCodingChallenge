using System;
using System.Collections.Generic;
using System.Text;
using Winc.Shared.EntityFrameworkCore.Repository;

namespace Winc.Library.Repository
{
    public interface IWincRepository : IGenericRepository
    {
        WincDbContext WincDbContext { get; }
    }
    public class WincRepository : GenericRepository, IWincRepository
    {
        public WincDbContext WincDbContext { get; }
        public WincRepository(WincDbContext wincDbContext) : base(wincDbContext)
        {
            WincDbContext = wincDbContext;
        }
    }
}
