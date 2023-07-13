using DocDocGo.DAL;
using DocDocGo.Models;
using DocDocGo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DocDocGo.Repositories
{
    public class PatientRepository : IRepository<PatientModel>
    {
        private readonly ApplicationDBContext _dbcontext;

        public PatientRepository(ApplicationDBContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<PatientModel> CreateAsync(PatientModel entity)
        {
            await _dbcontext.Patients.AddAsync(entity);
            
            await _dbcontext.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<PatientModel>> GetAsync()
        {
            return await _dbcontext.Patients.ToListAsync();
        }

        public async Task<PatientModel> GetByIdAsync(int id)
        {
            return await _dbcontext.Patients.FindAsync(id);
        }

        public async Task<PatientModel> UpdateAsync(PatientModel entity)
        {
            _dbcontext.Patients.Update(entity);

            await _dbcontext.SaveChangesAsync();

            return entity;
        }
    }
}
