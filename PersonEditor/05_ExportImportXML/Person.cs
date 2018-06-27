using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExportImportXML
{
    public class Person : INotifyPropertyChanged
    {
        string m_Name;
        string m_Surname;
        DateTime m_DateOfBirth;

        public event PropertyChangedEventHandler PropertyChanged;

        //Customize XML serialization by using proper attributes
        //Check other available attributes and their usage, under the namespace System.Xml.Serialization
        [XmlAttribute]
        public string GUID { get; set; }

        public string Name
        {
            get { return m_Name; }
            set
            {
                if(value != m_Name)
                {
                    m_Name = value;
                    RaisePropertyChanged(nameof(Name));
                }
            }
        }

        public string Surname
        {
            get { return m_Surname; }
            set
            {
                if (value != m_Surname)
                {
                    m_Surname = value;
                    RaisePropertyChanged(nameof(Surname));
                }
            }
        }

        public DateTime DateOfBirth
        {
            get { return m_DateOfBirth; }
            set
            {
                if (value != m_DateOfBirth)
                {
                    m_DateOfBirth = value;
                    RaisePropertyChanged(nameof(DateOfBirth));
                }
            }
        }

        public Person()
        {
            this.DateOfBirth = DateTime.Today;
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
