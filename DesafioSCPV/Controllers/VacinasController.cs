using _5___Core.Exceptions;
using Application.DTO.Vacina;
using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Presentation.Utilities;
using SCPV.Presentation.WebAPI.ViewModel.VacinaViewModel;
using ViewModels.Response;

namespace CadastroVacinas.API.Controllers
{
    [ApiController]
    [Route("api/vacinas")]
    public class VacinasController : ControllerBase
    {
        private readonly IVacinaService _service;
        private readonly IMapper _mapper;

        public VacinasController(IVacinaService service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateVacina([FromBody] CreateVacinaViewModel vacinaViewModel)
        {
            try
            {
                var vacinaDTO = _mapper.Map<VacinaDTO>(vacinaViewModel);

                var createdVacina = await _service.AddAsync(vacinaDTO);

                var response = new ResultViewModel
                {
                    Message = "Vacina adicionada com sucesso!",
                    Success = true,
                    Data = createdVacina
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
        public async Task<IActionResult> UpdateVacina([FromBody] UpdateVacinaViewModel vacinaViewModel)
        {
            try
            {
                var vacinaDTO = _mapper.Map<VacinaDTO>(vacinaViewModel);

                var updateVacina = await _service.UpdateAsync(vacinaDTO);

                var response = new ResultViewModel
                {
                    Message = "Vacina atualizada com sucesso!",
                    Success = true,
                    Data = updateVacina
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
        public async Task<IActionResult> DeleteVacina(long id)
        {
            try
            {
                await _service.DeleteAsync(id);

                return Ok(new ResultViewModel
                {
                    Message = "Vacina removida com sucesso",
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

        [HttpGet("vacinaId/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var device = await _service.GetByIdAsync(id);

                var response = new ResultViewModel
                {
                    Message = "Vacina encontrada!",
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
        public async Task<IActionResult> GetAllVacinas()
        {
            try
            {
                var devices = await _service.GetAllAsync();

                var response = new ResultViewModel
                {
                    Message = "Vacinas encontradas",
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
                var vacinas = await _service.SearchByName(name);

                if (vacinas == null)
                {
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhuma vacina foi encontrada.",
                        Success = true,
                        Data = vacinas
                    });
                }

                return Ok(new ResultViewModel
                {
                    Message = "Vacina encontrada com sucesso!",
                    Success = true,
                    Data = vacinas
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
