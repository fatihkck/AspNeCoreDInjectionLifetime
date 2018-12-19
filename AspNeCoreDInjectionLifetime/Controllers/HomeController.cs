using AspNeCoreDInjectionLifetime.Models;
using AspNeCoreDInjectionLifetime.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;

namespace AspNeCoreDInjectionLifetime.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly OperationService OperationService;
        private readonly IOperationTransient TransientOperation;
        private readonly IOperationScoped ScopedOperation;
        private readonly IOperationSingleton SingletonOperation;
        private readonly IOperationSingletonInstance SingletonInstanceOperation;

        public HomeController(
            OperationService operationService,
        IOperationTransient transientOperation,
        IOperationScoped scopedOperation,
        IOperationSingleton singletonOperation,
        IOperationSingletonInstance singletonInstanceOperation)
        {
            
            OperationService = operationService;
            TransientOperation = transientOperation;
            ScopedOperation = scopedOperation;
            SingletonOperation = singletonOperation;
            SingletonInstanceOperation = singletonInstanceOperation;
        }

        public void Index()
        {
            string transient = $"Transient :  {TransientOperation.OperationId}";
            string scoped = $"Scoped :  {ScopedOperation.OperationId}";
            string singleton = $"Singleton :  {SingletonOperation.OperationId}";
            string SingletonInstance = $"Singleton Instance :  {SingletonInstanceOperation.OperationId}";


            string transientOp = $"Transient (always different)  :  {OperationService.TransientOperation.OperationId}";
            string scopedOp = $"Scoped (same as top within a request but different across requests) :  {OperationService.ScopedOperation.OperationId}";
            string singletonOp = $"Singleton(always same) :  {OperationService.SingletonOperation.OperationId}";
            string SingletonInstanceOp = $"Singleton Instance :  {OperationService.SingletonInstanceOperation.OperationId}";

            using (StreamWriter wr = new StreamWriter(HttpContext.Response.Body))
            {
                wr.WriteLine("Controller Service");
                wr.WriteLine(transient);
                wr.WriteLine(scoped);
                wr.WriteLine(singleton);
                wr.WriteLine(SingletonInstance);
                wr.WriteLine("");
                wr.WriteLine("");
                wr.WriteLine("OperationService Operations");
                wr.WriteLine(transientOp);
                wr.WriteLine(scopedOp);
                wr.WriteLine(singletonOp);
                wr.WriteLine(SingletonInstanceOp);

                wr.Flush();

            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
