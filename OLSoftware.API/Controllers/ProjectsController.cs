using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OLSoftware.BL.DTOs;
using OLSoftware.BL.Models;
using System;
using System.Linq;
using System.Net;

namespace OLSoftware.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly OLSoftwareContext context;
        public ProjectsController(OLSoftwareContext context,
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
            var projects = context.Projects.Include(x => x.Customer).Include(x => x.ProjectState).ToList();
            var projectsDTO = projects.Select(x => mapper.Map<ProjectDTO>(x)).OrderByDescending(x => x.Id);

            return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = projectsDTO });
        }

        /// <summary>
        /// Obtiene un objeto por su id.
        /// </summary>
        /// <param name="id">Id del objeto</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var project = context.Projects.Include(x => x.Customer).Include(x => x.ProjectState).FirstOrDefault(x => x.Id == id);
            if (project == null)
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

            var projectDTO = mapper.Map<ProjectDTO>(project);
            return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = projectDTO });
        }

        /// <summary>
        /// Crea un objeto.
        /// </summary>
        /// <param name="projectDTO">Objeto</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(ProjectDTO projectDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var project = context.Projects.Add(mapper.Map<Project>(projectDTO)).Entity;
                context.SaveChanges();
                project.Id = project.Id;

                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = projectDTO });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }

        /// <summary>
        /// Edita un objeto
        /// </summary>
        /// <param name="id">Id del objeto</param>
        /// <param name="projectDTO">Objeto</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Edit(int id, ProjectDTO projectDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var project = context.Projects.Find(id);
                if (project == null)
                    return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

                context.Entry(project).State = EntityState.Detached;
                context.Projects.Update(mapper.Map<Project>(projectDTO));
                context.SaveChanges();

                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = projectDTO });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un objeto
        /// </summary>
        /// <param name="id">Id del objeto</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var project = context.Projects.Find(id);
                if (project == null)
                    return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

                if (context.ProjectLanguages.Any(x => x.Id == id))
                    throw new Exception("Dependencies");

                context.Projects.Remove(project);
                context.SaveChanges();

                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Message = "Se ha realizado el proceso con exito." });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }
    }
}
