using Customers.Api.DTO;
using Customers.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Customers.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly DBContext DBContext;

        public CustomersController(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerDTO>>> Get()
        {
            var List = await DBContext.Customers.Select(
                s => new CustomerDTO
                {
                    Id = s.Id,
                    Username = s.Username,
                    Password = s.Password,
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerById(int id)
        {
            CustomerDTO User = await DBContext.Customers.Select(
                    s => new CustomerDTO
                    {
                        Id = s.Id,
                        Username = s.Username,
                        Password = s.Password,
                    })
                .FirstOrDefaultAsync(s => s.Id == id);

            if (User == null)
            {
                return NotFound();
            }
            else
            {
                return User;
            }
        }

        [HttpPost]
        public async Task<HttpStatusCode> InsertUser(CustomerDTO customer)
        {
            var entity = new Customer()
            {
                Id = customer.Id,
                Username = customer.Username,
                Password = customer.Password
            };

            DBContext.Customers.Add(entity);
            await DBContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateUser")]
        public async Task<HttpStatusCode> UpdateUser(CustomerDTO User)
        {
            var entity = await DBContext.Customers.FirstOrDefaultAsync(s => s.Id == User.Id);

            entity.Username = User.Username;
            entity.Password = User.Password;

            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("{Id}")]
        public async Task<HttpStatusCode> DeleteUser(int id)
        {
            var entity = new Customer()
            {
                Id = id
            };
            DBContext.Customers.Attach(entity);
            DBContext.Customers.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
