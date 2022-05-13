using System.Collections.Generic;
using System.Linq;
using AtmDynamicTerminalListWorker.Entities.Post;
using AtmDynamicTerminalListWorker.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AtmDynamicTerminalListWorker.Services
{
    public class PostDataServiceQuery : IPostDataService
    {
        private readonly PostDbContext _postDbContext;
        private readonly IConfiguration _configuration;

        public PostDataServiceQuery(PostDbContext postDbContext, IConfiguration configuration)
        {
            _postDbContext = postDbContext;
            _configuration = configuration;
        }

        public IEnumerable<PostAtmItem> GetData()
        {
            var query = _configuration["AppSettings:PostAtmItemQuery"];
            return _postDbContext.PostAtmItems
                .FromSqlRaw(query).AsNoTracking()
                .ToList();

            //return _postDbContext.PostAtmItems
            //    .FromSqlRaw(@"
            //       SELECT
            //          A.atm_id AS TerminalID,
            //          A.external_address AS IPAddress,
            //          miscellaneous AS OnlineStatus,
            //          '000' + SUBSTRING(A.atm_id, 5, 3) AS BranchCode, Brand AS TerminalBrand
            //        FROM
            //          [realtime].[dbo].[atm_config] AS A INNER JOIN [realtime].[dbo].[TERM] AS B ON B.id = A.atm_id, [realtime].[dbo].[ATM_BRAND] AS C where c.Term_id = B.id AND B.term_active = '1'
            //    ")
            //    .ToList();
        }

        //public IEnumerable<PostAtmItem> GetData()
        //{
        //    return _postDbContext.PostAtmItems
        //        .FromSqlRaw(@"
        //            SELECT
        //              A.atm_id AS TerminalID,
        //              A.external_address AS IPAddress,
        //              miscellaneous AS OnlineStatus,
        //              '000' + SUBSTRING(A.atm_id, 5, 3) AS BranchCode,
        //              case when A.loadset_group = 'FEP NCR EMV' THEN 'NCR' when A.loadset_group = 'FEP NCR EMV +DUAL' THEN 'NCR' WHEN A.loadset_group = 'FEP Wincor EMV' THEN 'HYOSUNG' end TerminalBrand
        //            FROM
        //              [realtime].[dbo].[atm_config] AS A
        //              INNER JOIN [realtime].[dbo].[TERM] AS B ON B.id = A.atm_id
        //              AND B.term_active = '1'
        //            WHERE
        //              (
        //                A.external_address LIKE '10.234.%'
        //              )
        //              AND A.loadset_group IN (
        //                'FEP NCR EMV', 'FEP Wincor EMV',
        //                'FEP NCR EMV +DUAL'
        //              )
        //        ")
        //        .ToList();
        //}
    }
}