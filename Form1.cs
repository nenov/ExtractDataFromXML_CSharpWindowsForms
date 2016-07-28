using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace XMLtoForms
{
    public partial class Form1 : Form
    {
        Person me = new Person();
        public Form1()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            InitializeComponent();

            XmlDocument doc = new XmlDocument();
            doc.Load("../../CVNenov.xml");

            //Loading personal data from the XML into the Person Object
            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/CV/Person");            
            foreach (XmlNode node in nodes)
            {
               me = new Person
                    (node.SelectSingleNode("PersonName").InnerText,
                    node.SelectSingleNode("Address").SelectSingleNode("AddressLine").InnerText,
                    node.SelectSingleNode("Address").SelectSingleNode("PostalCode").InnerText,
                    node.SelectSingleNode("Address").SelectSingleNode("Municipality").InnerText,
                    node.SelectSingleNode("Address").SelectSingleNode("Country").InnerText,
                    node.SelectSingleNode("Email").InnerText,
                    node.SelectSingleNode("Telephone").InnerText,
                    node.SelectSingleNode("Website").InnerText);
                nameField.Text = me.name;
                addressField.Text = me.address + "\r\n" + me.postalCode + ", " + me.city + ", " + me.country;
                phoneField.Text = me.phone;
                siteField.Text = me.site;
                emailField.Text = me.email;
            }
            statementField.Text = doc.DocumentElement.SelectSingleNode("Headline").InnerText;

            //Loading job positions from the XML file and passing them to the object's job list
            nodes = doc.DocumentElement.SelectNodes("/CV/WorkExperienceList/WorkExperience");
            foreach (XmlNode node in nodes)
            {
                me.addJob(new Job(node.SelectSingleNode("Period").InnerText,
                                   node.SelectSingleNode("Position").InnerText,
                                    node.SelectSingleNode("Activities").InnerText,
                                    new Employer(node.SelectSingleNode("Employer").SelectSingleNode("Name").InnerText,
                                        node.SelectSingleNode("Employer").SelectSingleNode("Address").InnerText)));
            }
            foreach( Job temp in me.jobs)
            {
                jobsBox.Items.Add(temp);
            }

            //loading the education details into the object
            nodes = doc.DocumentElement.SelectNodes("/CV/EducationList/Education");
            foreach (XmlNode node in nodes)
            {
                me.addEducation(new Education(node.SelectSingleNode("Period").InnerText,
                                              node.SelectSingleNode("Title").InnerText,
                                              node.SelectSingleNode("Activities").InnerText,
                                              new Employer(node.SelectSingleNode("Organisation").SelectSingleNode("Name").InnerText,
                                                           node.SelectSingleNode("Organisation").SelectSingleNode("Address").InnerText)));
            }

            foreach (Education temp in me.education)
            {
                eduBox.Items.Add(temp);
            }

            nodes = doc.DocumentElement.SelectNodes("/CV/AchievementList");
            foreach (XmlNode node in nodes)
            {
                coursesBox.Text = node.SelectSingleNode("Achievement").SelectSingleNode("Description").InnerText;
            }
            nodes = doc.DocumentElement.SelectNodes("/CV/Skills");
            foreach (XmlNode node in nodes)
            {
                skillsBox.Text = node.SelectSingleNode("Computer").SelectSingleNode("Description").InnerText;
                motherBox.Text = node.SelectSingleNode("Linguistic").SelectSingleNode("MotherTongue").InnerText;
                englishBox.Text = node.SelectSingleNode("Linguistic").SelectSingleNode("ForeignLanguageList")
                    .SelectSingleNode("ForeignLanguage").SelectSingleNode("Description").InnerText +": ";
                nodes = doc.DocumentElement.SelectNodes("/CV/Skills/Linguistic/ForeignLanguageList/ForeignLanguage/VerifiedBy/Certificate");
                englishBox.Text += nodes[0].SelectSingleNode("Title").InnerText + ", ";
                englishBox.Text += nodes[1].SelectSingleNode("Title").InnerText;
            }
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            descBox.Items.Clear();
            descBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            descBox.MeasureItem += lst_MeasureItem;
            descBox.DrawItem += lst_DrawItem;
            descBox.Items.Add("Period: " + me.jobs.Find(x => x.position.Contains(jobsBox.SelectedItem.ToString())).period);
            descBox.Items.Add("Activities: " + me.jobs.Find(x => x.position.Contains(jobsBox.SelectedItem.ToString())).activities);
            descBox.Items.Add("Employer: " + me.jobs.Find(x => x.position.Contains(jobsBox.SelectedItem.ToString())).employer.name);
            descBox.Items.Add("Employer's Address: " + me.jobs.Find(x => x.position.Contains(jobsBox.SelectedItem.ToString())).employer.address);                    
        }

        private void eduBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            eduDescBox.Items.Clear();
            eduDescBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            eduDescBox.MeasureItem += second_MeasureItem;
            eduDescBox.DrawItem += second_DrawItem;
            eduDescBox.Items.Add("Period: " + me.education.Find(x => x.position.Contains(eduBox.SelectedItem.ToString())).period);
            eduDescBox.Items.Add("Core Courses: " + me.education.Find(x => x.position.Contains(eduBox.SelectedItem.ToString())).activities);
            eduDescBox.Items.Add("Organisaion: " + me.education.Find(x => x.position.Contains(eduBox.SelectedItem.ToString())).organization.name);
            eduDescBox.Items.Add("Organisaion's Address: " + me.education.Find(x => x.position.Contains(eduBox.SelectedItem.ToString())).organization.address);

        }

        /*not really sure how the word wrapping works. 
       Found this at http://stackoverflow.com/questions/17613613/are-there-anyway-to-make-listbox-items-to-word-wrap-if-string-width-is-higher-th
       */
         private void lst_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = (int)e.Graphics.MeasureString( descBox.Items[e.Index].ToString(), descBox.Font, descBox.Width).Height;
        }

        private void lst_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            e.Graphics.DrawString(descBox.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
        }
        private void second_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = (int)e.Graphics.MeasureString(eduDescBox.Items[e.Index].ToString(), eduDescBox.Font, eduDescBox.Width).Height;
        }

        private void second_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            e.Graphics.DrawString(eduDescBox.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
        }
       

        

        


    }
}
