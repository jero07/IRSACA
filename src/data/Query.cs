using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using IRSACA.src;

namespace IRSACA {

    public class Query {
        public ACARate GetACARate () {
            StringBuilder sb = new StringBuilder ();
            ACARate r = new ACARate ();
            sb.AppendLine (" SELECT SINGLE1,SINGLE2,YEAR from aca_rate ");
            DataTable dt = DataManager.GetData (sb.ToString (), DataConfig.GetDataConnection (DataConfig.aca_conn));
            foreach (DataRow dr in dt.Rows) {
                r.Single1 = dr["SINGLE1"].ToString ();
                r.Single2 = dr["SINGLE2"].ToString ();
            }
            return r;
        }

        public List<ACACode> GetACACode (string year) {
            StringBuilder sb = new StringBuilder ();
            List<ACACode> codels = new List<ACACode> ();
            sb.AppendLine (" select * from HR.ACA_1095CCODE where formyear='" + year + "'");
            DataTable dt = DataManager.GetData (sb.ToString (), DataConfig.GetDataConnection (DataConfig.aca_conn));
            foreach (DataRow dr in dt.Rows) {
                ACACode code = new ACACode ();
                code.code = dr["code"].ToString ();
                code.form14 = dr["form14"].ToString ();
                code.form15 = dr["form15"].ToString ();
                code.form16 = dr["form16"].ToString ();
                code.employeetype = dr["employeetype"].ToString ().ToLower ();
                codels.Add (code);
            }

            return codels;
        }

        public List<ACACompanyDetail> GetCompanyDetail (string year) {
            StringBuilder sb = new StringBuilder ();

            List<ACACompanyDetail> detaills = new List<ACACompanyDetail> ();
            sb.AppendLine (" select * from  HR.ACA_COMPANY_DETAIL where Year='" + year + "'");
            DataTable dt = DataManager.GetData (sb.ToString (), DataConfig.GetDataConnection (DataConfig.aca_conn));
            foreach (DataRow dr in dt.Rows) {
                ACACompanyDetail det = new ACACompanyDetail ();
                det.Company = dr["COMPANY_NAME"].ToString ();
                det.ein = dr["EIN"].ToString ();
                det.CompanyCode = dr["COMPANY_CODE"].ToString ();
                det.Contact = dr["CONTACT_FNAME"].ToString () + " " + dr["CONTACT_LNAME"].ToString ();
                det.phone = dr["PHONE_NUMBER"].ToString ();
                det.address = dr["ADDRESS1"].ToString () + "," + dr["ADDRESS2"].ToString ();
                det.city = dr["CITY"].ToString ();
                det.state = dr["STATE"].ToString ();
                det.zipcode = dr["ZIPCODE"].ToString ();
                det.country = "USA";
                det.address1 = dr["ADDRESS1"].ToString ();
                det.address2 = dr["ADDRESS2"].ToString ();
                det.ContactFName = dr["CONTACT_FNAME"].ToString ();
                det.ContactLName = dr["CONTACT_LNAME"].ToString ();
                detaills.Add (det);
            }

            return detaills;
        }
        public List<ACA1095rows> Get1095data (string year, string payroll, bool iscobra) {
            StringBuilder sb = new StringBuilder ();
            List<ACA1095rows> acals = new List<ACA1095rows> ();

            sb.AppendLine (" SELECT PAYROLLID, NAME,  COMPANY,  RELATIONSHIP,  EMPLOYEETYPE,  BIRTHDATE,  FORMYEAR,  JAN,  FEB,  MAR,  APR,  ");
            sb.AppendLine (" MAY,  JUN,  JUL,  AUG,  SEP,  OCT,  NOV,  DEC , ");
            sb.AppendLine (" Fileno ");
            sb.AppendLine (" FROM HR.ACA_1095C  where formyear = '" + year + "' and payrollid = '" + payroll + "'");

            if (iscobra) {
                sb.AppendLine (" and fileno is not null ");
            } else {
                sb.AppendLine (" and fileno is null ");
            }
            DataTable dt = DataManager.GetData (sb.ToString (), DataConfig.aca_conn);
            foreach (DataRow dr in dt.Rows) {
                ACA1095rows aca = new ACA1095rows ();
                aca.formdata = new ACA1095Cmonths ();
                aca.payroll = dr["PAYROLLID"].ToString ();
                aca.Name = dr["NAME"].ToString ();
                aca.Company = dr["COMPANY"].ToString ();
                aca.relation = dr["RELATIONSHIP"].ToString ();
                aca.empltype = dr["EMPLOYEETYPE"].ToString ().ToLower ();
                aca.bday = dr["BIRTHDATE"].ToString ();
                aca.year = dr["FORMYEAR"].ToString ();
                aca.formdata.jan = dr["JAN"].ToString ();
                aca.formdata.feb = dr["FEB"].ToString ();
                aca.formdata.mar = dr["MAR"].ToString ();
                aca.formdata.apr = dr["APR"].ToString ();
                aca.formdata.may = dr["MAY"].ToString ();
                aca.formdata.jun = dr["JUN"].ToString ();
                aca.formdata.jul = dr["JUL"].ToString ();
                aca.formdata.aug = dr["AUG"].ToString ();
                aca.formdata.sep = dr["SEP"].ToString ();
                aca.formdata.oct = dr["OCT"].ToString ();
                aca.formdata.nov = dr["NOV"].ToString ();
                aca.formdata.dec = dr["DEC"].ToString ();
                aca.Fileno = dr["Fileno"].ToString ();
                acals.Add (aca);
            }

            return acals;
        }

        public List<ACAEmployeeDetail> GetPayrollEmp(){
            return null;
        }
    }
}