using Microsoft.AspNetCore.Mvc;
using OLSoftware.BL.DTOs;
using OLSoftware.BL.Helpers;
using OLSoftware.BL.Services.Implements;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace OLSoftware.Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApiService apiService = new ApiService();

        public async Task<IActionResult> Index()
        {
            var responseDTO = await apiService.RequestAPI<List<CustomerDTO>>(Endpoints.URL_BASE,
                Endpoints.GET_CUSTOMERS,
                null,
                ApiService.Method.Get);

            var customers = (List<CustomerDTO>)responseDTO.Data;
            return View(customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CustomerDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerDTO customerDTO)
        {
            var responseDTO = await apiService.RequestAPI<CustomerDTO>(Endpoints.URL_BASE,
                Endpoints.POST_CUSTOMER,
                customerDTO,
                ApiService.Method.Post);

            if (responseDTO.Code == (int)HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));

            return View(customerDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {        
            var responseDTO = await apiService.RequestAPI<CustomerDTO>(Endpoints.URL_BASE,
                Endpoints.GET_CUSTOMER + id,
                null,
                ApiService.Method.Get);

            var customer = (CustomerDTO)responseDTO.Data;

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerDTO customerDTO)
        {
            var responseDTO = await apiService.RequestAPI<CustomerDTO>(Endpoints.URL_BASE,
                Endpoints.PUT_CUSTOMER + customerDTO.Id,
                customerDTO,
                ApiService.Method.Put);

            if (responseDTO.Code == (int)HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));

            return View(customerDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var responseDTO = await apiService.RequestAPI<CustomerDTO>(Endpoints.URL_BASE,
                Endpoints.GET_CUSTOMER + id,
                null,
                ApiService.Method.Get);

            var customer = (CustomerDTO)responseDTO.Data;

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CustomerDTO customerDTO)
        {
            var responseDTO = await apiService.RequestAPI<CustomerDTO>(Endpoints.URL_BASE,
                Endpoints.DELETE_CUSTOMER + customerDTO.Id,
                null,
                ApiService.Method.Delete);

            return RedirectToAction(nameof(Index));
        }
    }
}
