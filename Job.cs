using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLtoForms
{
    public class Job
    {
        public string period { get; private set; }
        public string position {get; private set;}
        public string activities {get; private set;}
        public Employer employer { get; private set; }

        public Job(string Period, string Position, string Activities, Employer Employer)
        {
            period = Period;
            position = Position;
            activities = Activities;
            employer = Employer;
        }

        public override string ToString()
        {
            return this.position;
        }

    }
}
