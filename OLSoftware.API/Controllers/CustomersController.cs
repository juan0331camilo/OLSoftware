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
    public class CustomersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly OLSoftwareContext context;
        public CustomersController(OLSoftwareContext context,
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
            var customers = context.Customers.ToList();
            var customersDTO = customers.Select(x => mapper.Map<CustomerDTO>(x)).OrderByDescending(x => x.Id);

            return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = customersDTO });
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
            var customer = context.Customers.Find(id);
            if (customer == null)
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

            var customerDTO = mapper.Map<CustomerDTO>(customer);
            return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = customerDTO });
        }

        /// <summary>
        /// Crea un objeto.
        /// </summary>
        /// <param name="customerDTO">Objeto</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(CustomerDTO customerDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var customer = context.Customers.Add(mapper.Map<Customer>(customerDTO)).Entity;
                context.SaveChanges();
                customer.Id = customer.Id;

                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = customerDTO });
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
        /// <param name="customerDTO">Objeto</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Edit(int id, CustomerDTO customerDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var customer = context.Customers.Find(id);
                if (customer == null)
                    return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

                context.Entry(customer).State = EntityState.Detached;
                context.Customers.Update(mapper.Map<Customer>(customerDTO));
                context.SaveChanges();

                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = customerDTO });
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
                var customer = context.Customers.Find(id);
                if (customer == null)
                    return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

                if (context.Projects.Any(x => x.Id == id))
                    throw new Exception("Dependencies");

                context.Customers.Remove(customer);
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
