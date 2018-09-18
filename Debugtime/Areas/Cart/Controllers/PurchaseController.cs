using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Debugtime.Common.Infrastructure;
using Debugtime.Common.Model;
using Debugtime.Common.Model.View;
using Debugtime.Common.Persistence;
using Debugtime.Paypal;
using DebugTime.Domain.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PayPal.Api;

namespace Debugtime.Areas.Cart.Controllers
{
    [Authorize]
    [RouteArea("Cart")]
    [RoutePrefix("Purchase")]
    public class PurchaseController : Controller
    {
        private Payment _payment;
        private ApplicationDbContext _context;
        private AppUserManager _userManager;
        private APIContext _apiContext;

        public APIContext ApiContext => _apiContext ?? (_apiContext = Configuration.GetApiContext());

        public AppUserManager UserManager =>
            _userManager ?? (_userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>());

        [HttpGet]
        [Route("{cId}")]
        public async Task<ActionResult> ConfirmPurchase(string cId)
        {
            _context = HttpContext.GetOwinContext().Get<ApplicationDbContext>();

            if (string.IsNullOrEmpty(cId))
                return RedirectToAction("Error", "Oops",
                    new
                    {
                        Subject = "Course Not Found",
                        Message = "Course you are trying to buy not exist.",
                        HelpUrl = Url.Action("Index", "Courses", new { area = "" }),
                        HelpText = "Try something else",
                        area = ""
                    });

            var course = _context.Courses.SingleOrDefault(c => c.Id == cId);

            if (course == null)
                return RedirectToAction("Error", "Oops",
                    new
                    {
                        Subject = "Course Not Found",
                        Message = "Course you are trying to buy not found.",
                        HelpUrl = Url.Action("Index", "Courses", new { area = "" }),
                        HelpText = "Try Another Course",
                        area = ""
                    });


            if (!await UserManager.HasCourseAsync(course.Id, User.Identity.GetUserId()))
            {
                var model = new CartPurchaseViewModel
                {
                    Id = course.Id,
                    Price = course.Price,
                    Discount = 0,
                    Title = course.Title
                };
                return View(model);
            }

            return RedirectToAction("Play", "Studio", new { area = "", courseId = cId, section = 1, lesson = 1 });
        }


        [HttpPost]
        [Route("PaymentWithPaypal")]
        public ActionResult PaymentWithPaypal(string id)
        {

            try
            {
                _context = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                var course = _context.Courses.SingleOrDefault(c => c.Id == id);
                if (course == null)
                    return RedirectToAction("Error", "Oops",
                        new { ares = "", Message = "Course you are trying to buy not exist", HelpUrl = Url.Action("Index", "Courses", new { area = "Library" }) });


                var baseUri = Request?.Url?.Scheme + "://" + Request?.Url?.Authority +
                                 "/Cart/Purchase/ConfirmPaymentWithPaypal?";


                var paymentModel = new PaypalCreatePaymentModel
                {
                    ItemPrice = (decimal)course.Price,
                    ItemName = course.Title,
                    Currency = "USD",
                    InvoiceNumber = Guid.NewGuid().ToString(),
                    Shiping = 0,
                    Subtotal = (decimal)course.Price,
                    Tax = 0,
                    Total = (decimal)course.Price,
                    TransactionDescription = String.Concat(User.Identity.Name, " buyes ", course.Title)
                };

                var userId = User.Identity.GetUserId();
                DebugTime.Domain.Model.Transaction tSac;
                if (!_context.Transactions.Any(t => t.CourseId == course.Id
                                                    && t.BuyerId == userId))
                {
                    var tId = _context.Transactions.Count();
                    tSac = new DebugTime.Domain.Model.Transaction
                    {
                        Id = Guid.NewGuid().ToString(),
                        CourseId = course.Id,
                        BuyerId = User.Identity.GetUserId(),
                        Order = $"#{++tId}",
                        PaymentStatus = PaymentStatus.WaitingPayment,
                        Statement = new Statement
                        {
                            Discount = 0,
                            Amount = (decimal)course.Price,
                            Time = DateTime.Today
                        }
                    };
                    _context.Transactions.Add(tSac);
                    _context.SaveChanges();
                }
                else
                {
                    tSac = _context.Transactions.FirstOrDefault(t => t.CourseId == course.Id
                                                                     && t.BuyerId == userId);
                }

                var createdPayment = CreatePayment(ApiContext, baseUri + "guid=" + tSac.Id, paymentModel);

                if (createdPayment?.links != null)
                {
                    using (var links = createdPayment.links.GetEnumerator())
                    {
                        string paypalRedirectUrl = null;

                        while (links.MoveNext())
                        {
                            var link = links.Current;

                            if (link != null && link.rel.ToLower().Trim().Equals("approval_url"))
                                paypalRedirectUrl = link.href;
                        }

                        Session.Add(tSac.Id, createdPayment.id);
                        Session.Add("Ctb_Id", course.Id);

                        return Redirect(paypalRedirectUrl);
                    }
                }

                return RedirectToAction("Error", "Oops",
                    new
                    {
                        area = "",
                        StatusCode = "500",
                        Subject = "Payment Failed",
                        Message = "Transaction has been failed due to some unknown error",
                        HelpUrl = Url.Action("ConfirmPurchase", "Purchase", new { area = "Cart", courseId = course.Id })
                    });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Oops",
                    new
                    {
                        area = "",
                        StatusCode = "500",
                        Subject = "Internal server error",
                        Message = "Transaction has been failed due to some unknown error",
                        HelpUrl = Url.Action("ConfirmPurchase", "Purchase", new { area = "Cart", courseId = id })
                    });

            }
        }


        [HttpGet]
        [Route("ConfirmPaymentWithPaypal")]
        public ActionResult ConfirmPaymentWithPaypal()
        {
            var cId = Session["Ctb_Id"] as string;

            try
            {
                _context = HttpContext.GetOwinContext().Get<ApplicationDbContext>();

                var payerId = Request.Params["PayerID"];

                var guid = Request.Params["guid"];
                var transactionId = Session[guid] as string;

                var executedPayment = ExecutePayment(ApiContext, payerId, transactionId);

                if (executedPayment.state.ToLower() != "approved")
                    return RedirectToAction("Error", "Oops",
                        new
                        {
                            area = "",
                            Subject = "Payment failed",
                            Message = "your Paypal payment is failed to execute",
                            HelpUr = Url.Action("ConfirmPurchase", "Purchase", new { area = "Cart", id = cId })
                        });

                var userCourse = new UserCourse
                {
                    CourseId = cId,
                    UserId = User.Identity.GetUserId()
                };

                var transaction = _context.Transactions.SingleOrDefault(t => t.Id == guid);
                transaction.PaymentStatus = PaymentStatus.Confirmed;
                _context.Entry(transaction).State = EntityState.Modified;
                _context.UserCourses.Add(userCourse);
                _context.SaveChanges();

                var userCount = _context.UserCourses.Count(c => c.CourseId == cId);
                var courseInDb = _context.Courses.SingleOrDefault(c => c.Id == cId);

                if (courseInDb != null) courseInDb.UserCount = userCount;

                _context.Entry(courseInDb).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("Play", "Studio", new { area = "", courseId = cId, Section = 1, Lesson = 1 });
            }
            catch (DbUpdateException)
            {
                return RedirectToAction("Play", "Studio", new { area = "", courseId = cId, Section = 1, Lesson = 1 });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Oops",
                    new
                    {
                        area="",
                        StatusCode = "500",
                        Subject = "Error Ocurred",
                        Message = "your Paypal payment is failed to execute",
                        HelpUr = Url.Action("ConfirmPurchase", "Purchase", new { area = "", id = cId })
                    });
            }
        }
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution { payer_id = payerId };
            _payment = new Payment { id = paymentId };
            return _payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl, PaypalCreatePaymentModel paymentModel)
        {
            var itemList = new ItemList
            {
                items = new List<Item>()
            };

            itemList.items.Add(new Item()
            {
                currency = paymentModel.Currency,
                name = paymentModel.ItemName,
                price = paymentModel.ItemPrice.ToString(CultureInfo.InvariantCulture),
                sku = "sku",
                quantity = "1"
            });

            var payer = new Payer
            {
                payment_method = "paypal"
            };

            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            var details = new Details()
            {
                tax = paymentModel.Tax.ToString(CultureInfo.InvariantCulture),
                shipping = paymentModel.Shiping.ToString(CultureInfo.InvariantCulture),
                subtotal = paymentModel.Subtotal.ToString(CultureInfo.InvariantCulture)
            };

            var amount = new Amount
            {
                currency = paymentModel.Currency,
                total = paymentModel.Total.ToString(CultureInfo.InvariantCulture),
                details = details
            };

            var transactionList = new List<PayPal.Api.Transaction>
            {
                new PayPal.Api.Transaction
                {
                    description = paymentModel.TransactionDescription,
                    invoice_number = paymentModel.InvoiceNumber,
                    amount = amount,
                    item_list = itemList
                }
            };


            _payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            return _payment.Create(apiContext);
        }
    }
}