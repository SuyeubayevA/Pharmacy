namespace Pharmacy.Infrastructure.Data.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }

        //Foreign Key
        public int? ProductTypeId { get; set; }
        public int? SalesInfoId { get; set; }
    }

    public class ProductDetailDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }

        //Foreign Key
        public int? ProductTypeId { get; set; }
        public int? SalesInfoId { get; set; }

        //Navigation Property
        public string ProductTypeName { get; set; }
        public string ProductTypeProperties { get; set; }

        public int Sales { get; set; }
        public int ProductReminder { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? EditDate { get; set; }
        public virtual ICollection<ProductAmountDetailsDTO>? ProductAmounts { get; private set; }
    }
}
