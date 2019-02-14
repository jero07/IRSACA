using System;
using IRSACA.src;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using iTextSharp.LGPLv2;
using iTextSharp.text.pdf;
using System.IO;
using IRSACA.src.helper;
using System.Linq;

namespace IRSACA
{
    internal class Generatepdf
    {
        Query q = new Query(); 
        GetFormValues f = new GetFormValues();
        internal void Generatebasepdf(IConfiguration configuration)
        {
            var year = configuration["Project:YEAR"];
            var rate = q.GetACARate();
            var code = q.GetACACode(year); 
            var company = q.GetCompanyDetail(year);
            var empldetail = q.GetPayrollEmp(year);
            Generate1095Cpdf(year, code, rate, company, empldetail, configuration); 
        }
        private void Generate1095Cpdf(string year, List<ACACode> code, ACARate rate, List<ACACompanyDetail> company, List<ACAEmployeeDetail> empldetail, IConfiguration configuration)
        {
            foreach (var em in empldetail)
            {
                var aca = f.Get1095CValues(em.EmployeeId, code, rate, company, year);
                fill1095Cform(aca, em.EmployeeId, configuration);
            }
        }

          private void fill1095Cform(ACA1095C aca, string payroll, IConfiguration configuration)
        {
             try
             {
                string pdfTemplate = configuration["Project:1095Template"];
                string newFile = configuration["Project:filegen"] + payroll + "_" + aca.FName + "_" + aca.LName + "_f1095c.pdf";

                PdfReader pdfReader = new PdfReader(pdfTemplate);
                PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(
                            newFile, FileMode.Create));

                AcroFields pdfFormFields = pdfStamper.AcroFields;

                // set form pdfFormFields form 1 - 13

                // details about employee  

                pdfFormFields.SetField("topmostSubform[0].Page1[0].EmployeeName[0].EmployeeName[0].f1_1[0]", aca.FName);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].EmployeeName[0].EmployeeName[0].f1_3[0]", aca.LName);


                pdfFormFields.SetField("topmostSubform[0].Page1[0].EmployeeName[0].f1_4[0]", Helper.Formatss(aca.ssn));//.Insert(5, "-").Insert(3, "-")
                pdfFormFields.SetField("topmostSubform[0].Page1[0].EmployeeName[0].f1_5[0]", aca.address);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].EmployeeName[0].f1_6[0]", aca.city);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].EmployeeName[0].f1_7[0]", aca.state);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].EmployeeName[0].f1_8[0]", aca.country + "," + aca.zipcode);

                //details about company

                pdfFormFields.SetField("topmostSubform[0].Page1[0].EmployerIssuer[0].f1_9[0]", aca.Company);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].EmployerIssuer[0].f1_10[0]", Helper.FormatEin(aca.ein));
                pdfFormFields.SetField("topmostSubform[0].Page1[0].EmployerIssuer[0].f1_11[0]", aca.companyaddress);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].EmployerIssuer[0].f1_12[0]", Helper.FormatPhone(aca.companyphone));
                pdfFormFields.SetField("topmostSubform[0].Page1[0].EmployerIssuer[0].f1_13[0]", aca.companycity);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].EmployerIssuer[0].f1_14[0]", aca.companystate);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].EmployerIssuer[0].f1_15[0]", aca.companycountry + "," + aca.companyzipcode);

                //plan start month
                pdfFormFields.SetField("topmostSubform[0].Page1[0].PartII[0].f1_16[0]", "06");

                // form 14 - 16
                //14
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row1[0].f1_17[0]", aca.form14.all12);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row1[0].f1_18[0]", aca.form14.jan);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row1[0].f1_19[0]", aca.form14.feb);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row1[0].f1_20[0]", aca.form14.mar);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row1[0].f1_21[0]", aca.form14.apr);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row1[0].f1_22[0]", aca.form14.may);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row1[0].f1_23[0]", aca.form14.jun);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row1[0].f1_24[0]", aca.form14.jul);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row1[0].f1_25[0]", aca.form14.aug);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row1[0].f1_26[0]", aca.form14.sep);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row1[0].f1_27[0]", aca.form14.oct);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row1[0].f1_28[0]", aca.form14.nov);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row1[0].f1_29[0]", aca.form14.dec);

                //15
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row2[0].f1_30[0]", aca.form15.all12);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row2[0].f1_31[0]", aca.form15.jan);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row2[0].f1_32[0]", aca.form15.feb);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row2[0].f1_33[0]", aca.form15.mar);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row2[0].f1_34[0]", aca.form15.apr);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row2[0].f1_35[0]", aca.form15.may);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row2[0].f1_36[0]", aca.form15.jun);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row2[0].f1_37[0]", aca.form15.jul);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row2[0].f1_38[0]", aca.form15.aug);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row2[0].f1_39[0]", aca.form15.sep);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row2[0].f1_40[0]", aca.form15.oct);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row2[0].f1_41[0]", aca.form15.nov);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row2[0].f1_42[0]", aca.form15.dec);

                //16
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row3[0].f1_43[0]", aca.form16.all12);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row3[0].f1_44[0]", aca.form16.jan);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row3[0].f1_45[0]", aca.form16.feb);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row3[0].f1_46[0]", aca.form16.mar);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row3[0].f1_47[0]", aca.form16.apr);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row3[0].f1_48[0]", aca.form16.may);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row3[0].f1_49[0]", aca.form16.jun);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row3[0].f1_50[0]", aca.form16.jul);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row3[0].f1_51[0]", aca.form16.aug);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row3[0].f1_52[0]", aca.form16.sep);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row3[0].f1_53[0]", aca.form16.oct);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row3[0].f1_54[0]", aca.form16.nov);
                pdfFormFields.SetField("topmostSubform[0].Page1[0].Table1[0].Row3[0].f1_55[0]", aca.form16.dec);
                 

                if (aca.formpart3.iscovered == "Y")
                { 
                    pdfFormFields.SetField("topmostSubform[0].Page1[0].PartIII[0].c1_2[0]", "1"); //part 3 covered individual checkbox
                     
                    pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row1[0].f1_56[0]", aca.FName);
                    pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row1[0].f1_58[0]", aca.LName);

                    pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row1[0].f1_59[0]", Helper.Formatss(aca.ssn));//.Insert(5, "-").Insert(3, "-")
                    pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row1[0].f1_60[0]", "");
                    pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row1[0].c1_3[0]", aca.formpart3.all12);
                    pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row1[0].c1_4[0]", aca.formpart3.jan);
                    pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row1[0].c1_5[0]", aca.formpart3.feb);
                    pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row1[0].c1_6[0]", aca.formpart3.mar);
                    pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row1[0].c1_7[0]", aca.formpart3.apr);
                    pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row1[0].c1_8[0]", aca.formpart3.may);
                    pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row1[0].c1_9[0]", aca.formpart3.jun);
                    pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row1[0].c1_10[0]", aca.formpart3.jul);
                    pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row1[0].c1_11[0]", aca.formpart3.aug);
                    pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row1[0].c1_12[0]", aca.formpart3.sep);
                    pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row1[0].c1_13[0]", aca.formpart3.oct);
                    pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row1[0].c1_14[0]", aca.formpart3.nov);
                    pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row1[0].c1_15[0]", aca.formpart3.dec);


                    ///
                }

               
                var i = 1;
                var j = 60;
                var k = 15;
                var deptn = aca.dependant;
                if (deptn.Count <= 5)
                {
                    foreach (var dep in aca.dependant)
                    {
                        i = i + 1;
                        j = j + 1;
                        k = k + 1; 

                        var depname = dep.name.Split(',');

                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].f1_" + j + "[0]", depname[1].Replace(",", ""));
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].f1_" + (j + 2) + "[0]", depname[0]);
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].f1_" + (j + 3) + "[0]", "");
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].f1_" + (j + 4) + "[0]", dep.dob);

                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].c1_" +  k + "[0]", dep.form17de.all12);
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].c1_" + (k + 1) + "[0]", dep.form17de.jan);
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].c1_" + (k + 2) + "[0]", dep.form17de.feb);
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].c1_" + (k + 3) + "[0]", dep.form17de.mar);
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].c1_" + (k + 4) + "[0]", dep.form17de.apr);
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].c1_" + (k + 5) + "[0]", dep.form17de.may);
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].c1_" + (k + 6) + "[0]", dep.form17de.jun);
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].c1_" + (k + 7) + "[0]", dep.form17de.jul);
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].c1_" + (k + 8) + "[0]", dep.form17de.aug);
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].c1_" + (k + 9) + "[0]", dep.form17de.sep);
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].c1_" + (k + 10) + "[0]", dep.form17de.oct);
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].c1_" + (k + 11) + "[0]", dep.form17de.nov);
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].c1_" + (k + 12) + "[0]", dep.form17de.dec);
                        k = k + 12;
                        j = j + 4;
                    }
                   
                }
                else
                {
                    //part 3 going to page 3 in the pdf 

                    var d1 = (from t in deptn
                              select t).Take(5);

                    var d2 = (from t in deptn
                              select t).Skip(5);
                     
                    foreach (var dep in d1)
                    {
                        i = i + 1;
                        j = j + 1;
                        k = k + 1; 
                         
                       //dependent detail 
                        var depname = dep.name.Split(',');

                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].f1_" + j + "[0]", depname[1].Replace(",", ""));
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].f1_" + (j + 2) + "[0]", depname[0]);
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].f1_" + (j + 3) + "[0]", "");
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].f1_" + (j + 4) + "[0]", dep.dob);

                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].c1_" + k + "[0]", dep.form17de.all12);
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].c1_" + (k + 1) + "[0]", dep.form17de.jan);
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].c1_" + (k + 2) + "[0]", dep.form17de.feb);
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].c1_" + (k + 3) + "[0]", dep.form17de.mar);
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].c1_" + (k + 4) + "[0]", dep.form17de.apr);
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].c1_" + (k + 5) + "[0]", dep.form17de.may);
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].c1_" + (k + 6) + "[0]", dep.form17de.jun);
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].c1_" + (k + 7) + "[0]", dep.form17de.jul);
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].c1_" + (k + 8) + "[0]", dep.form17de.aug);
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].c1_" + (k + 9) + "[0]", dep.form17de.sep);
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].c1_" + (k + 10) + "[0]", dep.form17de.oct);
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].c1_" + (k + 11) + "[0]", dep.form17de.nov);
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Table2[0].Row" + i + "[0].c1_" + (k + 12) + "[0]", dep.form17de.dec);
                        k = k + 12;
                        j = j + 4;
                    }
                     

                    //reseting the values
                    i = 0;
                    j = 4;
                    k = 0;

                    //Employee detail
                    pdfFormFields.SetField("topmostSubform[0].Page3[0].f3_1[0]", aca.FName);
                    pdfFormFields.SetField("topmostSubform[0].Page3[0].f3_3[0]", aca.LName);
                    pdfFormFields.SetField("topmostSubform[0].Page3[0].f3_4[0]", Helper.Formatss(aca.ssn));

                    foreach (var dep in d2)
                    {
                        //dependent detail 
                        var depname = dep.name.Split(',');

                        pdfFormFields.SetField("topmostSubform[0].Page3[0].Table1[0].Row" + i + "[0].f3_" + j + "[0]", depname[1].Replace(",", ""));
                        pdfFormFields.SetField("topmostSubform[0].Page3[0].Table1[0].Row" + i + "[0].f3_" + (j + 2) + "[0]", depname[0]);
                        pdfFormFields.SetField("topmostSubform[0].Page3[0].Table1[0].Row" + i + "[0].f3_" + (j + 3) + "[0]", "");
                        pdfFormFields.SetField("topmostSubform[0].Page3[0].Table1[0].Row" + i + "[0].f3_" + (j + 4) + "[0]", dep.dob);


                        pdfFormFields.SetField("topmostSubform[0].Page3[0].Table1[0].Row" + i + "[0].c3_" + k + "[0]", dep.form17de.all12);
                        pdfFormFields.SetField("topmostSubform[0].Page3[0].Table1[0].Row" + i + "[0].c3_" + (k + 1) + "[0]", dep.form17de.jan);
                        pdfFormFields.SetField("topmostSubform[0].Page3[0].Table1[0].Row" + i + "[0].c3_" + (k + 2) + "[0]", dep.form17de.feb);
                        pdfFormFields.SetField("topmostSubform[0].Page3[0].Table1[0].Row" + i + "[0].c3_" + (k + 3) + "[0]", dep.form17de.mar);
                        pdfFormFields.SetField("topmostSubform[0].Page3[0].Table1[0].Row" + i + "[0].c3_" + (k + 4) + "[0]", dep.form17de.apr);
                        pdfFormFields.SetField("topmostSubform[0].Page3[0].Table1[0].Row" + i + "[0].c3_" + (k + 5) + "[0]", dep.form17de.may);
                        pdfFormFields.SetField("topmostSubform[0].Page3[0].Table1[0].Row" + i + "[0].c3_" + (k + 6) + "[0]", dep.form17de.jun);
                        pdfFormFields.SetField("topmostSubform[0].Page3[0].Table1[0].Row" + i + "[0].c3_" + (k + 7) + "[0]", dep.form17de.jul);
                        pdfFormFields.SetField("topmostSubform[0].Page3[0].Table1[0].Row" + i + "[0].c3_" + (k + 8) + "[0]", dep.form17de.aug);
                        pdfFormFields.SetField("topmostSubform[0].Page3[0].Table1[0].Row" + i + "[0].c3_" + (k + 9) + "[0]", dep.form17de.sep);
                        pdfFormFields.SetField("topmostSubform[0].Page3[0].Table1[0].Row" + i + "[0].c3_" + (k + 10) + "[0]", dep.form17de.oct);
                        pdfFormFields.SetField("topmostSubform[0].Page3[0].Table1[0].Row" + i + "[0].c3_" + (k + 11) + "[0]", dep.form17de.nov);
                        pdfFormFields.SetField("topmostSubform[0].Page3[0].Table1[0].Row" + i + "[0].c3_" + (k + 12) + "[0]", dep.form17de.dec);
                        k = k + 12;
                        j = j + 4;
                    }


                }
                 
                // flatten the form to remove editting options, set it to false
                // to leave the form open to subsequent manual edits
                pdfStamper.FormFlattening = true;

                // close the pdf
                pdfStamper.Close();
             }
             catch (Exception ex)
             {
                Console.ReadLine();
            }
        }

    }
}