using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TrackIT.Business.Abstract;
using TrackIT.Entity.Model;

namespace TrackIT.UI.Controllers
{
    public class ReportController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductRegisterService _productRegisterService;

        public ReportController(IProductService productService, IProductRegisterService productRegisterService)
        {
            _productService = productService;
            _productRegisterService = productRegisterService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public FileResult ProductExcel()
        {
            var products = _productService.TGetWithIncluded(); // Ürünleri ve ilgili kategorileri al
            // Excel dosyasını oluştur
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Products");
            // Başlık satırını ekle
            worksheet.Cell(1, 1).Value = "ID";
            worksheet.Cell(1, 2).Value = "İsmi";
            worksheet.Cell(1, 3).Value = "Kategori";
            worksheet.Cell(1, 4).Value = "Seri Numarası";
            worksheet.Cell(1, 5).Value = "Açıklama";
            worksheet.Cell(1, 6).Value = "Eklenme Tarihi";
            // Verileri Excel'e ekle
            int row = 2;
            foreach (var product in products)
            {
                worksheet.Cell(row, 1).Value = product.ProductId;
                worksheet.Cell(row, 2).Value = product.Name;
                worksheet.Cell(row, 3).Value = product.Category?.Name;
                worksheet.Cell(row, 4).Value = product.Serial;
                worksheet.Cell(row, 5).Value = product.Description;
                worksheet.Cell(row, 6).Value = product.DateAdded;
                row++;
            }
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Products.xlsx");
            }
        }
        public FileResult ProductRegisterExcel()
        {
            var registers = _productRegisterService.TGetWithIncluded();
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Zimmetler");
            worksheet.Cell(1, 1).Value = "ID";
            worksheet.Cell(1, 2).Value = "İsim";
            worksheet.Cell(1, 3).Value = "Kategori";
            worksheet.Cell(1, 4).Value = "Seri Numarası";
            worksheet.Cell(1, 5).Value = "Açıklama";
            worksheet.Cell(1, 6).Value = "Eklenme Tarihi";
            worksheet.Cell(1, 7).Value = "Kullanıcı";
            worksheet.Cell(1, 8).Value = "Zimmet Tarihi";

            int row = 2;
            foreach (var register in registers)
            {
                worksheet.Cell(row, 1).Value = register.ProductId;
                worksheet.Cell(row, 2).Value = register.Product.Name;
                worksheet.Cell(row, 3).Value = register.Product.Category?.Name;
                worksheet.Cell(row, 4).Value = register.Product.Serial;
                worksheet.Cell(row, 5).Value = register.Product.Description;
                worksheet.Cell(row, 6).Value = register.Product.DateAdded;
                worksheet.Cell(row, 7).Value = register.AppUser.Name + " " + register.AppUser.Surname;
                worksheet.Cell(row, 8).Value = register.RegistirationDate;
                row++;
            }
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                string fileName = $"Zimmetler_{DateTime.Now}.xlsx";
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    }
}
