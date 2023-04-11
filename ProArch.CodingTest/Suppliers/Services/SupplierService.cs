using ProArch.CodingTest.Exceptions;
using ProArch.CodingTest.Suppliers.Repository;

namespace ProArch.CodingTest.Suppliers.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public Supplier GetById(int id) =>
            _supplierRepository.Get(id)
                ?? throw new EntityNotFoundException($"Supplier {id} not found");
    }
}
