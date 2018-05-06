using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyProject
{
    public class Employee
    {
		int Eid;
		string Address;
		string Position;
		Int64 Phone;
		string Evaluation;
		DateTime Dob;
		string Sex;
		string Email;
		DateTime Start_date;
		string Name;
		int E_dnum;
		DateTime E_d_start_date;
		int E_bnum;


		public int EID
		{
			get { return Eid; }
			set { Eid = value; }
		}
		public string address
		{
			get { return Address; }
			set { Address = value; }
		}
		public string position
		{
			get { return Position; }
			set { Position = value; }
		}
		public Int64 phone
		{
			get { return Phone; }
			set { Phone = value; }
		}
		public string evaluation
		{
			get { return Evaluation; }
			set { Evaluation = value; }
		}
		public DateTime DOB
		{
			get { return Dob; }
			set { Dob = value; }
		}
		public string sex
		{
			get { return Sex; }
			set { Sex = value; }
		}
		public string email
		{
			get { return Email; }
			set { Email = value; }
		}
		public DateTime start_date
		{
			get { return Start_date; }
			set { Start_date = value; }
		}
		public string name
		{
			get { return Name; }
			set { Name = value; }
		}
		public int E_Dnum
		{
			get { return E_dnum; }
			set { E_dnum = value; }
		}
		public DateTime E_D_start_date
		{
			get { return E_d_start_date; }
			set { E_d_start_date = value; }
		}
		public int E_Bnum
		{
			get { return E_bnum; }
			set { E_bnum = value; }
		}
	}
}
