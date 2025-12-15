using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;
using System;
using System.Linq;

namespace EFarming.Core.FarmModule.FarmAggregate
{
    /// <summary>
    /// Farm specification
    /// 
    /// Use for search atributes in the farm
    /// </summary>
    public static class FarmSpecification
    {
        /// <summary>
        /// Metodo de la especificacion para buscar por el codigo
        /// </summary>
        /// <param name="code">Codigo de la finca</param>
        /// <returns> Farm </returns>
        public static Specification<Farm> ByExactCode(string code)
        {
            Specification<Farm> spec = new TrueSpecification<Farm>();
            if (!string.IsNullOrEmpty(code))
            {
	      // adicion de la especificacion. 
                spec &= new DirectSpecification<Farm>(f => f.Code.ToUpper().Equals(code.ToUpper()));
            }
            return spec; // retorna la finca

        }

        /// <summary>
        /// Filtrado por cualquiera de los atributos mandados.
        /// </summary>
        /// <param name="code">Codigo de la finca</param>
        /// <param name="name">Nombre de la finca</param>
        /// <param name="farmSubstatusId">The farm substatus identifier.</param>
        /// <param name="cooperativeId">Cooperativa</param>
        /// <param name="ownershipTypeId">The ownership type identifier.</param>
        /// <param name="villageId">Vereda</param>
        /// <param name="municipalityId">Municipio</param>
        /// <param name="departmentId">Departamento</param>
        /// <returns> Farm </returns>
        public static Specification<Farm> Filter(string code, string name, Guid? farmSubstatusId,
            Guid? cooperativeId, Guid? ownershipTypeId, Guid? villageId, Guid? municipalityId, Guid? departmentId)
        {
            Specification<Farm> spec = new TrueSpecification<Farm>();

            if (!string.IsNullOrEmpty(name))
            {
                spec &= new DirectSpecification<Farm>(f => f.Name.ToUpper().Contains(name.ToUpper()));
            }

            if (!string.IsNullOrEmpty(code))
            {
                spec &= new DirectSpecification<Farm>(f => f.Code.ToUpper().Contains(code.ToUpper()));
            }

            if (farmSubstatusId.HasValue && !farmSubstatusId.Value.Equals(Guid.Empty))
            {
                spec &= new DirectSpecification<Farm>(f => f.FarmSubstatusId.Equals(farmSubstatusId.Value));
            }

            if (cooperativeId.HasValue && !cooperativeId.Value.Equals(Guid.Empty))
            {
                spec &= new DirectSpecification<Farm>(f => f.CooperativeId.Equals(cooperativeId.Value));
            }

            if (ownershipTypeId.HasValue && !ownershipTypeId.Value.Equals(Guid.Empty))
            {
                spec &= new DirectSpecification<Farm>(f => f.OwnershipTypeId.Equals(ownershipTypeId.Value));
            }

            if (villageId.HasValue && !villageId.Value.Equals(Guid.Empty))
            {
                spec &= new DirectSpecification<Farm>(f => f.VillageId.Equals(villageId.Value));
            }
            else if (municipalityId.HasValue && !municipalityId.Value.Equals(Guid.Empty))
            {
                spec &= new DirectSpecification<Farm>(f => f.Village.MunicipalityId.Equals(municipalityId.Value));
            }
            else if (departmentId.HasValue && !departmentId.Value.Equals(Guid.Empty))
            {
                spec &= new DirectSpecification<Farm>(f => f.Village.Municipality.DepartmentId.Equals(departmentId.Value));
            }

            return spec;
        }


        /// <summary>
        /// Filters the with farmer information.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="name">The name.</param>
        /// <param name="farmSubstatusId">The farm substatus identifier.</param>
        /// <param name="cooperativeId">The cooperative identifier.</param>
        /// <param name="ownershipTypeId">The ownership type identifier.</param>
        /// <param name="villageId">The village identifier.</param>
        /// <param name="municipalityId">The municipality identifier.</param>
        /// <param name="departmentId">The department identifier.</param>
        /// <param name="farmerName">Name of the farmer.</param>
        /// <param name="farmerIdentification">The farmer identification.</param>
        /// <returns>Farm</returns>
        public static Specification<Farm> FilterWithFarmerInfo(string code, string name, Guid? farmSubstatusId,
            Guid? cooperativeId, Guid? ownershipTypeId, Guid? villageId, Guid? municipalityId, 
            Guid? departmentId, string farmerName)
        {
            Specification<Farm> spec = Filter(code, name, farmSubstatusId, cooperativeId,
                                                ownershipTypeId, villageId, municipalityId, departmentId);

            if (!string.IsNullOrEmpty(farmerName))
            {
                spec &= new DirectSpecification<Farm>(
                    f => f.FamilyUnitMembers
                        .Where(u => u.FirstName.ToUpper().Contains(farmerName.ToUpper()) || u.LastName.ToUpper().Contains(farmerName.ToUpper()))
                        .Select(u => u.FarmId)
                        .Contains(f.Id));
            }

            return spec;
        }

        /// <summary>
        /// Filtrar para el tablero de datos
        /// </summary>
        /// <param name="supplyChainId">Proveedor</param>
        /// <param name="supplierId">Closter</param>
        /// <param name="countryId">Pais</param>
        /// <returns>Las finas que pertenecen al grupo solicitado </returns>
        public static Specification<Farm> FilterDashboard(Guid? supplyChainId, Guid? supplierId, Guid? countryId)
        {
            Specification<Farm> spec = new TrueSpecification<Farm>();
            spec &= new DirectSpecification<Farm>(f => f.Productivity != null);

            if (supplyChainId.HasValue && !supplyChainId.Value.Equals(Guid.Empty))
            {
                spec &= new DirectSpecification<Farm>(f => f.SupplyChain != null 
                                    && f.SupplyChain.Id.Equals(supplyChainId.Value));
            }
            else if (supplierId.HasValue && !supplierId.Value.Equals(Guid.Empty))
            {
                spec &= new DirectSpecification<Farm>(f => f.SupplyChain != null 
                                    && f.SupplyChain.Supplier != null 
                                    && f.SupplyChain.Supplier.Id.Equals(supplierId.Value));
            }
            else if (countryId.HasValue && !countryId.Value.Equals(Guid.Empty))
            {
                spec &= new DirectSpecification<Farm>(f => f.SupplyChain != null
                                    && f.SupplyChain.Supplier != null
                                    && f.SupplyChain.Supplier.Country != null
                                    && f.SupplyChain.Supplier.Country.Id.Equals(countryId.Value));
            }

            return spec;
        }


        /// <summary>
        /// Bies the size.
        /// </summary>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <param name="minInclusive">if set to <c>true</c> [minimum inclusive].</param>
        /// <param name="maxInclusive">if set to <c>true</c> [maximum inclusive].</param>
        /// <returns>Farm</returns>
        public static Specification<Farm> BySize(double? minimum, double? maximum, bool minInclusive = false, bool maxInclusive = false)
        {
            Specification<Farm> spec = new TrueSpecification<Farm>();
            if (minimum.HasValue)
            {
                if(minInclusive)
                    spec &= new DirectSpecification<Farm>(f => Convert.ToDouble(f.Productivity.TotalHectares) >= minimum.Value);
                else
                    spec &= new DirectSpecification<Farm>(f => Convert.ToDouble(f.Productivity.TotalHectares) > minimum.Value);
            }
            if (maximum.HasValue)
            {
                if(maxInclusive)
                    spec &= new DirectSpecification<Farm>(f => Convert.ToDouble(f.Productivity.TotalHectares) <= maximum.Value);
                else
                    spec &= new DirectSpecification<Farm>(f => Convert.ToDouble(f.Productivity.TotalHectares) < maximum.Value);
            }
            return spec;
        }
    }
}
