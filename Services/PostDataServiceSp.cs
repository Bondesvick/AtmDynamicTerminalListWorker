using System.Collections.Generic;
using System.Linq;
using AtmDynamicTerminalListWorker.Entities.Post;
using AtmDynamicTerminalListWorker.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AtmDynamicTerminalListWorker.Services
{
    internal class PostDataServiceSp : IPostDataService
    {
        private readonly PostDbContext _postDbContext;

        public PostDataServiceSp(PostDbContext postDbContext)
        {
            _postDbContext = postDbContext;
        }

        public IEnumerable<PostAtmItem> GetData()
        {
            return _postDbContext.PostAtmItems
                .FromSqlRaw("realtime.dbo.get_atm_terminal_details")
                .ToList();
        }
    }
}