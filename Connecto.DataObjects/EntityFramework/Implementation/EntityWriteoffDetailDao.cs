using System;
using System.Collections.Generic;
using System.Linq;
using Connecto.BusinessObjects;
using Connecto.Common.Enumeration;
using Connecto.DataObjects.EntityFramework.ModelMapper;
using Connecto.DataObjects.EntityFramework.Utility;

namespace Connecto.DataObjects.EntityFramework.Implementation
{
    public class EntityWriteoffDetailDao : IWriteoffDetailDao
    {
        
        public Tuple<IList<WriteoffDetail>, int> GetListofWriteoffDetails(FilterCriteria filter)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var writeoff = context.WriteoffDetails.Where(e => e.Status == RecordStatus.Active).ToList();
                var distIds = writeoff.Select(e => e.WriteoffId).Distinct();
                var retItems = new List<WriteoffDetail>();
                foreach (var distId in distIds.Skip(filter.iDisplayStart).Take(filter.iDisplayLength))
                {
                    var item = Mapper.Map(writeoff.FirstOrDefault(e => e.WriteoffId == distId));
                    item.ItemCount = writeoff.Count(e => e.WriteoffId == distId);
                    item.GrossPrice = writeoff.Where(e => e.WriteoffId == distId).Sum(e => e.NetPrice);
                    retItems.Add(item);
                }
                return new Tuple<IList<WriteoffDetail>, int>(retItems, distIds.Count());
            }
        }



        public List<Writeoff> GetWriteoffs(bool writeoff)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var writeoffIds = writeoff ? context.WriteoffDetails.Where(e => (e.Quantity + e.QuantityActual + e.QuantityLower) > 0 && e.Status == RecordStatus.Active).Select(e => e.WriteoffId).Distinct().ToList()
                            : context.WriteoffDetailCarts.Where(e => (e.Quantity + e.QuantityActual + e.QuantityLower) > 0 && e.Status == RecordStatus.Active).Select(e => e.WriteoffId).Distinct().ToList();
                return context.Writeoffs.Where(e => writeoffIds.Contains(e.WriteoffId)).Select(Mapper.Map).ToList();
            }
        }
        public List<WriteoffDetail> GetWriteoffDetails(int writeoffId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var writeoffDetails = context.WriteoffDetails.Where(e => e.WriteoffId == writeoffId && e.Status == RecordStatus.Active).ToList();
                return writeoffDetails.Select(Mapper.Map).ToList();
            }
        }
        public List<WriteoffDetail> GetWriteoffDetail(string productCode)
        {
            var writeoffDetails = new List<WriteoffDetail>();
            using (var context = DataObjectFactory.CreateContext())
            {
                var productDetails = context.ProductDetails.Where(e => (e.ProductCode.Equals(productCode) || e.Barcode.Equals(productCode)) && (e.Quantity + e.QuantityActual + e.QuantityLower) > 0).ToList();
                foreach (var productDetail in productDetails)
                {
                    var measure = productDetail.Product.ProductType.Measure;
                    writeoffDetails.Add(new WriteoffDetail
                    {
                        ProductDetailId = productDetail.ProductDetailId,
                        ProductCode = productDetail.ProductCode,
                        Name=productDetail.Product.Name,
                        OrderId=productDetail.OrderId,
                        UnitPrice = productDetail.UnitPrice,
                        Price = productDetail.Price,
                        NetPrice=productDetail.NetPrice,
                        Quantity=productDetail.Quantity,
                        QuantityActual=productDetail.QuantityActual,
                        QuantityLower=productDetail.QuantityLower,
                        SellingLower = productDetail.Product.SellingLower,
                        StockInHand = new StockInHand { Quantity = productDetail.Product.StockInHand, QuantityActual = productDetail.Product.QuantityActual, QuantityLower = productDetail.Product.QuantityLower },
                        RowStockInHand = new StockInHand { Quantity = productDetail.Quantity, QuantityActual = productDetail.QuantityActual, QuantityLower = productDetail.QuantityLower },
                        StockAs = productDetail.Product.ProductType.StockAs,
                        Volume = measure.Volume,
                        ContainsQty = productDetail.Product.ContainsQty,
                        Measure = new Measure { Actual = measure.Actual, Lower = measure.Lower },
                        CreatedOnText = productDetail.CreatedOn.ToShortDateString(),
                        ReceivedInfo = string.Format("{0} - {1} - {2}{3}{4}", productDetail.CreatedOn.ToShortDateString(), productDetail.Product.Name,
                        productDetail.Quantity > 0 ? string.Format("{0} {1}s ", productDetail.Quantity, productDetail.Product.ProductType.StockAs) : string.Empty,
                        productDetail.QuantityActual > 0 ? string.Format("{0} {1}s ", productDetail.QuantityActual, measure.Actual) : string.Empty,
                        productDetail.QuantityLower > 0 ? string.Format("{0} {1}s ", productDetail.QuantityLower, measure.Lower) : string.Empty)
                    });
                }
                return writeoffDetails;
            }
        }

        public int AddWriteoffDetailCart(WriteoffDetailCart writeoffDetailCart)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                writeoffDetailCart.WriteoffDetailGuid = Guid.NewGuid();
                writeoffDetailCart.CreatedOn = DateTime.Now;
                writeoffDetailCart.Status = RecordStatus.Active;
                var entity = Mapper.Map(writeoffDetailCart);

                if (writeoffDetailCart.WriteoffId.Equals(0))
                    entity.WriteoffId = AddWriteoff(context, writeoffDetailCart);

                if (!UpdateWriteoffDetailCart(writeoffDetailCart, context)) context.WriteoffDetailCarts.Add(entity);
                context.SaveChanges();
                return entity.WriteoffId;
            }
        }

        private bool UpdateWriteoffDetailCart(WriteoffDetailCart writeoffDetailCart, ConnectoManagerEntities context)
        {
            var cart = context.WriteoffDetailCarts.FirstOrDefault(e => e.WriteoffId == writeoffDetailCart.WriteoffId && e.WriteoffDetailId == writeoffDetailCart.WriteoffDetailId && e.ProductCode == writeoffDetailCart.ProductCode);
            if (cart == null) return false;
            cart.EmployeeId = writeoffDetailCart.EmployeeId;
            cart.Quantity = writeoffDetailCart.Quantity;
            cart.QuantityActual = writeoffDetailCart.QuantityActual;
            cart.QuantityLower = writeoffDetailCart.QuantityLower;
            cart.Price = writeoffDetailCart.Price;
            cart.NetPrice = writeoffDetailCart.NetPrice;
            cart.EditedBy = writeoffDetailCart.CreatedBy;
            cart.EditedOn = writeoffDetailCart.CreatedOn;
            return true;
        }

        public List<WriteoffDetailCart> GetWriteoffDetailsCart(int WriteoffId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var writeoffDetailCart = context.WriteoffDetailCarts.Where(e => e.WriteoffId == WriteoffId && e.Status == RecordStatus.Active).ToList();
                return writeoffDetailCart.Select(Mapper.Map).ToList();
            }
        }

        public int AddWriteoffDetail(int WriteoffId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var writeoffDetailsCart = context.WriteoffDetailCarts.Where(e => e.WriteoffId == WriteoffId && e.Status == RecordStatus.Active).ToList();
                var cartsToRemove = new List<EntityWriteoffDetailCart>();
                foreach (var item in writeoffDetailsCart)
                {
                    var productDetail = context.ProductDetails.FirstOrDefault(e => e.ProductDetailId == item.ProductDetailId);
                    if (productDetail == null) continue;

                    Stock.SyncStock(productDetail, item.Quantity, item.QuantityActual, item.QuantityLower, true);

                    context.WriteoffDetails.Add(Mapper.MapDiff(item));
                    cartsToRemove.Add(item);
                }

                if (cartsToRemove.Count <= 0) return cartsToRemove.Count;
                context.WriteoffDetailCarts.RemoveRange(writeoffDetailsCart);
                context.SaveChanges();
                return cartsToRemove.Count;
            }
        }
        internal int AddWriteoff(ConnectoManagerEntities context, WriteoffDetailCart item)
        {
            var entity = new EntityWriteoff
            {
                WriteoffGuid = Guid.NewGuid(),
                WriteoffDate = item.DateWriteoff,
                EmployeeId = item.EmployeeId,
                LocationId = item.LocationId,
                Status = item.Status,
                CreatedBy = item.CreatedBy,
                CreatedOn = item.CreatedOn,
            };
            context.Writeoffs.Add(entity);
            context.SaveChanges();
            return entity.WriteoffId;
        }
        public int DeleteWriteoffDetailCart(int id, int deletedBy)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.WriteoffDetailCarts.FirstOrDefault(s => s.WriteoffDetailId == id);
                entity.Status = RecordStatus.Deleted;
                entity.EditedOn = DateTime.Now;
                entity.EditedBy = deletedBy;
                return context.SaveChanges();
            }
        }

        public bool UpdateWriteoff(WriteoffDetailCart writeoffDetailCart)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var writeoff = context.Writeoffs.FirstOrDefault(e => e.WriteoffId == writeoffDetailCart.WriteoffId);
                if (writeoff == null) return false;
                writeoff.WriteoffDate = writeoffDetailCart.DateWriteoff;
                writeoff.EmployeeId = writeoffDetailCart.EmployeeId;
                writeoff.WriteoffDate = writeoffDetailCart.DateWriteoff;
                return context.SaveChanges() > 0;
            }
        }

        public  int DeleteWriteoffDetails(int id, int deletedBy)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var writeoffDetailItems = context.WriteoffDetails.Where(e => e.WriteoffId == id && e.Status == RecordStatus.Active).ToList();

                foreach (var item in writeoffDetailItems)
                {
                    item.Status = RecordStatus.Deleted;
                    item.EditedOn = DateTime.Now;
                    item.EditedBy = deletedBy;
                    context.SaveChanges();
                }
                return writeoffDetailItems.Count;
            }
        }
        public bool EditWriteoffDetailCart(WriteoffDetailCart cart)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.WriteoffDetailCarts.FirstOrDefault(s => s.WriteoffDetailId == cart.ProductDetailId);
                entity.ProductCode = cart.ProductCode;
                entity.Quantity = cart.Quantity;
                entity.UnitPrice = cart.UnitPrice;
                entity.EditedBy = cart.EditedBy;
                entity.EditedOn = cart.EditedOn;
                return context.SaveChanges() > 0;
            }
        }

        public List<WriteoffDetail> GetWriteoffedWriteoffDetailsCart(int writeoffId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var writeoffDetails = context.WriteoffDetails.Where(e => e.WriteoffId == writeoffId && e.Status == RecordStatus.Active).ToList();
                return writeoffDetails.Select(Mapper.Map).ToList();
            }
        }

        
    }
}
