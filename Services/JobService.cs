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
        Task<int> Add(Model.JobOffer job);
        Task<int> AddBid(Model.BidOffer offer);
        Task<List<Model.JobOffer>> GetAll();
        Task<int> UpdateOffer(Model.JobOffer edited);
        Task<int> EndCourse(EndModel end);
        Task<VM.JobOffer> GetOffer(int offId);
        Task<ProfileData> GetProfileData(int usId);
        Task<int> DeleteOffer(int offId);
        Task<int> TakeJob(Model.BidOffer offer);
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

        public async Task<int> Add(Model.JobOffer job)
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
        public async Task<int> AddBid(Model.BidOffer offer)
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
        public async Task<int> TakeJob(Model.BidOffer offer)
        {
            int res = 0;
            try
            {

                var off = await _db.JobOffer.Where(x => x.Id == offer.JobOfferId).FirstOrDefaultAsync();
                if (off!=null){
                    var edited = new Model.JobOffer();
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
        public async Task<List<Model.JobOffer>> GetAll()
        {
            List<Model.JobOffer> res = new List<Model.JobOffer>();
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
                res.User = _db.Users.Where(x => x.Id == usId).FirstOrDefault();
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return res;
        }
        public async Task<VM.JobOffer> GetOffer(int offId)
        {
            VM.JobOffer res = new VM.JobOffer();
            try
            {
                res = await _db.JobOffer.Where(x => x.Id == offId).Select(i=>new VM.JobOffer { 
                Id=i.Id,
                AddedById=i.AddedById,
                CategoryId=i.CategoryId,
                DeclaredCost=i.DeclaredCost,
                Description=i.Description,
                EndedAs=i.EndedAs,
                EndOfferDate=i.EndOfferDate,
                Status=i.Status,
                TakenById=i.TakenById,
                Title=i.Title,
                AddedBy= _db.Users.Where(x => x.Id == res.AddedById).Select(m=>new VM.Users { 
                
                }).FirstOrDefault(),
                Category= _db.Category.Where(x => x.Id == res.CategoryId).FirstOrDefault(),
      
                }).FirstOrDefaultAsync();


                res.BidOffers = _db.BidOffer.Where(x => x.JobOfferId == res.Id).Select(g => new VM.BidOffer
                {
                    Id = g.Id,
                    JobOfferId = g.JobOfferId,
                    OfferDate = g.OfferDate,
                    Proposition = g.Proposition,
                    UserId = g.UserId

                }).OrderBy(x => x.Proposition).ToList();
                foreach (var t in res.BidOffers)
                {
                    t.User = _db.Users.Where(x => x.Id == t.UserId).Select(x => new VM.Users
                    {
                        Id = x.Id,
                        Username = x.Username,
                        EndedBadCount = _db.JobOffer.Where(p => p.TakenById == x.Id && p.Status == 5).Count(),
                        EndedWellCount = _db.JobOffer.Where(p => p.TakenById == x.Id && p.Status == 3).Count()

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
        public async Task<int> UpdateOffer(Model.JobOffer edited)
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
                    var offer = new Model.JobOffer();
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
