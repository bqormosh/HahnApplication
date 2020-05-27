using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hahn.ApplicatonProcess.May2020.Domain.Models;
namespace Hahn.ApplicatonProcess.May2020.Data.Managers
{
    public class ApplicantManager
    {
        private readonly ApiContext _context;
        public ApplicantManager(ApiContext context)
        {
            this._context = context;
        }

        // this method should be updated to take a page size and page number, used for paging
        public IEnumerable<Applicant> Get()
        {
            var allApplicants = _context.Applicants.ToArray();
            return allApplicants;
        }
        public Applicant Get(int id)
        {
            var applicant = _context.Applicants.Where(x => x.ID == id).FirstOrDefault();
            return applicant;
        }
        public Applicant Update(Applicant inputApplicant)
        {
            _context.Applicants.Update(inputApplicant);
            var result = _context.SaveChanges();
            if (result > 0)
            {
                return inputApplicant;
            }
            else
            {
                return null;
            }
           
        }

        public Applicant Add(Applicant inputApplicant)
        {
            _context.Applicants.Add(inputApplicant);
            var result = _context.SaveChanges();
            if (result > 0)
            {
                return inputApplicant;
            }
            else
            {
                return null;
            }

        }

        public bool Delete(int id)
        {
            var applicant = _context.Applicants.Where(x => x.ID == id).FirstOrDefault();
            if (applicant != null)
            {
                _context.Applicants.Remove(applicant);
                int result = _context.SaveChanges();
                return result > 0 ? true : false;
            }
            else
            {
                return false;
            }
        }
    }
}
