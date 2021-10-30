using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OLSoftware.BL.DTOs;
using OLSoftware.BL.Helpers;
using OLSoftware.BL.Services.Implements;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace OLSoftware.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ApiService apiService = new ApiService();

        public async Task<IActionResult> Index()
        {
            var responseDTO = await apiService.RequestAPI<List<ProjectDTO>>(Endpoints.URL_BASE,
                Endpoints.GET_PROJECTS,
                null,
                ApiService.Method.Get);

            var projects = (List<ProjectDTO>)responseDTO.Data;
            return View(projects);
        }

        [HttpGet]
        public async Task<IActionResult> Download()
        {
            var responseDTO = await apiService.RequestAPI<Dictionary<string, string>>(Endpoints.URL_BASE_FUNCTION,
                Endpoints.GET_PROJECTS_REPORT,
                null,
                ApiService.Method.Get,
                false);

            var data = (Dictionary<string, string>)responseDTO.Data;
            var fileDownload = Convert.FromBase64String(data["fileBase64Str"]);

            return File(fileDownload, MediaTypeNames.Application.Octet, "Report.txt");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadData();
            return View(new ProjectDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectDTO projectDTO)
        {
            await LoadData();

            var responseDTO = await apiService.RequestAPI<ProjectDTO>(Endpoints.URL_BASE,
                Endpoints.POST_PROJECT,
                projectDTO,
                ApiService.Method.Post);

            if (responseDTO.Code == (int)HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));

            return View(projectDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            await LoadData();

            var responseDTO = await apiService.RequestAPI<ProjectDTO>(Endpoints.URL_BASE,
                Endpoints.GET_PROJECT + id,
                null,
                ApiService.Method.Get);

            var project = (ProjectDTO)responseDTO.Data;

            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProjectDTO projectDTO)
        {
            var responseDTO = await apiService.RequestAPI<ProjectDTO>(Endpoints.URL_BASE,
                Endpoints.PUT_PROJECT + projectDTO.Id,
                projectDTO,
                ApiService.Method.Put);

            if (responseDTO.Code == (int)HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));

            return View(projectDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var responseDTO = await apiService.RequestAPI<ProjectDTO>(Endpoints.URL_BASE,
                Endpoints.GET_PROJECT + id,
                null,
                ApiService.Method.Get);

            var project = (ProjectDTO)responseDTO.Data;

            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProjectDTO projectDTO)
        {
            var responseDTO = await apiService.RequestAPI<ProjectDTO>(Endpoints.URL_BASE,
                Endpoints.DELETE_PROJECT + projectDTO.Id,
                null,
                ApiService.Method.Delete);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Language(int id)
        {
            await GetLanguages();

            var responseDTO = await apiService.RequestAPI<List<ProjectLanguageDTO>>(Endpoints.URL_BASE,
                Endpoints.GET_PROJECT_LANGUAGES + id,
                null,
                ApiService.Method.Get);

            var projectLanguages = (List<ProjectLanguageDTO>)responseDTO.Data;
            ViewData["projectLanguages"] = projectLanguages;

            return View(new ProjectLanguageDTO { ProjectId = id });
        }

        [HttpPost]
        public async Task<IActionResult> Language(ProjectLanguageDTO projectLanguageDTO)
        {
            await GetLanguages();

            projectLanguageDTO.Id = 0;
            var responseDTO = await apiService.RequestAPI<ProjectLanguageDTO>(Endpoints.URL_BASE,
                Endpoints.POST_PROJECT_LANGUAGE,
                projectLanguageDTO,
                ApiService.Method.Post);

            return RedirectToAction(nameof(Language), new { id = projectLanguageDTO.ProjectId });
        }

        private async Task GetLanguages()
        {
            var responseDTO = await apiService.RequestAPI<List<LanguageDTO>>(Endpoints.URL_BASE,
                Endpoints.GET_LANGUAGES,
                null,
                ApiService.Method.Get);

            var languages = (List<LanguageDTO>)responseDTO.Data;
            ViewData["languages"] = new SelectList(languages, "Id", "Name");
        }

        private async Task LoadData()
        {
            var responseDTO = await apiService.RequestAPI<List<CustomerDTO>>(Endpoints.URL_BASE,
                Endpoints.GET_CUSTOMERS,
                null,
                ApiService.Method.Get);

            var customers = (List<CustomerDTO>)responseDTO.Data;

            responseDTO = await apiService.RequestAPI<List<ProjectStateDTO>>(Endpoints.URL_BASE,
                Endpoints.GET_PROJECT_STATES,
                null,
                ApiService.Method.Get);

            var projectStates = (List<ProjectStateDTO>)responseDTO.Data;

            ViewData["customers"] = new SelectList(customers, "Id", "FullName");
            ViewData["projectStates"] = new SelectList(projectStates, "Id", "Name");
        }
    }
}
