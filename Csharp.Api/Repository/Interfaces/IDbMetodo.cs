using Csharp.Api.Model;

namespace Csharp.Api.Repository.Interfaces
{
    public interface IDbMetodo
    {
        public Task<ReturnModel> Create(object model);
        public Task<ReturnModel> Update(object model);
        public Task<ReturnModel> Delete(object model);
        public Task<ReturnModel> SelectAll();
        public Task<ReturnModel> SelectBy(object model);
    }
}
