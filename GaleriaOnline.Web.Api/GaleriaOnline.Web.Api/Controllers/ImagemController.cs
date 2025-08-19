using GaleriaOnline.Web.Api.DTO;
using GaleriaOnline.Web.Api.Interfaces;
using GaleriaOnline.Web.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GaleriaOnline.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagemController : ControllerBase
    {
        private readonly IImagemRepository _repository;
        private readonly IWebHostEnvironment _env;

        public ImagemController(IImagemRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImagemPorId(int id)

        {
            var imagem = await _repository.GetByIdAsync(id);
            if (imagem == null)
            {
                return NotFound();

            }
            return Ok(imagem);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodasAsImagens()
        {
            var imagens = await _repository.GetAllAsync();
            return Ok(imagens);
        }



        [HttpPost("upload")]
        public async Task<IActionResult> UploadImagem([FromForm] ImagemDTO dto)
        {
            if (dto.Arquivo == null || dto.Arquivo.Length == 0 || String.IsNullOrWhiteSpace(dto.Nome))
            {
                return BadRequest("Deve ser enviado um nome e uma imagem");
            }

            var extensao = Path.GetExtension(dto.Arquivo.FileName);
            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

            var pastaRelativa = "wwwroot/imagens";
            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

            if (!Directory.Exists(caminhoPasta))
            {

                Directory.CreateDirectory(caminhoPasta);
            }
            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await dto.Arquivo.CopyToAsync(stream);
            }
            var imagem = new Imagem
            {
                Nome = dto.Nome,
                Caminho = Path.Combine(pastaRelativa, nomeArquivo).
                Replace("\\", "/")
            };
            await _repository.CreateAsync(imagem);
            return CreatedAtAction(nameof(GetImagemPorId), new { id = imagem.Id }, imagem);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarImagem(int id, PutImagemDto ImagemAtualizada)
        {
            var imagem = await _repository.GetByIdAsync(id);
            if (imagem == null)
            {
                return NotFound("imagem nao encontrada");
            }
            if (ImagemAtualizada.Arquivo == null && string.IsNullOrWhiteSpace(ImagemAtualizada.Nome))
            {
                return BadRequest("Pelo menos uim dos cantos tem que ser ");

            }
            if (!string.IsNullOrWhiteSpace(ImagemAtualizada.Nome))
            {
                imagem.Nome = ImagemAtualizada.Nome;
            }
            var caminhoAntigo = Path.Combine
                (Directory.GetCurrentDirectory(),
                imagem.Caminho.Replace("/", Path.DirectorySeparatorChar.ToString()));
            if (ImagemAtualizada.Arquivo != null && ImagemAtualizada.Arquivo.Length > 0)
            {
                if (System.IO.File.Exists(caminhoAntigo))
                {
                    System.IO.File.Delete(caminhoAntigo);
                }
                var extensao = Path.GetExtension(ImagemAtualizada.Arquivo.FileName);
                var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

                var pastaRelativa = "wwwroot/imagens";
                var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

                if (!Directory.Exists(caminhoPasta))
                {

                    Directory.CreateDirectory(caminhoPasta);
                }
                var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

                using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    await ImagemAtualizada.Arquivo.CopyToAsync(stream);
                }

                imagem.Caminho = Path.Combine(pastaRelativa, nomeArquivo).Replace("\\", "/");

            }
            var atualizado = await _repository.UpdateAsync(imagem);
            if (!atualizado)

            {
                return StatusCode(500, "Erro ao arulizar a imagem");

            }
            return Ok(imagem);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarImagem(int id)
        {
            var imagem = await _repository.GetAllAsync(id);
            if(imagem == null)
            {
                return NotFound("imagem nao encontrada");

            }
            var caminhoFisico = Path.Combine(Directory.GetCurrentDirectory(),
                imagem.Caminho.Replace("/",
                Path.DirectorySeparatorChar.ToString()

                ));
            if(System.IO.File.Exists(caminhoFisico))
            {
                try
                {
                    System.IO.File.Delete(caminhoFisico);
                    
                }
                catch(Exception ex)
                {
                    return StatusCode(500, $"Erro Ao excluir o arquivo : {ex.Message}");
                }
            }
            var deletado = await _repository.DeleteAsync(id);
            if(!deletado)
            {
                return StatusCode(500, "Erro ao excluir imagem do banco");

            }
            return NoContent();
         }
    }
}

