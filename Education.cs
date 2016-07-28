using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLtoForms
{
    public struct Employer
    {
        public string name;
        public string address;

        public Employer (string Name, string Address)
        {
            this.name = Name;
            this.address = Address;
        }
    }
    public class Education
    {
        public string period { get; private set; }
        public string position { get; private set; }
        public string activities { get; private set; }
        public Employer organization { get; private set; }

        public Education(string Period, string Position, string Activities, Employer Organzation)
        {
            period = Period;
            position = Position;
            activities = Activities;
            organization = Organzation;
        }
        public override string ToString()
        {
            return this.position;
        }
    }
}
