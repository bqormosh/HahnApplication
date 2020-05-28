using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.May2020.Data;
using Hahn.ApplicatonProcess.May2020.Domain.Models;
using Hahn.ApplicatonProcess.May2020.Domain.ValidationModels;
using Hahn.ApplicatonProcess.May2020.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Hahn.ApplicatonProcess.May2020.Data.Managers;
using Serilog;
using Serilog.Core;
using Microsoft.Extensions.Configuration;

namespace Hahn.ApplicatonProcess.May2020.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {

        private ApplicantManager applicantManager;
        private IConfigurationRoot configuration;
        private Logger logger;

        public ApplicantController(ApiContext context)
        {
            applicantManager = new ApplicantManager(context);
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            
        }
        [HttpGet]
        public IEnumerable<Applicant> Get()
        {
            var allApplicants = applicantManager.Get();
            logger.Information("Requesting a List of Applicant with Get Request");
            return allApplicants;
        }
        // GET: api/Applicant/5
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var applicant = applicantManager.Get(id);
            if (applicant != null)
            {
                logger.Information($"Sending applicant data with Id :{id}");
                return Ok(applicant);
            }
            else
            {
                logger.Information($"applicant with Id :{id} is not found");

                return NotFound();
            }
           
        }

        // POST: api/Applicant
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] ApplicantViewModel inputApplicant)
        {
            // mapping shoud be done using Automapper or similar libraries but for simplicity it is mapped manually
            var applicant = new Applicant { Name = inputApplicant.Name, FamilyName = inputApplicant.FamilyName, Address = inputApplicant.Address,Age  = inputApplicant.Age, CountryOfOrigin = inputApplicant.CountryOfOrigin, EmailAdress = inputApplicant.EmailAdress,Hired = inputApplicant.Hired};
            var applicantValidator = new ApplicantValidator();
            var result = await applicantValidator.ValidateAsync(applicant);
            if (result.IsValid)
            {
                var addedApplicant = applicantManager.Add(applicant);
                if (addedApplicant != null)
                {
                    logger.Information($"Added new Apllicant with Id:{applicant.ID}");
                    return CreatedAtAction("Get", new { id = applicant.ID }, applicant);
                    
                }
                else
                {
                    logger.Warning("Failed to Add a new Applicant with Post request");
                    return BadRequest();
                }
               
            }
            else
            {
                logger.Warning("Failed to Add a new Applicant with Post request, validation failed");
                return BadRequest();

            }
            
        }

        // PUT: api/Applicant/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromBody] Applicant inputApplicant)
        {
            var applicant = inputApplicant;
            var applicantValidator = new ApplicantValidator();
            var result = await applicantValidator.ValidateAsync(applicant);
            if (result.IsValid)
            {

                    var updatedApplicant = applicantManager.Update(applicant);
                    if (updatedApplicant != null)
                    {
                        logger.Information($"updated Apllicant with Id:{applicant.ID}");
                        return Ok(updatedApplicant);
                    }
                    else
                    {
                        logger.Warning("Failed to update Applicant with Put request");
                        return BadRequest();
                    }
                    
            }
            else
            {
                logger.Warning("Failed to update Applicant with Put request, validation failed!");
                return BadRequest();

            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {

            bool result = applicantManager.Delete(id);
            if (result)
            {
                logger.Information($"Apllicant was deleted successfully with Id:{id}");
                return Ok();
            }
            else
            {
                logger.Warning("Failed to deleted applicant");
                return BadRequest();
            }

        }
    }
}
