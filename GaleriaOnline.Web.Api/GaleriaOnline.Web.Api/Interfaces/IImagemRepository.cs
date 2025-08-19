using GaleriaOnline.Web.Api.Models;

namespace GaleriaOnline.Web.Api.Interfaces
{
    public interface IImagemRepository
    {
        Task<IEnumerable<Imagem>> GetAllAsync();    
        Task<Imagem?> GetByIdAsync(int id);
        Task<Imagem> CreateAsync(Imagem imagem);

        Task<bool> UpdateAsync(Imagem imagem);

        Task<bool> DeleteAsync(int id);
    }
}
