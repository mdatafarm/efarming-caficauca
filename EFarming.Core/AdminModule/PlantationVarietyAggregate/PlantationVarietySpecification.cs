using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;
using System;

namespace EFarming.Core.AdminModule.PlantationVarietyAggregate
{
    /// <summary>
    /// PlantationVariety Spacification
    /// </summary>
    public static class PlantationVarietySpecification
    {
        /// <summary>
        /// Filters the plantation variety.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="plantationTypeId">The plantation type identifier.</param>
        /// <returns>the result</returns>
        public static Specification<PlantationVariety> FilterPlantationVariety(string name, Guid plantationTypeId)
        {
            Specification<PlantationVariety> spec = new TrueSpecification<PlantationVariety>();

            if (!string.IsNullOrEmpty(name))
            {
                spec &= new DirectSpecification<PlantationVariety>(pv => pv.Name.ToUpper().Contains(name.ToUpper()));
            }

            if (plantationTypeId != null && Guid.Empty != plantationTypeId)
            {
                if (plantationTypeId == new Guid("d221bec9-5f73-43a0-9ebf-16417f5674f5"))
                {
                    spec &= new DirectSpecification<PlantationVariety>(pv => pv.PlantationTypeId.Equals(plantationTypeId));
                }
                else if (plantationTypeId == new Guid("46496de0-3cd6-4f25-beb7-2446d0f6929d"))
                {

                    spec &= new DirectSpecification<PlantationVariety>(pv => pv.Id == new Guid("55959369-0611-4180-adf9-2cf5a602420a"));
                }
                else if (plantationTypeId == new Guid("80838f7d-a18e-4f25-aea3-5550ff159eb2"))
                {
                    spec &= new DirectSpecification<PlantationVariety>(pv => pv.Id == new Guid("50bd3880-3c7f-4329-855e-2c75f1757b4f"));
                }
                else if (plantationTypeId == new Guid("9e7b7f6b-03c9-49eb-aafd-5db7f4e8337e"))
                {
                    spec &= new DirectSpecification<PlantationVariety>(pv => pv.Id == new Guid("f781c50b-c895-488c-9563-e61227f8ece0"));
                }
                else if (plantationTypeId == new Guid("067EC55A-7B4B-436B-83DF-713B26390929"))
                {
                    spec &= new DirectSpecification<PlantationVariety>(pv => pv.Id == new Guid("6279c725-f82f-4057-a970-a64c9531d961"));
                }
                else if (plantationTypeId == new Guid("B6CF7A3D-6070-4A16-9714-8CE60734D2AF"))
                {
                    spec &= new DirectSpecification<PlantationVariety>(pv => pv.Id == new Guid("b8b8cf51-22a8-4f35-8277-e48dcdd967cf"));
                }
                else if (plantationTypeId == new Guid("ee9582c6-46f2-4c99-bca2-a307a09c5973"))
                {
                    spec &= new DirectSpecification<PlantationVariety>(pv => pv.Id == new Guid("e6539371-25e7-46f2-a812-615e79938c12"));
                }
                else if (plantationTypeId == new Guid("260C7669-F515-442E-BE64-DCBBC539A991"))
                {
                    spec &= new DirectSpecification<PlantationVariety>(pv => pv.Id == new Guid("2fcb2716-213a-45f4-9faa-5e0246bcf57e"));
                }
                else if (plantationTypeId == new Guid("2CD08524-1BE7-49D3-B67D-E7E691001F67"))
                {
                    spec &= new DirectSpecification<PlantationVariety>(pv => pv.Id == new Guid("9d198f69-e10a-4e48-a348-5b8408e9badb"));
                }

            }

            return spec;
        }


    }
}
