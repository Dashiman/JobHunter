using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using Services.VM;
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
        Task<int> AddBid(BidOffer offer);
        Task<List<JobOffer>> GetAll();
        Task<int> UpdateOffer(JobOffer edited);
        Task<int> EndCourse(EndModel end);
        Task<JobOffer> GetOffer(int offId);
        Task<ProfileData> GetProfileData(int usId);
        Task<int> DeleteOffer(int offId);
        Task<int> TakeJob(BidOffer offer);
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
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return res;
        }
        public async Task<int> AddBid(BidOffer offer)
        {
            int res = 0;
            try
            {
                await _db.BidOffer.AddAsync(offer);
                await _db.SaveChangesAsync();
                return 1;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return res;
        }   
        public async Task<int> TakeJob(BidOffer offer)
        {
            int res = 0;
            try
            {

                var off = await _db.JobOffer.Where(x => x.Id == offer.JobOfferId).FirstOrDefaultAsync();
                if (off!=null){
                    var edited = new JobOffer();
                    edited = off;
                    edited.TakenById = offer.UserId;
                    edited.Status = 2;
                    _db.Entry(off).CurrentValues.SetValues(edited);
                    _db.JobOffer.Update(off);
                    //await _db.TakenOffer.AddAsync(offer);
                    await _db.SaveChangesAsync();
                    return 1; }
                return 0;
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
                res = await _db.JobOffer.Where(x=>x.Status==1).ToListAsync();
                foreach(var a in res)
                {
                    a.AddedBy = await _db.Users.Where(x => x.Id == a.AddedById).FirstOrDefaultAsync();
                    a.Category = await _db.Category.Where(x => x.Id == a.CategoryId).FirstOrDefaultAsync();
                }
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return res;
        }   
        public async Task<ProfileData> GetProfileData(int usId)
        {
           var res = new ProfileData();
            try
            {

                res.AddedByUser =await  _db.JobOffer.Where(a => a.AddedById == usId&&a.Status==1).ToListAsync();
                res.TakenbyUser =await  _db.JobOffer.Where(a => a.TakenById == usId).ToListAsync();
                res.AddandTaken =await  _db.JobOffer.Where(a => a.AddedById == usId&&a.TakenById!=null&&a.Status==2).ToListAsync();
                foreach(var a in res.AddedByUser)
                {
                    a.Category = await _db.Category.Where(x => x.Id == a.CategoryId).FirstOrDefaultAsync();

                }
                foreach (var a in res.TakenbyUser)
                {
                    a.Category = await _db.Category.Where(x => x.Id == a.CategoryId).FirstOrDefaultAsync();

                }
                foreach (var a in res.AddandTaken)
                {
                    a.Category = await _db.Category.Where(x => x.Id == a.CategoryId).FirstOrDefaultAsync();

                }

                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return res;
        }
        public async Task<JobOffer> GetOffer(int offId)
        {
            JobOffer res = new JobOffer();
            try
            {
                res = await _db.JobOffer.Where(x => x.Id == offId).FirstOrDefaultAsync();
                
                    res.AddedBy = await _db.Users.Where(x => x.Id == res.AddedById).FirstOrDefaultAsync();
                    res.Category = await _db.Category.Where(x => x.Id == res.CategoryId).FirstOrDefaultAsync();
                res.BidOffers = await _db.BidOffer.Where(x => x.JobOfferId == res.Id).OrderBy(x=>x.Proposition).ToListAsync();
                foreach(var t in res.BidOffers)
                {
                    t.User = _db.Users.Where(x => x.Id == t.UserId).Select(x => new Users
                    {
                        Id = x.Id,
                        Username = x.Username

                    }).FirstOrDefault();

                }
                    return res;
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return res;
        }
        public async Task<int> DeleteOffer(int offId)
        {
            int res =0;
            try
            {
                 var off=await _db.JobOffer.Where(x => x.Id ==offId).FirstOrDefaultAsync();
                 _db.JobOffer.Remove(off);
                await _db.SaveChangesAsync();

                return 1;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return res;
        }
        public async Task<int> UpdateOffer(JobOffer edited)
        {
            int res = 0;
            try
            {
                var off = await _db.JobOffer.Where(x => x.Id == edited.Id).FirstOrDefaultAsync();
                if (off != null)
                {
                    _db.Entry(off).CurrentValues.SetValues(edited);
                    _db.JobOffer.Update(off);
                    await _db.SaveChangesAsync();
                    return 1;
                }

                return 0;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return res;
        }        
        public async Task<int> EndCourse(EndModel end)
        {
            int res = 0;
            try
            {
                var off = await _db.JobOffer.Where(x => x.Id == end.OfferId).FirstOrDefaultAsync();
                if (off != null)
                {
                    var offer = new JobOffer();
                    offer = off;
                    offer.Status = end.StatusId;
                    _db.Entry(off).CurrentValues.SetValues(offer);
                    _db.JobOffer.Update(off);
                    await _db.SaveChangesAsync();
                    return 1;
                }

                return 0;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return res;
        }
    }
}
