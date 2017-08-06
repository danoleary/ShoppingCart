using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ShoppingCart.Services
{
    public class ItemImportService
    {
        private readonly ShoppingCartContext _context;

        public ItemImportService(ShoppingCartContext context)
        {
            _context = context;
        }

        public async Task ImportItems()
        {
            var fileName = "example_data.csv";
            var rows = File.ReadAllLines(fileName).Skip(1);
            var items = rows.Select(x =>
            {
                var fields = x.Split(',');
                return new Item
                {
                    Name = fields[1],
                    Description = fields[2],
                    Stock = Int32.Parse(fields[3]),
                    Price = Decimal.Parse(fields[4])
                };
            });
            _context.Items.AddRange(items);
            await _context.SaveChangesAsync();

        }

    }

    
}