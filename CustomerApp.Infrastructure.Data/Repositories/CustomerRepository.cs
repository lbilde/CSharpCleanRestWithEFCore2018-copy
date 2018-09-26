using System.Collections.Generic;
using System.Linq;
using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Infrastructure.Data.Repositories
{
    public class CustomerRepository: ICustomerRepository
    {
        readonly CustomerAppContext _ctx;

        public CustomerRepository(CustomerAppContext ctx)
        {
            _ctx = ctx;
        }
        
        public Customer Create(Customer customer)
        {
           var cust = _ctx.Customers.Add(customer).Entity;
            _ctx.SaveChanges();
            return cust;
        }

        public Customer ReadyById(int id)
        {
            return _ctx.Customers
                .FirstOrDefault(c => c.Id == id);
        }
        
        public Customer ReadyByIdIncludeOrders(int id)
        {
            return _ctx.Customers
                .Include(c => c.Orders)
                .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Customer> ReadAll()
        {
            return _ctx.Customers;
        }

        public Customer Update(Customer customerUpdate)
        {
            _ctx.Attach(customerUpdate).State = EntityState.Modified;
            _ctx.SaveChanges();

            foreach (var order in _ctx.Orders.Where(o => o.Customer.Id == customerUpdate.Id))
            {
                if (!customerUpdate.Orders.Exists(co => co.Id == order.Id))
                {
                    order.Customer = null;
                    _ctx.Entry(order).Reference(o => o.Customer).IsModified = true;
                }
            }
            _ctx.SaveChanges();
            return customerUpdate;
        }

        public Customer Delete(int id)
        {
            /*var ordersToRemove = _ctx.Orders.Where(o => o.Customer.Id == id);
            _ctx.RemoveRange(ordersToRemove);*/
            var custRemoved = _ctx.Remove(new Customer {Id = id}).Entity;
            _ctx.SaveChanges();
            return custRemoved;
        }
    }
}