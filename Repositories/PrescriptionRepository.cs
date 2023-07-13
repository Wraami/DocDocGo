using DocDocGo.DAL;
using DocDocGo.Models;
using DocDocGo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DocDocGo.Repositories
{
    public class PrescriptionRepository : IRepository<PrescriptionModel>
    {
        private ApplicationDBContext _dbcontext;
        
        public PrescriptionRepository(ApplicationDBContext dbContext)
        {
                _dbcontext = dbContext;
        }

        public async Task<PrescriptionModel> CreateAsync(PrescriptionModel entity)
        {
            await _dbcontext.Prescriptions.AddAsync(entity);
           
            await _dbcontext.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<PrescriptionModel>> GetAsync()
        {
            return await _dbcontext.Prescriptions.ToListAsync();
        }

        public async Task<PrescriptionModel> GetByIdAsync(int id)
        {
            return await _dbcontext.Prescriptions.FindAsync(id);

        }

        public async Task<PrescriptionModel> UpdateAsync(PrescriptionModel entity)
        {
            _dbcontext.Prescriptions.Update(entity);
            
            await _dbcontext.SaveChangesAsync();
           
            return entity;
        }
    }
}
