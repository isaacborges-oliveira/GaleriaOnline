using GaleriaOnline.Web.Api.DbContextImagem;
using GaleriaOnline.Web.Api.Interfaces;
using GaleriaOnline.Web.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GaleriaOnline.Web.Api.Repositories
{
    public class ImagemRepository : IImagemRepository
    {
        private readonly GaleriaOnlineDBContext _dbContext;

        public ImagemRepository (GaleriaOnlineDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Imagem> CreateAsync(Imagem imagem)
        {
           _dbContext.Imagens.Add(imagem);  
            await _dbContext.SaveChangesAsync();
            return imagem;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var imagem = await _dbContext.Imagens.FindAsync(id);
            if (imagem == null)
            {
                return false;
            }
            _dbContext.Imagens.Remove(imagem);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Imagem>> GetAllAsync()
        {
         return await  _dbContext.Imagens.ToListAsync();
        }

        public async Task<Imagem?> GetByIdAsync(int id)
        {
          return await _dbContext.Imagens.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Imagem imagem)
        {
            _dbContext.Imagens.Update(imagem);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
