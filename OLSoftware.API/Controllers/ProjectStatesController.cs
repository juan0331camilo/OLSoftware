using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OLSoftware.BL.DTOs;
using OLSoftware.BL.Models;
using System.Linq;
using System.Net;

namespace OLSoftware.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectStatesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly OLSoftwareContext context;
        public ProjectStatesController(OLSoftwareContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtiene una lista de objetos.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var projectStates = context.ProjectStates.ToList();
            var projectStatesDTO = projectStates.Select(x => mapper.Map<ProjectStateDTO>(x)).OrderByDescending(x => x.Id);

            return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = projectStatesDTO });
        }
    }
}
