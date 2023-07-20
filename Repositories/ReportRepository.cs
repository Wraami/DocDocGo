﻿using DocDocGo.DAL;
using DocDocGo.Models;
using DocDocGo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DocDocGo.Repositories
{
    public class ReportRepository : IRepository<ReportModel>
    {
        private readonly ApplicationDBContext _dbcontext;

        public ReportRepository(ApplicationDBContext dbContext)
        {
            _dbcontext = dbContext; 
        }

        public async Task<ReportModel> CreateAsync(ReportModel entity)
        {
            await _dbcontext.Reports.AddAsync(entity);

            await _dbcontext.SaveChangesAsync();

           return entity;
        }

        public async Task<IEnumerable<ReportModel>> GetAsync()
        {
            return await _dbcontext.Reports.ToListAsync();
        }

        public async Task<ReportModel> GetByIdAsync(int id)
        {
          var report = await _dbcontext.Reports.FindAsync(id);
            
            if(report == null)
            {

                throw new Exception("report not found");
            }

            return report;
        }

        public async Task<ReportModel> UpdateAsync(ReportModel entity)
        {
            _dbcontext.Entry(entity).CurrentValues.SetValues(entity);

            await _dbcontext.SaveChangesAsync();

            return entity;
        }
    }
}
