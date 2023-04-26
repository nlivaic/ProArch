using ProArch.CodingTest.Suppliers;

namespace ProArch.CodingTest.UnitTests.Builders
{
    internal class SupplierBuilder
    {
        private readonly Supplier _target = new();

        public SupplierBuilder AsSupplier()
        {
            _target.IsExternal = false;
            return this;
        }

        public SupplierBuilder AsExternalSupplier()
        {
            _target.IsExternal = true;
            return this;
        }

        public Supplier Create() => _target;
    }
}
