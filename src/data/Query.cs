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
            sb.AppendLine (" select * from ACA_CODE where formyear='" + year + "'");
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
            sb.AppendLine (" select * from ACA_COMPANY_DETAIL where formyear='" + year + "'");
            DataTable dt = DataManager.GetData(sb.ToString (), DataConfig.GetDataConnection (DataConfig.aca_conn));
            foreach (DataRow dr in dt.Rows) {
                ACACompanyDetail det = new ACACompanyDetail ();
                det.ein = dr["EIN"].ToString ();
                det.address1 = dr["ADDRESS1"].ToString();
                det.address2 = dr["ADDRESS2"].ToString();
                det.ContactFName = dr["CONTACT_FNAME"].ToString();
                det.ContactLName = dr["CONTACT_LNAME"].ToString();
                det.Company = dr["COMPANY_NAME"].ToString();
                det.Contact = dr["CONTACT_FNAME"].ToString () + " " + dr["CONTACT_LNAME"].ToString ();
                det.phone = dr["PHONE_NUMBER"].ToString ();
                det.address = dr["ADDRESS1"].ToString () + "," + dr["ADDRESS2"].ToString ();
                det.city = dr["CITY"].ToString ();
                det.state = dr["STATE"].ToString ();
                det.zipcode = dr["ZIPCODE"].ToString ();
                det.country = "USA";
                detaills.Add (det);
            }

            return detaills;
        }
       
        public ACAEmployeeDetail GetEmployeeDetail(string employeeId, string year)
        {
            StringBuilder sb = new StringBuilder(); 
            ACAEmployeeDetail det = new ACAEmployeeDetail();
            sb.AppendLine(" select * FROM ACA_EMPLOYEEDETAIL where "); 
            sb.AppendLine("  formyear='" + year + "'");
            sb.AppendLine(" and empId='" + employeeId + "'");
            DataTable dt = DataManager.GetData(sb.ToString(), DataConfig.GetDataConnection(DataConfig.aca_conn));
            foreach (DataRow dr in dt.Rows)
            {
                det.EmployeeId = dr["empId"].ToString();
                det.Fname = dr["firstname"].ToString();
                det.Lname = dr["lastname"].ToString();
                det.Name = dr["firstname"].ToString() + " " + dr["lastname"].ToString();
                det.Employeetype = dr["employeetype"].ToString();
                det.ssn = dr["ss"].ToString();  
                det.address = dr["address1"].ToString() + dr["address2"].ToString();
                det.city = dr["city"].ToString().Replace("-", " ");
                det.state = dr["state"].ToString();
                det.zipcode = dr["zipcode"].ToString();
                det.country = dr["country"].ToString();
            }

            return det;
        }

        public List<ACAEmployeeDetail> GetPayrollEmp(string year)
        {
            StringBuilder sb = new StringBuilder();
            List<ACAEmployeeDetail> detls = new List<ACAEmployeeDetail>();
            sb.AppendLine(" select * FROM ACA_1095C where formyear='" + year + "' ");
            sb.AppendLine(" and relationship ='E' order by empId");
            DataTable dt = DataManager.GetData(sb.ToString(), DataConfig.GetDataConnection(DataConfig.aca_conn));
            foreach (DataRow dr in dt.Rows)
            {
                ACAEmployeeDetail det = new ACAEmployeeDetail();
                det.EmployeeId = dr["EMPID"].ToString();
                detls.Add(det);
            }
            return detls;
        } 
        public List<ACA1095rows> Get1095data(string year, string employeeId)
        {
            StringBuilder sb = new StringBuilder();
            List<ACA1095rows> acals = new List<ACA1095rows>();

            sb.AppendLine(" SELECT EMPID, NAME,  RELATIONSHIP,  EMPLOYEETYPE,  BIRTHDATE,  FORMYEAR,  JAN,  FEB,  MAR,  APR,  ");
            sb.AppendLine(" MAY,  JUN,  JUL,  AUG,  SEP,  OCT,  NOV,  DEC  "); 
            sb.AppendLine(" FROM ACA_1095C where formyear = '" + year + "' and empId = '" + employeeId + "'");
             
            DataTable dt = DataManager.GetData(sb.ToString(), DataConfig.GetDataConnection(DataConfig.aca_conn));
            foreach (DataRow dr in dt.Rows)
            {
                ACA1095rows aca = new ACA1095rows();
                aca.formdata = new ACA1095Cmonths();
                aca.EmployeeId = dr["EMPID"].ToString();
                aca.Name = dr["NAME"].ToString();
                aca.relation = dr["RELATIONSHIP"].ToString();
                aca.empltype = dr["EMPLOYEETYPE"].ToString().ToLower();  
                aca.bday = dr["BIRTHDATE"].ToString();
                aca.year = dr["FORMYEAR"].ToString();
                aca.formdata.jan = dr["JAN"].ToString();
                aca.formdata.feb = dr["FEB"].ToString();
                aca.formdata.mar = dr["MAR"].ToString();
                aca.formdata.apr = dr["APR"].ToString();
                aca.formdata.may = dr["MAY"].ToString();
                aca.formdata.jun = dr["JUN"].ToString();
                aca.formdata.jul = dr["JUL"].ToString();
                aca.formdata.aug = dr["AUG"].ToString();
                aca.formdata.sep = dr["SEP"].ToString();
                aca.formdata.oct = dr["OCT"].ToString();
                aca.formdata.nov = dr["NOV"].ToString();
                aca.formdata.dec = dr["DEC"].ToString();
                acals.Add(aca);
            }

            return acals;
        }

        public ACA1094Count f1094CEmployeeCount(string year)
        {
            StringBuilder sb = new StringBuilder();
            ACA1094Count cnt = new ACA1094Count();
            sb.AppendLine(" SELECT ");
            sb.AppendLine(" (SELECT COUNT(payrollid)  FROM ACA_1095C  WHERE relationship in ('E','B') and formyear ='" + year + "') total ,");

            sb.AppendLine(" (SELECT COUNT(PAYROLLID) FROM ACA_1095C  WHERE relationship='E' AND jan  IN ('C','W')  and formyear ='" + year + "') janft, ");
            sb.AppendLine(" (SELECT nvl(sum(cnt),0) cnt from ( SELECT CASE WHEN mn = currmon  THEN  case when dt = '01' then 1  else 0 end  ELSE 1 END cnt  "); //date dt=01 
            sb.AppendLine(" FROM (SELECT TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'dd') dt,  TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'mm') mn, '01' AS currmon ");
            sb.AppendLine(" FROM ACA_1095C WHERE relationship='E' AND jan IN ('D') and formyear ='" + year + "' ) ");

            sb.AppendLine(" union all SELECT CASE WHEN mn = currmon  THEN  case when dt = '01' then 1  else 0 end  ELSE 1 END cnt  ");
            sb.AppendLine(" FROM (SELECT TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'dd') dt,  TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'mm') mn, '01' AS currmon ");
            sb.AppendLine(" FROM ACA_1095C WHERE relationship='E' AND jan IN ('HS') and formyear ='" + year + "' ) ");


            sb.AppendLine(" ) ) jannh , ");

            sb.AppendLine(" (SELECT COUNT(PAYROLLID) FROM ACA_1095C  WHERE relationship='E' AND feb  IN ('C','W') and formyear ='" + year + "' ) febft, ");
            sb.AppendLine(" (SELECT nvl(sum(cnt),0) cnt from ( SELECT CASE WHEN mn = currmon  THEN  case when dt = '01' then 1  else 0 end  ELSE 1 END cnt  ");//date dt=01 
            sb.AppendLine(" FROM (SELECT TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'dd') dt,  TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'mm') mn, '02' AS currmon ");
            sb.AppendLine(" FROM ACA_1095C WHERE relationship='E' AND feb IN ('D') and formyear ='" + year + "' ) ");


            sb.AppendLine(" union all SELECT CASE WHEN mn = currmon  THEN  case when dt = '01' then 1  else 0 end  ELSE 1 END cnt  ");
            sb.AppendLine(" FROM (SELECT TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'dd') dt,  TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'mm') mn, '02' AS currmon ");
            sb.AppendLine(" FROM ACA_1095C WHERE relationship='E' AND feb IN ('HS') and formyear ='" + year + "' ) ");


            sb.AppendLine(" ) ) febnh , ");

            sb.AppendLine(" (SELECT COUNT(PAYROLLID) FROM ACA_1095C  WHERE relationship='E' AND mar IN ('C','W') and formyear ='" + year + "' ) marft, ");
            sb.AppendLine(" (SELECT nvl(sum(cnt),0) cnt from ( SELECT CASE WHEN mn = currmon  THEN  case when dt = '01' then 1  else 0 end  ELSE 1 END cnt  ");
            sb.AppendLine(" FROM (SELECT TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'dd') dt,  TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'mm') mn, '03' AS currmon ");
            sb.AppendLine(" FROM ACA_1095C WHERE relationship='E' AND mar IN ('D') and formyear ='" + year + "'  )  ");


            sb.AppendLine(" union all SELECT CASE WHEN mn = currmon  THEN  case when dt = '01' then 1  else 0 end  ELSE 1 END cnt  ");
            sb.AppendLine(" FROM (SELECT TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'dd') dt,  TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'mm') mn, '03' AS currmon ");
            sb.AppendLine(" FROM ACA_1095C WHERE relationship='E' AND mar IN ('HS') and formyear ='" + year + "'  )  ");


            sb.AppendLine(" ) ) marnh , ");

            sb.AppendLine(" (SELECT COUNT(PAYROLLID) FROM ACA_1095C  WHERE relationship='E' AND apr IN ('C','W') and formyear ='" + year + "' ) aprft, ");
            sb.AppendLine(" (SELECT nvl(sum(cnt),0) cnt from ( SELECT CASE WHEN mn = currmon  THEN  case when dt = '01' then 1  else 0 end  ELSE 1 END cnt  ");
            sb.AppendLine(" FROM (SELECT TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'dd') dt,  TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'mm') mn, '04' AS currmon ");
            sb.AppendLine(" FROM ACA_1095C WHERE relationship='E' AND apr IN ('D') and formyear ='" + year + "' )  ");


            sb.AppendLine(" union all SELECT CASE WHEN mn = currmon  THEN  case when dt = '01' then 1  else 0 end  ELSE 1 END cnt  ");
            sb.AppendLine(" FROM (SELECT TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'dd') dt,  TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'mm') mn, '04' AS currmon ");
            sb.AppendLine(" FROM ACA_1095C WHERE relationship='E' AND apr IN ('HS') and formyear ='" + year + "' )  ");

            sb.AppendLine(" ) ) aprnh , ");

            sb.AppendLine(" (SELECT COUNT(PAYROLLID) FROM ACA_1095C  WHERE relationship='E' AND may IN ('C','W') and formyear ='" + year + "' ) mayft, ");
            sb.AppendLine(" (SELECT nvl(sum(cnt),0) cnt from ( SELECT CASE WHEN mn = currmon  THEN  case when dt = '01' then 1  else 0 end  ELSE 1 END cnt  ");
            sb.AppendLine(" FROM (SELECT TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'dd') dt,  TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'mm') mn, '05' AS currmon ");
            sb.AppendLine(" FROM ACA_1095C WHERE relationship='E' AND may IN ('D') and formyear ='" + year + "' )  ");

            sb.AppendLine(" union all SELECT CASE WHEN mn = currmon  THEN  case when dt = '01' then 1  else 0 end  ELSE 1 END cnt  ");
            sb.AppendLine(" FROM (SELECT TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'dd') dt,  TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'mm') mn, '05' AS currmon ");
            sb.AppendLine(" FROM ACA_1095C WHERE relationship='E' AND may IN ('HS') and formyear ='" + year + "' )  ");

            sb.AppendLine(" ) ) maynh , ");

            sb.AppendLine(" (SELECT COUNT(PAYROLLID) FROM ACA_1095C  WHERE relationship='E' AND jun IN ('C','W') and formyear ='" + year + "' ) junft, ");
            sb.AppendLine(" (SELECT nvl(sum(cnt),0) cnt from ( SELECT CASE WHEN mn = currmon  THEN  case when dt = '01' then 1  else 0 end  ELSE 1 END cnt  ");
            sb.AppendLine(" FROM (SELECT TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'dd') dt,  TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'mm') mn, '06' AS currmon ");
            sb.AppendLine(" FROM ACA_1095C WHERE relationship='E' AND jun IN ('D') and formyear ='" + year + "' )  ");


            sb.AppendLine(" union all SELECT CASE WHEN mn = currmon  THEN  case when dt = '01' then 1  else 0 end  ELSE 1 END cnt  ");
            sb.AppendLine(" FROM (SELECT TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'dd') dt,  TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'mm') mn, '06' AS currmon ");
            sb.AppendLine(" FROM ACA_1095C WHERE relationship='E' AND jun IN ('HS') and formyear ='" + year + "' )  ");

            sb.AppendLine(" ) ) junnh , ");

            sb.AppendLine(" (SELECT COUNT(PAYROLLID) FROM ACA_1095C  WHERE relationship='E' AND jul IN ('C','W') and formyear ='" + year + "' ) julft, ");
            sb.AppendLine(" (SELECT nvl(sum(cnt),0) cnt from ( SELECT CASE WHEN mn = currmon  THEN  case when dt = '01' then 1  else 0 end  ELSE 1 END cnt  ");
            sb.AppendLine(" FROM (SELECT TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'dd') dt,  TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'mm') mn, '07' AS currmon ");
            sb.AppendLine(" FROM ACA_1095C WHERE relationship='E' AND jul IN ('D') and formyear ='" + year + "' ) ");

            sb.AppendLine(" union all SELECT CASE WHEN mn = currmon  THEN  case when dt = '01' then 1  else 0 end  ELSE 1 END cnt  ");
            sb.AppendLine(" FROM (SELECT TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'dd') dt,  TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'mm') mn, '07' AS currmon ");
            sb.AppendLine(" FROM ACA_1095C WHERE relationship='E' AND jul IN ('HS') and formyear ='" + year + "' ) ");

            sb.AppendLine(" ) ) julnh , ");

            sb.AppendLine(" (SELECT COUNT(PAYROLLID) FROM ACA_1095C  WHERE relationship='E' AND aug IN ('C','W') and formyear ='" + year + "' ) augft, ");
            sb.AppendLine(" (SELECT nvl(sum(cnt),0) cnt from ( SELECT CASE WHEN mn = currmon  THEN  case when dt = '01' then 1  else 0 end  ELSE 1 END cnt  ");
            sb.AppendLine(" FROM (SELECT TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'dd') dt,  TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'mm') mn, '08' AS currmon ");
            sb.AppendLine(" FROM ACA_1095C WHERE relationship='E' AND aug IN ('D') and formyear ='" + year + "' ) ");

            sb.AppendLine(" union all SELECT CASE WHEN mn = currmon  THEN  case when dt = '01' then 1  else 0 end  ELSE 1 END cnt  ");
            sb.AppendLine(" FROM (SELECT TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'dd') dt,  TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'mm') mn, '08' AS currmon ");
            sb.AppendLine(" FROM ACA_1095C WHERE relationship='E' AND aug IN ('HS') and formyear ='" + year + "' ) ");

            sb.AppendLine(" ) ) augnh , ");

            sb.AppendLine(" (SELECT COUNT(PAYROLLID) FROM ACA_1095C WHERE relationship='E' AND sep IN ('C','W') and formyear ='" + year + "' ) sepft, ");
            sb.AppendLine(" (SELECT nvl(sum(cnt),0) cnt from ( SELECT CASE WHEN mn = currmon  THEN  case when dt = '01' then 1  else 0 end  ELSE 1 END cnt  ");
            sb.AppendLine(" FROM (SELECT TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'dd') dt,  TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'mm') mn, '09' AS currmon ");
            sb.AppendLine(" FROM ACA_1095C WHERE relationship='E' AND sep IN ('D') and formyear ='" + year + "' ) ");

            sb.AppendLine(" union all SELECT CASE WHEN mn = currmon  THEN  case when dt = '01' then 1  else 0 end  ELSE 1 END cnt  ");
            sb.AppendLine(" FROM (SELECT TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'dd') dt,  TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'mm') mn, '09' AS currmon ");
            sb.AppendLine(" FROM ACA_1095C WHERE relationship='E' AND sep IN ('HS') and formyear ='" + year + "' ) ");

            sb.AppendLine(" ) ) sepnh , ");

            sb.AppendLine(" (SELECT COUNT(PAYROLLID) FROM ACA_1095C WHERE relationship='E' AND oct IN ('C','W') and formyear ='" + year + "' ) octft, ");
            sb.AppendLine(" (SELECT nvl(sum(cnt),0) cnt from ( SELECT CASE WHEN mn = currmon  THEN  case when dt = '01' then 1  else 0 end  ELSE 1 END cnt  ");
            sb.AppendLine(" FROM (SELECT TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'dd') dt,  TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'mm') mn, '10' AS currmon ");
            sb.AppendLine(" FROM ACA_1095C WHERE relationship='E' AND oct IN ('D') and formyear ='" + year + "' )  ");

            sb.AppendLine(" union all SELECT CASE WHEN mn = currmon  THEN  case when dt = '01' then 1  else 0 end  ELSE 1 END cnt  ");
            sb.AppendLine(" FROM (SELECT TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'dd') dt,  TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'mm') mn, '10' AS currmon ");
            sb.AppendLine(" FROM ACA_1095C WHERE relationship='E' AND oct IN ('HS') and formyear ='" + year + "' )  ");

            sb.AppendLine(" ) ) octnh , ");

            sb.AppendLine(" (SELECT COUNT(PAYROLLID) FROM ACA_1095C WHERE relationship='E' AND nov IN ('C','W') and formyear ='" + year + "' ) novft, ");
            sb.AppendLine(" (SELECT nvl(sum(cnt),0) cnt from ( SELECT CASE WHEN mn = currmon  THEN  case when dt = '01' then 1  else 0 end  ELSE 1 END cnt  ");
            sb.AppendLine(" FROM (SELECT TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'dd') dt,  TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'mm') mn, '11' AS currmon ");
            sb.AppendLine(" FROM ACA_1095C WHERE relationship='E' AND nov IN ('D') and formyear ='" + year + "' ) ");

            sb.AppendLine(" union all SELECT CASE WHEN mn = currmon  THEN  case when dt = '01' then 1  else 0 end  ELSE 1 END cnt  ");
            sb.AppendLine(" FROM (SELECT TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'dd') dt,  TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'mm') mn, '11' AS currmon ");
            sb.AppendLine(" FROM ACA_1095C WHERE relationship='E' AND nov IN ('HS') and formyear ='" + year + "' ) ");

            sb.AppendLine(" ) ) novnh , ");

            sb.AppendLine(" (SELECT COUNT(PAYROLLID) FROM ACA_1095C  WHERE relationship='E' AND dec IN ('C','W') and formyear ='" + year + "' ) decft, ");
            sb.AppendLine(" (SELECT nvl(sum(cnt),0) cnt from ( SELECT CASE WHEN mn = currmon  THEN  case when dt = '01' then 1  else 0 end  ELSE 1 END cnt  ");
            sb.AppendLine(" FROM (SELECT TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'dd') dt,  TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'mm') mn, '12' AS currmon ");
            sb.AppendLine(" FROM ACA_1095C WHERE relationship='E' AND dec IN ('D') and formyear ='" + year + "' ) ");

            sb.AppendLine(" union all  SELECT CASE WHEN mn = currmon THEN case when dt = '01' then 1  else 0 end  ELSE 1 END cnt  ");
            sb.AppendLine(" FROM (SELECT TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'dd') dt, TO_CHAR(to_date(hire_date,'mm/dd/yyyy'),'mm') mn, '12' AS currmon ");
            sb.AppendLine(" FROM ACA_1095C WHERE relationship='E' AND dec IN ('HS') and formyear ='" + year + "' ) ");

            sb.AppendLine(" ) ) decnh  ");

            sb.AppendLine(" FROM dual ");
            DataTable dt = DataManager.GetData(sb.ToString(), DataConfig.GetDataConnection(DataConfig.aca_conn)); 
            foreach (DataRow dr in dt.Rows)
            {
                cnt.total = dr["total"].ToString();
                cnt.janft = dr["janft"].ToString();
                cnt.febft = dr["febft"].ToString();
                cnt.marft = dr["marft"].ToString();
                cnt.aprft = dr["aprft"].ToString();
                cnt.mayft = dr["mayft"].ToString();
                cnt.junft = dr["junft"].ToString();
                cnt.julft = dr["julft"].ToString();
                cnt.augft = dr["augft"].ToString();
                cnt.sepft = dr["sepft"].ToString();
                cnt.octft = dr["octft"].ToString();
                cnt.novft = dr["novft"].ToString();
                cnt.decft = dr["decft"].ToString();

                cnt.janall = Convert.ToString(Convert.ToInt32(dr["janft"]) + Convert.ToInt32(dr["jannh"]));
                cnt.feball = Convert.ToString(Convert.ToInt32(dr["febft"]) + Convert.ToInt32(dr["febnh"]));
                cnt.marall = Convert.ToString(Convert.ToInt32(dr["marft"]) + Convert.ToInt32(dr["marnh"]));
                cnt.aprall = Convert.ToString(Convert.ToInt32(dr["aprft"]) + Convert.ToInt32(dr["aprnh"]));
                cnt.mayall = Convert.ToString(Convert.ToInt32(dr["mayft"]) + Convert.ToInt32(dr["maynh"]));
                cnt.junall = Convert.ToString(Convert.ToInt32(dr["junft"]) + Convert.ToInt32(dr["junnh"]));
                cnt.julall = Convert.ToString(Convert.ToInt32(dr["julft"]) + Convert.ToInt32(dr["julnh"]));
                cnt.augall = Convert.ToString(Convert.ToInt32(dr["augft"]) + Convert.ToInt32(dr["augnh"]));
                cnt.sepall = Convert.ToString(Convert.ToInt32(dr["sepft"]) + Convert.ToInt32(dr["sepnh"]));
                cnt.octall = Convert.ToString(Convert.ToInt32(dr["octft"]) + Convert.ToInt32(dr["octnh"]));
                cnt.novall = Convert.ToString(Convert.ToInt32(dr["novft"]) + Convert.ToInt32(dr["novnh"]));
                cnt.decall = Convert.ToString(Convert.ToInt32(dr["decft"]) + Convert.ToInt32(dr["decnh"]));
            }

            return cnt;
        }

    }
}