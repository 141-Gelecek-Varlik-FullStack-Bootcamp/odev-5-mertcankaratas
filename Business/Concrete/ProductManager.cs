using AutoMapper;
using Business.Abstract;
using Business.Constant;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Model.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{ /// <summary>
  /// iş katmanı
  /// burada yapılacak işleri direk verdiğimiz kısım api tarafındaki controller'lardan direk burası ile çalışırız bu da ilgili işi yapmak için diğer katmanlara başvurur
  /// SOLID presiplerinden S harfi (single of responsibility - Tek sorumluluk ilkesi)katı bir şekilde geçerlidir. Bir method bir iş  yapar. 
  /// if suistimali yapılmamalıdır. 
  /// Product ile ilgili Ekleme Silme Güncelleme Listeleme VB. DataAccess ile iletişim kurar ve gerçekleştirir.
  /// </summary>
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        IMapper _mapper;
        public ProductManager(IProductDal productDal,IMapper mapper)
        {
            _productDal = productDal;
            _mapper = mapper;
        }
        public IResult Add(Product product)
        {
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        public IDataResult<Product> Get(int id)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.Id == id));
        }

        public IDataResult<List<ProductModel>> GetAll()
        {
            var data = _productDal.GetAll();
            var map = _mapper.Map<List<ProductModel>>(data);
            return new SuccessDataResult<List<ProductModel>>(map, Messages.ProductsListed);
           
        }
        // fiyat üstünden artan sırada listeleme yapar 
        public IDataResult<List<Product>> GetAllSortedAsc()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll().OrderBy(p=>p.Price).ToList(), Messages.ProductsListed);
        }

        // fiyat üstünden azalan sırada listeleme yapar 
        public IDataResult<List<Product>> GetAllSortedDesc()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll().OrderByDescending(p => p.Price).ToList(), Messages.ProductsListed);
        }
        // isme göre filtreleme yapar ve  fiyat üstünden azalan sırada listeleme yapar 
        public IDataResult<List<Product>> GetAllFiltered(string name)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll().Where(p => p.Name.Contains(name)).OrderByDescending(p=>p.Price).ToList(), Messages.ProductsListed);
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
