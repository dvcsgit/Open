using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Member.Models;
using System.Configuration;
using System.Data;

namespace Member.Controllers
{
    public class HomeController : Controller
    {
        
        DBContext dbcontext = new DBContext();
        public ActionResult Index()
        {
            string sql = @"SELECT * FROM dbo.Member";

            List<MemberModel> userModel = new List<MemberModel>();

            DataSet ds = new DataSet();
            ds = dbcontext.ExcuteSearch(sql);
            if (ds != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    userModel.Add(new MemberModel { Id = (int)dr["Id"], Name = dr["Name"].ToString(), Email = dr["Email"].ToString() });
                }
            }
            return View(userModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MemberModel model)
        {
            if (ModelState.IsValid)
            {
                string sql = "INSERT INTO [dbo].[Member]([Name],[Email]) VALUES(@Name,@Email)";
                dbcontext.ExcuteCreate(sql, model.Name, model.Email);

                //using (SqlConnection connection = new SqlConnection(conStr))
                //{
                //    SqlDataAdapter adapter = new SqlDataAdapter();
                //    adapter.SelectCommand = new SqlCommand("SELECT * FROM dbo.Users", connection);
                //    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                //    connection.Open();

                //    DataSet ds = new DataSet();
                //    adapter.Fill(ds);

                //    //
                //    //builder.GetUpdateCommand();

                //    adapter.Update(ds);


                //}
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            string sql = "SELECT * FROM dbo.Member WHERE Id=@Id";
            MemberModel memberModel = new MemberModel();
            DataSet ds = dbcontext.ExcuteSearch(sql, id);
            if (ds != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    memberModel.Id = (int)dr["Id"];
                    memberModel.Name = dr["Name"].ToString();
                    memberModel.Email = dr["Email"].ToString();
                }
            }
            return View(memberModel);
        }

        [HttpPost]
        public ActionResult Edit(int id, string name, string email)
        {
            string sql = "UPDATE [dbo].[Member] SET [Name] =@Name,[Email] =@Email WHERE [Id]=@Id";
            dbcontext.ExcuteEdit(sql, id, name, email);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            string sql = "DELETE FROM [dbo].[Member] WHERE Id=@Id";
            dbcontext.ExcuteDelete(sql, id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Search(string q)
        {
            string sql = @"SELECT * FROM dbo.Member WHERE NAME=@Name";

            List<MemberModel> userModel = new List<MemberModel>();

            DataSet ds = new DataSet();
            ds = dbcontext.ExcuteSearch(sql, q);
            if (ds != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    userModel.Add(new MemberModel { Id = (int)dr["Id"], Name = dr["Name"].ToString(), Email = dr["Email"].ToString() });
                }
            }
            return View(userModel);
        }
    }
}