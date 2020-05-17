using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IJobService
    {
        Task<int> Add(JobOffer job);
        Task<List<JobOffer>> GetAll();

    }

    public class JobService:IJobService
    {
        private readonly JobHunterContext _db;
        private readonly ILogger _logger;
        public JobService(JobHunterContext db,ILogger<JobService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<int> Add(JobOffer job)
        {
            int res = 0;
            try
            {
                _db.JobOffer.Add(job);
                await _db.JobOffer.AddAsync(job);
                await _db.SaveChangesAsync();
                return 1;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return res;
        }
        public async Task<List<JobOffer>> GetAll()
        {
            List<JobOffer> res = new List<JobOffer>();
            try
            {
                res = await _db.JobOffer.ToListAsync();
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return res;
        }
    }
}
