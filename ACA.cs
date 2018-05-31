using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace irsAca
{
    public class ACA1095C
    {
        public string Name { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string ssn { get; set; } 
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string zipcode { get; set; }

        public string Company { get; set; }
        public string ein { get; set; } 
        public string companyaddress { get; set; }
        public string companyphone { get; set; }
        public string companycity { get; set; }
        public string companystate { get; set; }
        public string companycountry { get; set; }
        public string companyzipcode { get; set; }

        
        public ACA1095Cmonths form14 { get; set; }
        public ACA1095Cmonths form15 { get; set; }
        public ACA1095Cmonths form16 { get; set; }

        public ACA1095Cmonths formpart3 { get; set; }

        public List<ACA1095Cdependant> dependant { get; set; }
         
    }

    public class ACA1095rows
    {
        //
        public string payroll { get; set; }
        public string Name { get; set; }
        public string Company { get; set; } 
        public string bday { get; set; }
        public string empltype { get; set; }
        public string relation { get; set; }
        public string year { get; set; }
        public string Fileno { get; set; }
        public ACA1095Cmonths formdata { get; set; }
    }

    public class ACA1095Cdependant
    {
        public string name { get; set; }
        public string ssn { get; set; }
        public string dob { get; set; }
        public ACA1095Cmonths form17de { get; set; }
    }

    public class ACA1095Cmonths
    {
        public string all12 { get; set; }
        public string jan { get; set; }
        public string feb { get; set; }
        public string mar { get; set; }
        public string apr { get; set; }
        public string may { get; set; }
        public string jun { get; set; }
        public string jul { get; set; }
        public string aug { get; set; }
        public string sep { get; set; }
        public string oct { get; set; }
        public string nov { get; set; }
        public string dec { get; set; }
        public string minamt { get; set; }
        public string iscovered { get; set; }
    }

    public class ACACode
    {
        public string code { get; set; }
        public string form14 { get; set; }
        public string form15 { get; set; }
        public string form16 { get; set; }
        public string employeetype { get; set; }
    }

    public class ACARate
    {
        public string year { get; set; }
        public string Single1 { get; set; }
        public string Single2 { get; set; }
        public string Cobra1 { get; set; }
        public string Cobra2 { get; set; }

    }

    public class ACACompanyDetail
    { 
        public string ein { get; set; } 
        public string Company { get; set; }
        public string CompanyCode { get; set; }
        public string Contact { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string zipcode { get; set; }
        public string year { get; set; } 
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string ContactFName { get; set; }
        public string ContactLName { get; set; }

    }

    public class ACAEmployeeDetail
    {
        public string Name { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Payroll { get; set; }
        public string Employeetype { get; set; }
        public string ssn { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string zipcode { get; set; }

    }

    public class ACAException
    {
        public string Name { get; set; }
        public string LocalName { get; set; }
        public string NamespaceURI { get; set; }
        public string LineNumber { get; set; }
        public string LinePosition { get; set; }
        public string Message { get; set; } 

    }

    public class ACA1094Count
    {
        public string total { get; set; } 

        public string janft { get; set; }
        public string febft { get; set; }
        public string marft { get; set; }
        public string aprft { get; set; }
        public string mayft { get; set; }
        public string junft { get; set; }
        public string julft { get; set; }
        public string augft { get; set; }
        public string sepft { get; set; }
        public string octft { get; set; }
        public string novft { get; set; }
        public string decft { get; set; }

        public string janall { get; set; }
        public string feball { get; set; }
        public string marall { get; set; }
        public string aprall { get; set; }
        public string mayall { get; set; }
        public string junall { get; set; }
        public string julall { get; set; }
        public string augall { get; set; }
        public string sepall { get; set; }
        public string octall { get; set; }
        public string novall { get; set; }
        public string decall { get; set; }
    }

    public class ACAUniqReceipt
    {
        public string UniqueReceipt { get; set; }
        public string ErrorCode { get; set; }
        public string Count  { get; set; }
    }
 
}
