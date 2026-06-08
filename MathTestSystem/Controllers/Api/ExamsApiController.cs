using MathTestSystem.Application.Common;
using MathTestSystem.Application.DTO_s;
using MathTestSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace MathTestSystem.Web.Controllers.Api
{
    [ApiController]
    [Route("api/exams")]
    public class ExamsApiController : ControllerBase
    {
        private const long MaxXmlFileSizeInBytes = 5 * 1024 * 1024;
        private readonly IXMLService _xmlService;
        private readonly IExamResultRepository _examResultRepository;

        public ExamsApiController(IXMLService xmlService, IExamResultRepository examResultRepository)
        {
            _xmlService = xmlService;
            _examResultRepository = examResultRepository;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(
                    ApiResponseHelper.Fail<object>("Invalid or empty file")
                );
            }

            if (!Path.GetExtension(file.FileName).Equals(".xml", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(
                    ApiResponseHelper.Fail<object>("Only XML files are allowed")
                );
            }

            if (file.Length > MaxXmlFileSizeInBytes)
            {
                return BadRequest(
                    ApiResponseHelper.Fail<object>("XML file size must be 5 MB or smaller")
                );
            }

            try
            {
                using var stream = file.OpenReadStream();
                var results = _xmlService.Process(stream);
                await _examResultRepository.SaveResultsAsync(results);

                return Ok(ApiResponseHelper.Success(results, "File processed successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(
                    ApiResponseHelper.Fail<object>($"XML processing failed: {ex.Message}")
                );
            }
        }

        [HttpPost("process")]
        [Consumes("application/xml", "text/xml", "text/plain")]
        public async Task<IActionResult> ProcessXml()
        {
            using var reader = new StreamReader(Request.Body);
            var xml = await reader.ReadToEndAsync();

            if (string.IsNullOrWhiteSpace(xml))
            {
                return BadRequest(
                    ApiResponseHelper.Fail<object>("XML is empty")
                );
            }

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));

            try
            {
                var results = _xmlService.Process(stream);
                await _examResultRepository.SaveResultsAsync(results);

                return Ok(ApiResponseHelper.Success(results, "XML processed successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(
                    ApiResponseHelper.Fail<object>($"XML processing failed: {ex.Message}")
                );
            }
        }
    }
}
