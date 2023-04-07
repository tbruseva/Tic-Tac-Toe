using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.CompilerServices;
using Tic_Tac_Toe_Web_API.Database_Models;
using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Models.Interfaces;
using Tic_Tac_Toe_Web_API.Respository.Interfaces;

namespace Tic_Tac_Toe_Web_API.Respository
{
    public class ResultsRepository : IResultsRepository
    {
        private AppDbContext _dbContext;

        public ResultsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResultDbModel> Get(ResultDbModel result)
        {
            var foundResult = await _dbContext.Results.Where(r => r.PlayerId == result.PlayerId && r.GameName == result.GameName)
                                                      .Include(r=>r.Player).FirstOrDefaultAsync();
            if (foundResult == null) 
            {
                throw new Exception("Result not found!");
            }
            return foundResult;
        }

        public async Task<ResultDbModel> Create (ResultDbModel result)
        {
            var createdResult = await _dbContext.AddAsync(result);
            await _dbContext.SaveChangesAsync();

            return createdResult.Entity;
        }

        public async Task<ResultDbModel> Update (ResultDbModel result)
        {
            var existingResult = await GetByPlayerId(result);
            existingResult.Wins++;
            var updatedResult = _dbContext.Update(existingResult);
            await _dbContext.SaveChangesAsync();

            return updatedResult.Entity;
        }
    }
}
