using System;
using System.Collections.Generic;
using System.Linq;

namespace IRSACA.src
{ 
    public class GetFormValues 
    { 
        Query q = new Query();
        public ACA1095C Get1095CValues(string employeeId, List<ACACode> code, ACARate rate, List<ACACompanyDetail> company, string year) //
        {
            var aca = q.Get1095data(year, employeeId);
            var empldetail = q.GetEmployeeDetail(employeeId, year);  

            ACA1095C ac = new ACA1095C();

            var emplaca = (from a in aca
                           where a.relation == "E"
                           select a).FirstOrDefault();

            var empldept = (from a in aca
                            where a.relation != "E"
                            select a).ToList();

            var compl = (from c in company
                         where c.CompanyCode == emplaca.Company
                         select c).FirstOrDefault();

            var codetype = (from c in code
                            where c.employeetype == emplaca.empltype
                            select c).ToList();
            var deptdetail = (from d in empldept
                              select d).FirstOrDefault();

             
            //get empl detail
             
            ac.ssn = empldetail.ssn;//.Insert(5, "-").Insert(3, "-"); // 
            ac.Name = empldetail.Fname + " " + empldetail.Lname;
            ac.FName = empldetail.Fname;
            ac.LName = empldetail.Lname;
            ac.address = empldetail.address;
            ac.city = empldetail.city;
            ac.state = empldetail.state;
            ac.zipcode = empldetail.zipcode;
            ac.country = empldetail.country;

            //get company detail

            ac.Company = compl.Company;
            ac.ein = compl.ein;//.Insert(2, "-"); //compl.ein.Substring(0,2) + "-" + compl.ein.Substring(2);
            ac.companyaddress = compl.address;
            ac.companyphone = compl.phone;
            ac.companycity = compl.city;
            ac.companystate = compl.state;
            ac.companycountry = compl.country;
            ac.companyzipcode = compl.zipcode;

            var form14 = new ACA1095Cmonths();
            var form15 = new ACA1095Cmonths();
            var form16 = new ACA1095Cmonths();

            var formpart3 = new ACA1095Cmonths();

            var jan = (from c in codetype
                       where c.code == emplaca.formdata.jan
                       select c).FirstOrDefault();
            var feb = (from c in codetype
                       where c.code == emplaca.formdata.feb
                       select c).FirstOrDefault();
            var mar = (from c in codetype
                       where c.code == emplaca.formdata.mar
                       select c).FirstOrDefault();
            var apr = (from c in codetype
                       where c.code == emplaca.formdata.apr
                       select c).FirstOrDefault();
            var may = (from c in codetype
                       where c.code == emplaca.formdata.may
                       select c).FirstOrDefault();
            var jun = (from c in codetype
                       where c.code == emplaca.formdata.jun
                       select c).FirstOrDefault();
            var jul = (from c in codetype
                       where c.code == emplaca.formdata.jul
                       select c).FirstOrDefault();
            var aug = (from c in codetype
                       where c.code == emplaca.formdata.aug
                       select c).FirstOrDefault();
            var sep = (from c in codetype
                       where c.code == emplaca.formdata.sep
                       select c).FirstOrDefault();
            var oct = (from c in codetype
                       where c.code == emplaca.formdata.oct
                       select c).FirstOrDefault();
            var nov = (from c in codetype
                       where c.code == emplaca.formdata.nov
                       select c).FirstOrDefault();
            var dec = (from c in codetype
                       where c.code == emplaca.formdata.dec
                       select c).FirstOrDefault();

            //filing form 14,15,16

            form14.all12 = "";
            form15.all12 = "";
            form16.all12 = "";

            //check if insured all year

            var emplformdata = emplaca.formdata;

            var checkall12plan = new[] { emplformdata }.All(s => checkhasplan(s.jan) == "Y" && checkhasplan(s.feb) == "Y" && checkhasplan(s.mar) == "Y"
                        && checkhasplan(s.apr) == "Y" && checkhasplan(s.may) == "Y" && checkhasplan(s.jun) == "Y" && checkhasplan(s.jul) == "Y" && checkhasplan(s.aug) == "Y"
                         && checkhasplan(s.sep) == "Y" && checkhasplan(s.oct) == "Y" && checkhasplan(s.nov) == "Y" && checkhasplan(s.dec) == "Y");

            var checkwithdrawn12plan = new[] { emplformdata }.All(s => checkwidrawnplan(s.jan) == "Y" && checkwidrawnplan(s.feb) == "Y" && checkwidrawnplan(s.mar) == "Y"
                        && checkwidrawnplan(s.apr) == "Y" && checkwidrawnplan(s.may) == "Y" && checkwidrawnplan(s.jun) == "Y" && checkwidrawnplan(s.jul) == "Y" && checkwidrawnplan(s.aug) == "Y"
                         && checkwidrawnplan(s.sep) == "Y" && checkwidrawnplan(s.oct) == "Y" && checkwidrawnplan(s.nov) == "Y" && checkwidrawnplan(s.dec) == "Y");
             
            if(checkall12plan || checkwithdrawn12plan )
            {
                form14.all12 = jan.form14;
                form16.all12 = jan.form16;

                //test form15
              //  form15.minamt = 

                //jan
                form14.jan = "";
                form16.jan = "";

                //feb
                form14.feb = "";
                form16.feb = "";

                //mar
                form14.mar = "";
                form16.mar = "";

                //apr
                form14.apr = "";
                form16.apr = "";

                //may
                form14.may = "";
                form16.may = "";

                //jun
                form14.jun = "";
                form16.jun = "";

                //jul
                form14.jul = "";
                form16.jul = "";

                //aug
                form14.aug = "";
                form16.aug = "";

                //sep
                form14.sep = "";
                form16.sep = "";

                //oct
                form14.oct = "";
                form16.oct = "";

                //nov
                form14.nov = "";
                form16.nov = "";

                //dec
                form14.dec = "";
                form16.dec = "";

                 
            }
            else
            {
                //jan
                form14.jan = jan.form14;
                form16.jan = jan.form16;

                //feb
                form14.feb = feb.form14;
                form16.feb = feb.form16;

                //mar
                form14.mar = mar.form14;
                form16.mar = mar.form16;

                //apr
                form14.apr = apr.form14;
                form16.apr = apr.form16;

                //may
                form14.may = may.form14;
                form16.may = may.form16;

                //jun
                form14.jun = jun.form14;
                form16.jun = jun.form16;

                //jul
                form14.jul = jul.form14;
                form16.jul = jul.form16;

                //aug
                form14.aug = aug.form14;
                form16.aug = aug.form16;

                //sep
                form14.sep = sep.form14;
                form16.sep = sep.form16;

                //oct
                form14.oct = oct.form14;
                form16.oct = oct.form16;

                //nov
                form14.nov = nov.form14;
                form16.nov = nov.form16;

                //dec
                form14.dec = dec.form14;
                form16.dec = dec.form16;

                  
            }

            //part 3 - check if employee is covered

            if (new[] { emplformdata }.All(s => chkplaninclcobra(s.jan) == "Y" || chkplaninclcobra(s.feb) == "Y" || chkplaninclcobra(s.mar) == "Y"
                       || chkplaninclcobra(s.apr) == "Y" || chkplaninclcobra(s.may) == "Y" || chkplaninclcobra(s.jun) == "Y" || chkplaninclcobra(s.jul) == "Y" || chkplaninclcobra(s.aug) == "Y"
                        || chkplaninclcobra(s.sep) == "Y" || chkplaninclcobra(s.oct) == "Y" || chkplaninclcobra(s.nov) == "Y" || chkplaninclcobra(s.dec) == "Y"))
            {
                formpart3.iscovered = "Y";
            } 

            if (new[] { emplformdata }.All(s => chkplaninclcobra(s.jan) == "Y" && chkplaninclcobra(s.feb) == "Y" && chkplaninclcobra(s.mar) == "Y"
                       && chkplaninclcobra(s.apr) == "Y" && chkplaninclcobra(s.may) == "Y" && chkplaninclcobra(s.jun) == "Y" && chkplaninclcobra(s.jul) == "Y" && chkplaninclcobra(s.aug) == "Y"
                        && chkplaninclcobra(s.sep) == "Y" && chkplaninclcobra(s.oct) == "Y" && chkplaninclcobra(s.nov) == "Y" && chkplaninclcobra(s.dec) == "Y"))
            { 
                
                //form part 3 data

            formpart3.all12 = "1";
            formpart3.jan = "";
            formpart3.feb = "";
            formpart3.mar = "";
            formpart3.apr = "";
            formpart3.may = "";
            formpart3.jun = "";
            formpart3.jul = "";
            formpart3.aug = "";
            formpart3.sep = "";
            formpart3.oct = "";
            formpart3.nov = "";
            formpart3.dec = "";

        } 
        else{
            //form part 3 data

            formpart3.all12 = "";
            formpart3.jan = chkplaninclcobra(emplformdata.jan) == "Y" ? "1" : "0";
            formpart3.feb = chkplaninclcobra(emplformdata.feb) == "Y" ? "1" : "0";
            formpart3.mar = chkplaninclcobra(emplformdata.mar) == "Y" ? "1" : "0";
            formpart3.apr = chkplaninclcobra(emplformdata.apr) == "Y" ? "1" : "0";
            formpart3.may = chkplaninclcobra(emplformdata.may) == "Y" ? "1" : "0";
            formpart3.jun = chkplaninclcobra(emplformdata.jun) == "Y" ? "1" : "0";
            formpart3.jul = chkplaninclcobra(emplformdata.jul) == "Y" ? "1" : "0";
            formpart3.aug = chkplaninclcobra(emplformdata.aug) == "Y" ? "1" : "0";
            formpart3.sep = chkplaninclcobra(emplformdata.sep) == "Y" ? "1" : "0";
            formpart3.oct = chkplaninclcobra(emplformdata.oct) == "Y" ? "1" : "0";
            formpart3.nov = chkplaninclcobra(emplformdata.nov) == "Y" ? "1" : "0";
            formpart3.dec = chkplaninclcobra(emplformdata.dec) == "Y" ? "1" : "0";
          }


            //form 15
            //jan 

            switch (jan.form15)
            {
                case "S":
                    form15.jan = rate.Single1;
                    break;

                case "C":
                    form15.jan = rate.Cobra1;
                    break;

                default:
                    form15.jan = "";
                    break;
            }
            
            //feb
            
            switch (feb.form15)
            {
                case "S":
                    form15.feb = rate.Single1;
                    break;

                case "C":
                    form15.feb = rate.Cobra1;
                    break;

                default:
                    form15.feb = "";
                    break;
            }
             
            //mar 

            switch (mar.form15)
            {
                case "S":
                    form15.mar = rate.Single1;
                    break;

                case "C":
                    form15.mar = rate.Cobra1;
                    break;

                default:
                    form15.mar = "";
                    break;
            }
             
            //apr 

            switch (apr.form15)
            {
                case "S":
                    form15.apr = rate.Single1;
                    break;

                case "C":
                    form15.apr = rate.Cobra1;
                    break;

                default:
                    form15.apr = "";
                    break;
            }
             
            //may
             

            switch (may.form15)
            {
                case "S":
                    form15.may = rate.Single1;
                    break;

                case "C":
                    form15.may = rate.Cobra1;
                    break;

                default:
                    form15.may = "";
                    break;
            }
             

            //jun
             

            switch (jun.form15)
            {
                case "S":
                    form15.jun = rate.Single2;
                    break;

                case "C":
                    form15.jun = rate.Cobra2;
                    break;

                default:
                    form15.jun = "";
                    break;
            }
             

            //jul 

            switch (jul.form15)
            {
                case "S":
                    form15.jul = rate.Single2;
                    break;

                case "C":
                    form15.jul = rate.Cobra2;
                    break;

                default:
                    form15.jul = "";
                    break;
            }
             
            //aug 

            switch (aug.form15)
            {
                case "S":
                    form15.aug = rate.Single2;
                    break;

                case "C":
                    form15.aug = rate.Cobra2;
                    break;

                default:
                    form15.aug = "";
                    break;
            }
             
            //sep 

            switch (sep.form15)
            {
                case "S":
                    form15.sep = rate.Single2;
                    break;

                case "C":
                    form15.sep = rate.Cobra2;
                    break;

                default:
                    form15.sep = "";
                    break;
            }
             
            //oct 

            switch (oct.form15)
            {
                case "S":
                    form15.oct = rate.Single2;
                    break;

                case "C":
                    form15.oct = rate.Cobra2;
                    break;

                default:
                    form15.oct = "";
                    break;
            }
             
            //nov 

            switch (nov.form15)
            {
                case "S":
                    form15.nov = rate.Single2;
                    break;

                case "C":
                    form15.nov = rate.Cobra2;
                    break;

                default:
                    form15.nov = "";
                    break;
            }
              
            //dec 

            switch (dec.form15)
            {
                case "S":
                    form15.dec = rate.Single2;
                    break;

                case "C":
                    form15.dec = rate.Cobra2;
                    break;

                default:
                    form15.dec = "";
                    break;
            }

            // end  form 15

            ac.form14 = form14;
            ac.form15 = form15;
            ac.form16 = form16;

            ac.formpart3 = formpart3;
             
                //filling dependant
                List<ACA1095Cdependant> dptls = new List<ACA1095Cdependant>();
                foreach (var dept in empldept)
                {
                    ACA1095Cdependant dpt = new ACA1095Cdependant();
                    dpt.form17de = new ACA1095Cmonths();
                    dpt.name = dept.Name;
                    dpt.dob = dept.bday;

                    var formdata = dept.formdata;
                    if (new[] { formdata }.All(s => chkplaninclcobra(s.jan) == "Y" && chkplaninclcobra(s.feb) == "Y" && chkplaninclcobra(s.mar) == "Y"
                        && chkplaninclcobra(s.apr) == "Y" && chkplaninclcobra(s.may) == "Y" && chkplaninclcobra(s.jun) == "Y" && chkplaninclcobra(s.jul) == "Y" && chkplaninclcobra(s.aug) == "Y"
                         && chkplaninclcobra(s.sep) == "Y" && chkplaninclcobra(s.oct) == "Y" && chkplaninclcobra(s.nov) == "Y" && chkplaninclcobra(s.dec) == "Y"))
                    {
                        dpt.form17de.all12 = "1";
                        dpt.form17de.jan = "";
                        dpt.form17de.feb = "";
                        dpt.form17de.mar = "";
                        dpt.form17de.apr = "";
                        dpt.form17de.may = "";
                        dpt.form17de.jun = "";
                        dpt.form17de.jul = "";
                        dpt.form17de.aug = "";
                        dpt.form17de.sep = "";
                        dpt.form17de.oct = "";
                        dpt.form17de.nov = "";
                        dpt.form17de.dec = "";
                    }
                    else
                    {
                        dpt.form17de.all12 = "";
                        dpt.form17de.jan = chkplaninclcobra(formdata.jan) == "Y" ? "1" : "0";
                        dpt.form17de.feb = chkplaninclcobra(formdata.feb) == "Y" ? "1" : "0";
                        dpt.form17de.mar = chkplaninclcobra(formdata.mar) == "Y" ? "1" : "0";
                        dpt.form17de.apr = chkplaninclcobra(formdata.apr) == "Y" ? "1" : "0";
                        dpt.form17de.may = chkplaninclcobra(formdata.may) == "Y" ? "1" : "0";
                        dpt.form17de.jun = chkplaninclcobra(formdata.jun) == "Y" ? "1" : "0";
                        dpt.form17de.jul = chkplaninclcobra(formdata.jul) == "Y" ? "1" : "0";
                        dpt.form17de.aug = chkplaninclcobra(formdata.aug) == "Y" ? "1" : "0";
                        dpt.form17de.sep = chkplaninclcobra(formdata.sep) == "Y" ? "1" : "0";
                        dpt.form17de.oct = chkplaninclcobra(formdata.oct) == "Y" ? "1" : "0";
                        dpt.form17de.nov = chkplaninclcobra(formdata.nov) == "Y" ? "1" : "0";
                        dpt.form17de.dec = chkplaninclcobra(formdata.dec) == "Y" ? "1" : "0";
                    }
                    dptls.Add(dpt);
                }

                ac.dependant = dptls;
           
            return ac;
        }

        private string checkhasplan(string plan)
        {
            if (new[] { plan }.All(p => p == "C" || p == "G"))  
            {
                return "Y";
            }
            else
            {
                return "N";
            }
        }

        private string checkwidrawnplan(string plan)
        {
            if (new[] { plan }.All(p => p == "W"))
            {
                return "Y";
            }
            else
            {
                return "N";
            }
        }

        private string chkplaninclcobra(string plan)
        {
            if (new[] { plan }.All(p => p == "C" || p == "B" || p == "G"))
            {
                return "Y";
            }
            else
            {
                return "N";
            }
        }
    }
}