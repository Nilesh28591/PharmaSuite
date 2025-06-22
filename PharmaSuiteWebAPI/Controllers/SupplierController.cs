using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmaSuiteWebAPI.Dto;
using PharmaSuiteWebAPI.Repo;

namespace PharmaSuiteWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {

        ISupplierRepo repo;
        public SupplierController(ISupplierRepo repo)
        {
            this.repo = repo;
        }


        [HttpPost]
        [Route("AddSup")]
        public IActionResult AddSupp(SupplierDTO supplier)
        {
            supplier.CreatedBy = "Admin";

            repo.AddSupplier(supplier);
            return Ok("Added Success");
        }

        [HttpGet]
        [Route("GetSup")]
        public ActionResult GetSupp()
        {
            var data = repo.GetSupplier();
            return Ok(data);
        }

        [HttpDelete]
        [Route("DeleteSup/{id}")]
        public IActionResult DeleteSup(int id)
        {
            var result = repo.DeleteSup(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok("Deleted Successfully");
        }


        //[HttpPut]
        //[Route("EditSup/{id}")]
        //public IActionResult EditSupp(int id,[FromBody] SupplierDTO supplier)
        //{


        //    repo.EditSupplier(supplier);
        //    return Ok("Supplier Updated Successfully");

        //}


        [HttpGet]
        [Route("GetSupById/{id}")]
        public IActionResult GetEmpbyid(int id)
        {
            var supp = repo.GetSupbyId(id);

            if (supp == null)
            {
                return NotFound();
            }

            return Ok(supp);

        }



        [HttpPut]
        [Route("EditSup")]
        public IActionResult EditSupp([FromBody] SupplierDTO supplier)
        {
            //if (id != supplier.SupplierId)
            //{
            //    return BadRequest("ID mismatch");
            //}

            bool updated = repo.EditSupplier(supplier);
            if (!updated)
            {
                return NotFound("Supplier not found");
            }

            return Ok("Supplier updated successfully");
        }

    }
}
