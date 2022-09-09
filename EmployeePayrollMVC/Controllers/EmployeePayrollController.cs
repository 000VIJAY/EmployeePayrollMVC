using EmployeePayrollMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ActionNameAttribute = System.Web.Mvc.ActionNameAttribute;
using BindAttribute = Microsoft.AspNetCore.Mvc.BindAttribute;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using HttpDeleteAttribute = System.Web.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using ValidateAntiForgeryTokenAttribute = System.Web.Mvc.ValidateAntiForgeryTokenAttribute;

namespace EmployeePayrollMVC.Controllers
{
    public class EmployeePayrollController : Controller
    {
        EmployeePayrollDbContext _dbContext;
        
        public EmployeePayrollController(EmployeePayrollDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }

        public IActionResult Getall()
        {
            try
            {
               
                return View(this._dbContext.EmployeesDetails.ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IActionResult GetByEmployeeId(int Id)
        {
            try
            {
                var empDetail = _dbContext.EmployeesDetails.Where(x => x.EmployeeId == Id).FirstOrDefault();
                return View(empDetail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Get
        public IActionResult Remove(int Id)
        {
            try
            {
                var empInfo = _dbContext.EmployeesDetails.Where(x => x.EmployeeId == Id).FirstOrDefault();
                if (empInfo == null)
                {
                    return NotFound();
                }
                return View(empInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // POST
        [HttpPost,ActionName("RemoveConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveConfirmed(int Id)
        {
            try
            {
                var employeeDetail =  _dbContext.EmployeesDetails.Find(Id);
                if(employeeDetail == null)
                {
                    return NotFound();
                }
                _dbContext.EmployeesDetails.Remove(employeeDetail);
                _dbContext.SaveChanges();
                return Json(new { html = Helper.RenderRazorViewToString(this, "Getall", _dbContext.EmployeesDetails.ToList()) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public IActionResult Add(int Id=0)
        {
            try
            {
               if(Id==0)
                {
                    return View(new EmployeeDataModel ());
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add( [Bind("EmployeeId,FirstName,LastName,Age,Department,Salary,StartedDate")] EmployeeDataModel employeeDataModel)
        {
            try
            {
               if(ModelState.IsValid)
                {
                    if(employeeDataModel != null)
                    {
                        employeeDataModel.StartedDate = DateTime.Now;
                        _dbContext.EmployeesDetails.Add(employeeDataModel);
                        _dbContext.SaveChanges();
                    }
                    return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this,"Getall",this._dbContext.EmployeesDetails.ToList())});
                }
                return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "Add", this._dbContext.EmployeesDetails.ToList()) });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IActionResult Edit(int Id =0)
        {
            try
            {
                if(Id == 0)
                {
                    return NotFound();
                }
                var employeeDataModel = _dbContext.EmployeesDetails.Find(Id);
                if (employeeDataModel == null)
                {
                    return NotFound();
                }
                return View(employeeDataModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id ,[Bind("EmployeeId,FirstName,LastName,Age,Department,Salary")] EmployeeDataModel employeeDataModel)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                    if (Id != 0)
                    {
                        employeeDataModel.StartedDate = employeeDataModel.StartedDate;
                        _dbContext.EmployeesDetails.Update(employeeDataModel);
                        _dbContext.SaveChanges();
                    }
                    return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "Getall", this._dbContext.EmployeesDetails.ToList()) });
                }
                return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "Edit", this._dbContext.EmployeesDetails.ToList()) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
