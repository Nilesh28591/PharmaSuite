using Microsoft.AspNetCore.Mvc;
using PharmaSuiteWebAPI.Data;
using PharmaSuiteWebAPI.Dto;
using PharmaSuiteWebAPI.Model;
using PharmaSuiteWebAPI.Repo;

namespace PharmaSuiteWebAPI.Services
{
    public class SupplierService: ISupplierRepo
    {
        
            PharmaSuiteDBContext _context;
            public SupplierService(PharmaSuiteDBContext context)
            {
                this._context = context;
            }


            public void AddSupplier([FromBody] SupplierDTO supplier)
            {
                var Supplier = new Supplier()
                {
                    SupplierId = supplier.SupplierId,
                    Name = supplier.Name,
                    ContactPerson = supplier.ContactPerson,
                    Phone = supplier.Phone,
                    Email = supplier.Email,
                    Address = supplier.Address,
                    CreatedBy = "Admin",
                    //CreatedBy will be the session
                    CreatedAt = DateTime.Now
                };

                _context.supplier.Add(Supplier);
                _context.SaveChanges();





                //supplier.CreatedAt = DateTime.Now;
                //_context.supplier.Add(supplier);
                //_context.SaveChanges();
            }

            public List<Supplier> GetSupplier()
            {
                return _context.supplier.ToList();
            }


            public bool DeleteSup(int id)
            {
                var del = _context.supplier.Find(id);
                if (del == null)
                {
                    return false;
                }
                _context.supplier.Remove(del);
                _context.SaveChanges();
                return true;
            }

            //public bool EditSupplier(SupplierDTO supplier)
            //{
            //    var ex = _context.supplier.Find(supplier.SupplierId);

            //    if(ex == null)
            //    {
            //        return false;
            //    }

            //    ex.Name = supplier.Name;
            //    ex.ContactPerson = supplier.ContactPerson;
            //    ex.Phone = supplier.Phone;
            //    ex.Email = supplier.Email;
            //    ex.Address = supplier.Address;
            //    ex.CreatedBy = supplier.CreatedBy;

            //    _context.supplier.Update(ex);
            //    _context.SaveChanges();
            //    return true;
            //}

            public bool EditSupplier(SupplierDTO supplier)
            {

                var ex = _context.supplier.Find(supplier.SupplierId);
                if (ex == null)
                {
                    return false;
                }

                ex.Name = supplier.Name;
                ex.ContactPerson = supplier.ContactPerson;
                ex.Phone = supplier.Phone;
                ex.Email = supplier.Email;
                ex.Address = supplier.Address;
                ex.ModifiedBy = "Admin";   
                //ex.CreatedBy = supplier.CreatedBy;  session
                ex.ModifiedAt = DateTime.Now;

                _context.SaveChanges(); // no need for Update() here
                return true;
            }



            public Supplier GetSupbyId(int id)
            {
                return _context.supplier.FirstOrDefault(x => x.SupplierId == id);


            }


    }


    
}
