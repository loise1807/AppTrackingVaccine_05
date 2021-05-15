using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Mvc;
using BPOM.Models;

namespace BPOM.Controllers
{
    public class PasienController : Controller
    {
        // GET: Pasien
        SqlConnection conn = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        // GET: User

        DataClassesDataContext dc = new DataClassesDataContext();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DaftarForm()
        {
            return View();
        }

        void connectionString()
        {
            conn.ConnectionString = "data source=lumbanraja1807\\LUMBANRAJA; database=TrackingVaccine_05; integrated security = SSPI;";
        }

        [HttpPost]
        public ActionResult Verify(AkunPasien akunUSER)
        {
            connectionString();
            conn.Open();
            com.Connection = conn;
            com.CommandText = "Select * from akun_pasien where username='" + akunUSER.username
                + "' and password='" + akunUSER.password + "' and role=5";

            dr = com.ExecuteReader();
            if (dr.Read())
            {
                conn.Close();
                return View("Home");
            }
            else
            {
                conn.Close();
                return View("Login");

            }
        }

        [HttpPost]
        public ActionResult Register(AkunPasien akunUSER)
        {
            connectionString();
            conn.Open();
            com.Connection = conn;
            com.CommandText = "Insert into akun_pasien (nama,nik,username,password,role) values ('" + akunUSER.nama
                + "','" + akunUSER.nik + "','"+ akunUSER.username +"','"+akunUSER.password+"',5)";

            dr = com.ExecuteReader();
            if (dr.Read())
            {
                conn.Close();
                return View("DaftarForm");
            }
            else
            {
                conn.Close();
                return View("Login");

            }
        }

        public ActionResult HomeVaksin()
        {
            var dataVaksin = from x in dc.daftar_vaksins select x;
            return View(dataVaksin);
        }

        public ActionResult DetailsVaksin(int id)
        {
            var getVaksinDetail = dc.daftar_vaksins.Single(x => x.id_vaksin == id);
            return View(getVaksinDetail);
        }
    }
}