using MathTestSystem.Application.Interfaces;
using MathTestSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MathTestSystem.Web.Controllers
{
    public class TeacherController : Controller
    {
        private const long MaxXmlFileSizeInBytes = 5 * 1024 * 1024;
        private readonly IXMLService _xmlService;
        private readonly IExamResultRepository _examResultRepository;

        public TeacherController(IXMLService xmlService, IExamResultRepository examResultRepository)
        {
            _xmlService = xmlService;
            _examResultRepository = examResultRepository;
        }

        public IActionResult Index()
        {
            return View(new UploadExamVm());
        }

        public async Task<IActionResult> History()
        {
            var model = new TeacherHistoryVm
            {
                Results = await _examResultRepository.GetResultsAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadExam(IFormFile file)
        {
            var model = new UploadExamVm();

            if (file == null || file.Length == 0)
            {
                model.ErrorMessage = "Please upload a valid XML file.";
                return View("Index", model);
            }

            if (!Path.GetExtension(file.FileName).Equals(".xml", StringComparison.OrdinalIgnoreCase))
            {
                model.ErrorMessage = "Only XML files are allowed.";
                return View("Index", model);
            }

            if (file.Length > MaxXmlFileSizeInBytes)
            {
                model.ErrorMessage = "XML file size must be 5 MB or smaller.";
                return View("Index", model);
            }

            try
            {
                using var stream = file.OpenReadStream();
                model.Results = _xmlService.Process(stream);
                await _examResultRepository.SaveResultsAsync(model.Results);
            }
            catch
            {
                model.ErrorMessage = "Invalid XML file or unsupported exam format.";
            }

            return View("Index", model);
        }
    }
}
