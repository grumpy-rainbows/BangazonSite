﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bangazon.Data;
using Bangazon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Bangazon.Controllers
{
    public class OrderProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderProductsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        // GET: OrderProducts
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            var applicationDbContext =
                _context.OrderProduct
                .Include(o => o.Order)
                .Include(o => o.Product);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OrderProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderProduct = await _context.OrderProduct
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderProductId == id);
            if (orderProduct == null)
            {
                return NotFound();
            }

            return View(orderProduct);
        }

        // GET: OrderProducts/Create
        /*public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "UserId");
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Title");
            return View();
        }*/

        // POST: OrderProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id)
        {
            // Find the product requested
            Product productToAdd = await _context.Product.SingleOrDefaultAsync(p => p.ProductId == id);

            var user = await GetCurrentUserAsync();

            // See if the user has an open order
            var openOrder = await _context.Order.SingleOrDefaultAsync(o => o.User == user && o.PaymentTypeId == null);

            if (openOrder == null)
            {
                // Create new order
                var order = new Order();
                order.UserId = user.Id;
                _context.Add(order);

                // Add product to order, i.e. create OrderProduct
                var orderProduct = new OrderProduct();
                orderProduct.ProductId = productToAdd.ProductId;
                orderProduct.OrderId = order.OrderId;
                productToAdd.Quantity = productToAdd.Quantity - 1;
                _context.Add(orderProduct);
                await _context.SaveChangesAsync();
            }

            else
            {
                // Add product to existing order, i.e. create OrderProduct
                var orderProduct = new OrderProduct();
                orderProduct.ProductId = productToAdd.ProductId;
                orderProduct.OrderId = openOrder.OrderId;
                productToAdd.Quantity = productToAdd.Quantity - 1;
                _context.Add(orderProduct);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Orders");


        }

        // GET: OrderProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderProduct = await _context.OrderProduct.FindAsync(id);
            if (orderProduct == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "UserId", orderProduct.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Description", orderProduct.ProductId);
            return View(orderProduct);
        }

        // POST: OrderProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderProductId,OrderId,ProductId")] OrderProduct orderProduct)
        {
            if (id != orderProduct.OrderProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderProductExists(orderProduct.OrderProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "UserId", orderProduct.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Description", orderProduct.ProductId);
            return View(orderProduct);
        }

        // GET: OrderProducts/Delete/5
        public async Task<IActionResult> Delete(int? ProductId, int? OrderId)
        {
            if (ProductId == null || OrderId == null)
            {
                return NotFound();
            }

            var orderProduct = await _context.OrderProduct
                .Include(o => o.Order)
                .Include(o => o.Product)

                .FirstOrDefaultAsync(m => m.OrderId == OrderId && m.ProductId == ProductId);
            if (orderProduct == null)
            {
                return NotFound();
            }
            _context.Remove(orderProduct);
            await _context.SaveChangesAsync();

            //return View(orderProduct);
            return RedirectToAction("Index", "Orders");
        }

        // POST: OrderProducts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var orderProduct = await _context.OrderProduct.FindAsync(id);
        //    _context.OrderProduct.Remove(orderProduct);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index", "Orders");
        //}

        private bool OrderProductExists(int id)
        {
            return _context.OrderProduct.Any(e => e.OrderProductId == id);
        }
    }
}
