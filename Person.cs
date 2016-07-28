using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLtoForms
{
    
    public class Person
    {
        public string name {get; private set;}
        public string email { get; private set; }
        public string phone { get; private set; }
        public string site { get; private set; }
        public string address { get; private set; }
        public string postalCode { get; private set; }
        public string city { get; private set; }
        public string country { get; private set; }
        public List<Job> jobs = new List<Job>();
        public List<Education> education = new List<Education>();
 
        public Person( string Name, string Address, string Code, string City, string Country, string Email, string Phone, String Site)
        {
            name = Name;
            address = Address;
            postalCode = Code;
            city = City;
            country = Country;
            email = Email;
            phone = Phone;
            site = Site;
        }

        public Person()
        {
        }
   
        public void addJob(Job experience)
        {
            this.jobs.Add(experience);
        }

        public void addEducation(Education educ)
        {
            this.education.Add(educ);
        }
    
    }
}
