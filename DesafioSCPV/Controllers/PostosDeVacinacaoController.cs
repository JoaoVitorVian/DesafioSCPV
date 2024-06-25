using _5___Core.Exceptions;
using Application.DTO.PostoDeVacinacao;
using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Presentation.Utilities;
using SCPV.Presentation.WebAPI.ViewModel.PostoViewModel;
using ViewModels.Response;

namespace CadastroPostosVacinacao.API.Controllers
{
    [ApiController]
    [Route("api/postos-vacinacao")]
    public class PostosDeVacinacaoController : ControllerBase
    {
        private readonly IPostoDeVacinacaoService _service;
        private readonly IMapper _mapper;

        public PostosDeVacinacaoController(IPostoDeVacinacaoService service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePosto([FromBody] CreatePostoDeVacinacaoViewModel postoViewModel) 
        {
            try
            {
                var postoDTO = _mapper.Map<PostoDeVacinacaoDTO>(postoViewModel);

                var createdPosto = await _service.AddAsync(postoDTO);

                var response = new ResultViewModel
                {
                    Message = "Posto criado com sucesso!",
                    Success = true,
                    Data = createdPosto
                };

                return Ok(response);
            }
            catch (DomainExceptions ex)
            {
                return BadRequest(new ResultViewModel
                {
                    Message = ex.Message,
                    Success = false,
                    Data = ex.Errors
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel
                {
                    Message = ex.Message,
                    Success = false
                });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdatePosto([FromBody] UpdatePostoDeVacinacaoViewModel postoViewModel)
        {
            try
            {
                var postoDTO = _mapper.Map<PostoDeVacinacaoDTO>(postoViewModel);

                var updatePosto = await _service.UpdateAsync(postoDTO);

                var response = new ResultViewModel
                {
                    Message = "Posto atualizado com sucesso!",
                    Success = true,
                    Data = updatePosto
                };

                return Ok(response);
            }
            catch (DomainExceptions ex)
            {
                return BadRequest(new ResultViewModel
                {
                    Message = ex.Message,
                    Success = false,
                    Data = ex.Errors
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel
                {
                    Message = ex.Message,
                    Success = false
                });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePosto(long id)
        {
            try
            {
                await _service.DeleteAsync(id);

                return Ok(new ResultViewModel
                {
                    Message = "Posto removido com sucesso",
                    Success = true
                });
            }
            catch (DomainExceptions ex)
            {
                return BadRequest(new ResultViewModel
                {
                    Message = ex.Message,
                    Success = false,
                    Data = ex.Errors
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel
                {
                    Message = ex.Message,
                    Success = false
                });
            }
        }

        [HttpGet("postoId/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var device = await _service.GetByIdAsync(id);

                var response = new ResultViewModel
                {
                    Message = "Posto encontrado!",
                    Success = true,
                    Data = device
                };

                return Ok(response);
            }
            catch (DomainExceptions ex)
            {
                return BadRequest(new ResultViewModel
                {
                    Message = ex.Message,
                    Success = false,
                    Data = ex.Errors
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel
                {
                    Message = ex.Message,
                    Success = false
                });
            }
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var devices = await _service.GetAllAsync();

                var response = new ResultViewModel
                {
                    Message = "Postos encontrados",
                    Success = true,
                    Data = devices
                };

                return Ok(response);
            }
            catch (DomainExceptions ex)
            {
                return BadRequest(new ResultViewModel
                {
                    Message = ex.Message,
                    Success = false,
                    Data = ex.Errors
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel
                {
                    Message = ex.Message,
                    Success = false
                });
            }
        }

        [HttpGet("search-by-name")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            try
            {
                var postos = await _service.SearchByName(name);

                if (postos == null)
                {
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum posto foi encontrada.",
                        Success = true,
                        Data = postos
                    });
                }

                return Ok(new ResultViewModel
                {
                    Message = "Posto encontrado com sucesso!",
                    Success = true,
                    Data = postos
                });
            }
            catch (DomainExceptions ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }
    }
}
