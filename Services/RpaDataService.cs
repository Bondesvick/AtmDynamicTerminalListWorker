using System;
using System.Linq;
using System.Threading.Tasks;
using AtmDynamicTerminalListWorker.Entities.Rpa;
using AtmDynamicTerminalListWorker.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AtmDynamicTerminalListWorker.Services
{
    public class RpaDataService : IRpaDataService
    {
        private readonly IFinacleDataService _finacleDataService;
        private readonly IPostDataService _postDataService;
        private readonly RpaDbContext _rpaDbContext;
        private readonly ILogger<RpaDataService> _logger;

        public RpaDataService(IFinacleDataService finacleDataService, IPostDataService postDataService,
            RpaDbContext rpaDbContext, ILogger<RpaDataService> logger)
        {
            _logger = logger;
            _finacleDataService = finacleDataService;
            _postDataService = postDataService;
            _rpaDbContext = rpaDbContext;
        }

        // public async Task UpdateAtmLists()
        // {
        //     var finacleList = _finacleDataService.GetData();
        //     var postList = _postDataService.GetData();
        //
        //      finacleList = finacleList.GroupBy(x => x.TerminalID).Select(x => x.First()).ToList();
        //      postList = postList.GroupBy(x => x.TerminalID).Select(x => x.First()).ToList();
        //
        //
        //     var atmTerminals = finacleList.Select(finacleAtmItem => new AtmTerminal {AtmGl = finacleAtmItem.AtmGL, TerminalId = finacleAtmItem.TerminalID}).ToList();
        //
        //     foreach (var postAtmItem in postList)
        //     {
        //         var atmTerminal =
        //             atmTerminals.SingleOrDefault(terminal => terminal.TerminalId == postAtmItem.TerminalID);
        //
        //         if (atmTerminal == null)
        //         {
        //             var newAtmTerminal = new AtmTerminal
        //             {
        //                 TerminalId = postAtmItem.TerminalID,
        //                 BranchCode = postAtmItem.BranchCode,
        //                 IpAddress = postAtmItem.IPAddress,
        //                 OnlineStatus = postAtmItem.OnlineStatus,
        //                 TerminalBrand = postAtmItem.TerminalBrand
        //             };
        //
        //             atmTerminals.Add(newAtmTerminal);
        //         }
        //         else
        //         {
        //             atmTerminal.BranchCode = postAtmItem.BranchCode;
        //             atmTerminal.IpAddress = postAtmItem.IPAddress;
        //             atmTerminal.OnlineStatus = postAtmItem.OnlineStatus;
        //             atmTerminal.TerminalBrand = postAtmItem.TerminalBrand;
        //
        //             atmTerminals.Add(atmTerminal);
        //         }
        //
        //     }
        //
        //
        //     _rpaDbContext.AtmTerminals.RemoveRange(_rpaDbContext.AtmTerminals.ToList());
        //
        //     _rpaDbContext.AtmTerminals.AddRange(atmTerminals);
        //     _rpaDbContext.SaveChanges();
        //
        // }

        public async Task UpdateAtmLists()
        {
            try
            {
                _logger.LogInformation("Get finacle data");
                var finacleList = _finacleDataService.GetAtmItems();
                var finacleBranchDetails = _finacleDataService.GetBranchDetails();

                _logger.LogInformation("Get postillion data");
                var postList = _postDataService.GetData();

                _logger.LogInformation("Create new list for post data");
                var atmDynamicTerminals = postList.Select(postAtmItem => new AtmDynamicTerminal
                {
                    BranchCode = postAtmItem.BranchCode,
                    IpAddress = postAtmItem.IPAddress,
                    OnlineStatus = postAtmItem.OnlineStatus,
                    TerminalBrand = postAtmItem.TerminalBrand,
                    TerminalId = postAtmItem.TerminalID
                });

                _logger.LogInformation("Create new list for finacle data");
                var atmGls = finacleList.Select(finacleAtmItem => new AtmGL
                {
                    GL = finacleAtmItem.AtmGL,
                    TerminalId = finacleAtmItem.TerminalID
                });

                var branchDetails = finacleBranchDetails.Select(x => new BranchDetail
                {
                    BranchAddress = x.BranchAddress,
                    BranchEmail = x.BranchEmail,
                    BranchName = x.BranchName,
                    BranchSol = x.BranchSol,
                    State = x.State
                });

                _logger.LogInformation("Remove previous post data from context");
                //_rpaDbContext.AtmDynamicTerminals.RemoveRange(_rpaDbContext.AtmDynamicTerminals.AsNoTracking().ToList());
                //_rpaDbContext.AtmDynamicTerminals.FromSqlRaw("TRUNCATE 'RPA_ATM_Dynamic_List'");
                await _rpaDbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE RPA_ATM_Dynamic_List");
                _logger.LogInformation("Done");

                _logger.LogInformation("Remove previous finacle data from context");
                //_rpaDbContext.AtmGls.RemoveRange(_rpaDbContext.AtmGls.AsNoTracking().ToList());
                //_rpaDbContext.AtmGls.FromSqlRaw("TRUNCATE 'RPA_ATM_GL'");
                await _rpaDbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE RPA_ATM_GL");
                await _rpaDbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE RPA_BranchDetail");

                await _rpaDbContext.SaveChangesAsync();
                _rpaDbContext.ChangeTracker.Clear();
                _logger.LogInformation("Done");

                _logger.LogInformation("Add new postillion data to context");
                await _rpaDbContext.AtmDynamicTerminals.AddRangeAsync(atmDynamicTerminals);
                _logger.LogInformation("Done");

                _logger.LogInformation("Add new finacle data to context");
                await _rpaDbContext.AtmGls.AddRangeAsync(atmGls);
                await _rpaDbContext.BranchDetails.AddRangeAsync(branchDetails);
                _logger.LogInformation("Done");

                _logger.LogInformation("Saving changes");
                await _rpaDbContext.SaveChangesAsync();

                _rpaDbContext.ChangeTracker.Clear();

                _logger.LogInformation("Job done");
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}", e.ToString());
            }
        }
    }
}