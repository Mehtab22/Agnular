using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestWebApi.Context;
using TestWebApi.Models;

namespace TestWebApi.Controllers
{
    public class StudentController : ApiController
    {
        private static readonly StudenDBEntities _Db = new StudenDBEntities();

        [HttpPost]
        public HttpResponseMessage insert(Student student)
        {
            try
            {
                _Db.Student.Add(student);
                _Db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "ok");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "something went wrng");

            }
        }

        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            try
            {

                var stds =  _Db.Student.ToList();
                //var result = _Db.Student.AsQueryable().ToList();
                return Request.CreateResponse(HttpStatusCode.OK, stds);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Something went wrong"); 

            }

        }


        [Route("api/edit")]
        [HttpGet]
        public HttpResponseMessage Edit(int id)
        {
            try
            {
                var result = _Db.Student.AsQueryable().Where(x => x.id == id).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "some error");

            }

        }



        [Route("api/update")]
        [HttpPost]
        public HttpResponseMessage UpdateStudent(Student std)
        {
            try
            {
                var result = _Db.Student.AsQueryable().Where(x => x.id == std.id).FirstOrDefault();
           
                result.name = std.name;
                result.address = std.address;
                _Db.Entry(result).State = System.Data.Entity.EntityState.Modified;
                _Db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "some error");

            }

        }
        [Route("api/delete")]
        [HttpGet]
        public HttpResponseMessage DeleteStudent(int id)
        {
            try
            {
                var result = _Db.Student.AsQueryable().Where(x => x.id == id).FirstOrDefault();
                _Db.Student.Remove(result);
                _Db.SaveChanges();
      
                return Request.CreateResponse(HttpStatusCode.OK, "");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "some error");

            }

        }
    }
}
