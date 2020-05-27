using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hahn.ApplicatonProcess.May2020.Domain.ViewModels
{
    public class ApplicantViewModel
    {
        /// <summary>
        /// The name of the Applicant (at least 5 characters)
        /// </summary>
        /// <example>Bassam</example>
        public string Name { get; set; }



        /// <summary>
        /// The Family Name of the Applicant (at least 5 characters)
        /// </summary>
        /// <example>Qormosh</example>
        public string FamilyName { get; set; }


        /// <summary>
        /// The home address of the applicant (at least 10 characters)
        /// </summary>
        /// <example>Gaza, Palestine</example>
        public string Address { get; set; }



        /// <summary>
        /// a valid country name
        /// </summary>
        /// <example>USA</example>
        public string CountryOfOrigin { get; set; }



        /// <summary>
        /// a valid email address
        /// </summary>
        /// <example>john.smith@gmail.com</example>
        public string EmailAdress { get; set; }




        /// <summary>
        /// applicant age between 20 to 60
        /// </summary>
        /// <example>29</example>
        public int Age { get; set; }



        /// <summary>
        /// value indicate if the applicant is hired or not
        /// </summary>
        /// <example>true</example>
        [DefaultValue(false)]
        public bool Hired { get; set; }
    }
}
