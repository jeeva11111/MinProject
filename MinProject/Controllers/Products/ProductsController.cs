using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MinProject.Data;
using MinProject.Filters;
using MinProject.Models;
using MinProject.SignalRFun;
using System;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MinProject.Controllers.Pagenaction
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        //private readonly ConnectionMapping _connection;

        public ProductsController(ApplicationDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _contextAccessor = httpContext;
            //_connection = connection;
        }

        [ValidRoutingBlockerFilter]
        public IActionResult Index()
        {
            var currentUser = Guid.Parse(_contextAccessor.HttpContext.Session.GetString("UserId"));
            var filterUser = (from x in _context.Users where x.Id != currentUser select x).ToList();

            ViewBag.Users = filterUser;
            return View();
        }

        public IActionResult ProductList()
        {
            return PartialView("");
        }

        //    [HttpPost]
        //    public async Task<IActionResult> AddProduct(Guid productId, int quantity)
        //    {
        //        var product = _context.Products.Where(x => x.Id == productId).FirstOrDefault();
        //        var currentLogginUser = _contextAccessor.HttpContext.Session.GetString("UserId");
        //        if (product == null || product.Stock < 0)
        //        {
        //            return BadRequest("Unable to place the order");
        //        }
        //        var order = new Order()
        //        {
        //            CreatedBy = "User",
        //            CreatedDate = DateTime.UtcNow,
        //            IsDeleted = false,
        //            UserId = Guid.Parse(currentLogginUser),
        //            Name = product.Name,
        //            LastUpdatedDate = DateTime.UtcNow,
        //            UpdatedBy = "user",
        //            ProductId = product.Id
        //        };
        //        _context.Orders.Add(order);

        //        var notify = new Notifaction()
        //        {
        //            Id = new Guid(),
        //            CreatedDate = DateTime.UtcNow,
        //            UserId = Guid.Parse(currentLogginUser),
        //            Message = $"Hello, {currentLogginUser}. Order is placed: {product.Name}"
        //        };

        //        product.Stock -= quantity;
        //        _context.Notifications.Add(notify);
        //        _context.SaveChanges();
        //        var notificationMessage = $"Order placed successfully for product: {product.Name}";

        //        await _hubContext.Clients.User(currentLogginUser).SendAsync("ReceiveNotification", notificationMessage);

        //        return Ok(new { message = "Order placed successfully" });
        //    }

        //[HttpPost]
        //public async Task<IActionResult> AddProduct(Guid productId, int quantity)
        //{
        //    var product = _context.Products.FirstOrDefault(x => x.Id == productId);
        //    var currentLogginUser = _contextAccessor.HttpContext.Session.GetString("UserId");




        //    if (product == null || product.Stock < 0)
        //    {
        //        return BadRequest("Unable to place the order");
        //    }
        //    var order = new Order()
        //    {
        //        CreatedBy = "User",
        //        CreatedDate = DateTime.UtcNow,
        //        IsDeleted = false,
        //        UserId = Guid.Parse(currentLogginUser),
        //        Name = product.Name,
        //        LastUpdatedDate = DateTime.UtcNow,
        //        UpdatedBy = "user",
        //        ProductId = product.Id
        //    };
        //    _context.Orders.Add(order);

        //    var notify = new Notifaction()
        //    {
        //        Id = Guid.NewGuid(), // Use Guid.NewGuid() to generate a new unique ID
        //        CreatedDate = DateTime.UtcNow,
        //        UserId = Guid.Parse(currentLogginUser),
        //        Message = $"Hello, {currentLogginUser}. Order is placed: {product.Name}"
        //    };

        //    //  product.Stock -= quantity;
        //    _context.Notifications.Add(notify);
        //    _context.SaveChanges();

        //    var notificationMessage = $"Order placed successfully for product: {product.Name}";

        //    // Log the notification message to check if it's being sent
        //    Console.WriteLine($"Sending notification: {notificationMessage}");

        //    var connectionId = _connection.GetConnectionId(currentLogginUser);
        //    if (!string.IsNullOrEmpty(connectionId))
        //    {
        //        await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveNotification", notificationMessage);
        //    }


        //    // Send notification using SignalR
        //    await _hubContext.Clients.All.SendAsync("ReceiveNotification", notificationMessage);
        //    return Ok(new { message = "Order placed successfully" });
        //}

    }
}
