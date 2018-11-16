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
            if (customer.Type != null)
            {
                _ctx.Attach(customer.Type).State = EntityState.Unchanged;
            }
            var customerSaved = _ctx.Customers.Add(customer).Entity;
            _ctx.SaveChanges();
            return customerSaved;
        }

        public Customer ReadyById(int id)
        {
            return _ctx.Customers
                .Include(c => c.Type)
                .FirstOrDefault(c => c.Id == id);
        }
        
        public Customer ReadyByIdIncludeOrders(int id)
        {
            return _ctx.Customers
                .Include(c => c.Type)
                .Include(c => c.Orders)
                .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Customer> ReadAll(Filter filter)
        {
            if (filter != null && filter.ItemsPrPage > 0 && filter.CurrentPage > 0)
            {
                return _ctx.Customers
                    .Include(c => c.Type)
                    .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                    .Take(filter.ItemsPrPage);
            }
            return _ctx.Customers
                    .Include(c => c.Type);
        }

        public Customer Update(Customer customerUpdate)
        {
            _ctx.Attach(customerUpdate).State = EntityState.Modified;
            _ctx.Entry(customerUpdate).Collection(c => c.Orders).IsModified = true;
            _ctx.Entry(customerUpdate).Reference(c => c.Type).IsModified = true;
            var orders = _ctx.Orders.Where(o => o.Customer.Id == customerUpdate.Id
                                   && !customerUpdate.Orders.Exists(co => co.Id == o.Id));
            foreach (var order in orders)
            {
                order.Customer = null;
                _ctx.Entry(order).Reference(o => o.Customer)
                    .IsModified = true;
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