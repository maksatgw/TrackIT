using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.Business.Abstract;
using TrackIT.DataAccess.Abstract;
using TrackIT.Entity.Model;

namespace TrackIT.Business.Concrete
{
    public class ProductRegisterManager : IProductRegisterService
    {
        private readonly IProductRegisterDataAccess _service;

        public ProductRegisterManager(IProductRegisterDataAccess service)
        {
            _service = service;
        }

        public void TDelete(ProductRegistiration model)
        {
            _service.Delete(model);
        }

        public ProductRegistiration TGet(int id)
        {
            return _service.Get(id);
        }

        public List<ProductRegistiration> TGet()
        {
            return _service.Get();
        }

        public ProductRegistiration TGetByUserId(string userId)
        {
            return _service.GetByUserId(userId);
        }

        public int TGetProductRegisteredUserCount(string id)
        {
            return _service.GetProductRegisteredUserCount(id);
        }

        public List<ProductRegistiration> TGetWithIncluded()
        {
            return _service.GetWithIncluded();
        }

        public List<ProductRegistiration> TGetWithIncludedSearch(string? searchQuery = null, int? categoryId = null, string? userId = null)
        {
            return _service.GetWithIncludedSearch(searchQuery, categoryId, userId);
        }

        public void TInsert(ProductRegistiration model)
        {
            _service.Insert(model);
        }

        public async Task<string> TSaveFile(IFormFile file, string destinationFolder)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Dosya yok veya hatalı");
            }

            var extension = Path.GetExtension(file.FileName);
            var newAssetName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/assets/{destinationFolder}/", newAssetName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return newAssetName;
        }

        public void TUpdate(ProductRegistiration model)
        {
            _service.Update(model);
        }
    }
}
