using Tic_Tac_Toe_Web_API.Database_Models;

namespace Tic_Tac_Toe_Web_API.Respository.Interfaces
{
    public interface IResultsRepository
    {
        public Task<ResultDbModel> Get(ResultDbModel result);
        public Task<ResultDbModel> Create(ResultDbModel result);
        public Task<ResultDbModel> Update(ResultDbModel result);
    }
}
