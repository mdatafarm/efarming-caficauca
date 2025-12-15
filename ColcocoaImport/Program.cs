using Castle.Windsor;
using EFarmingConsole.Dependency;
using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.DTO.AdminModule;
using EFarming.DTO.FarmModule;
using EFarming.DTO.TraceabilityModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Implementation;
using EFarming.Manager.Implementation.AdminModule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Common.Consts;
using EFarming.DTO.QualityModule;

namespace EFarmingConsole
{
    class Program
    {
        static IWindsorContainer container;
        static CooperativeDTO Coop;
        static int op = 1;

        static void Main(string[] args)
        {
            Console.WriteLine("Loading dependencies...");

            container =
                new WindsorContainer().Install(new DependencyConventions());

            switch (Menu())
            {
                case 1:
                    LoadFarms();
                    break;
                case 2:
                    LoadInvoices();
                    break;
                case 3:
                    DuplicateAssessments();
                    break;
                case 4:
                    RecreateQualityAttributes();
                    break;
                case 5:
                    DistributeFarms();
                    break;
                case 6:
                    RegenerateCharts();
                    break;
                case 7:
                    RemoveDuplicatedFamily();
                    break;
            }
            Console.ReadLine();
        }

        private static void RemoveDuplicatedFamily()
        {
            var farmManager = container.Resolve<IFarmManager>();
            var all = farmManager.Find();
            foreach (var farm in all)
            {
                var grouped = farm.FamilyUnitMembers.GroupBy(m => m.FullName);
                foreach (var group in grouped)
                {
                    if (group.Count() > 1)
                    {
                        group.OrderBy(m => m.LastName).Take(group.Count() - 1).ToList().ForEach(m => farm.FamilyUnitMembers.Remove(m));
                    }
                }
                farmManager.Edit(farm.Id, farm, FarmManager.FAMILY_UNIT_MEMBERS);
            }
        }

        private static void RegenerateCharts()
        {
            var manager = container.Resolve<ICountryManager>();
            var dashboardManager = container.Resolve<IDashboardManager>();
            var countries = manager.GetAll(new string[] { "Suppliers", "Suppliers.SupplyChains" });

            foreach (var country in countries)
            {
                foreach (var supplier in country.Suppliers)
                {
                    foreach (var supplyChain in supplier.SupplyChains)
                    {
                        Console.WriteLine("Supply chain " + supplyChain.Name);
                        dashboardManager.EvolutionFarms(country.Id, supplier.Id, supplyChain.Id);
                        Console.WriteLine("Supply chain " + supplyChain.Name + " finished!");
                    }
                    Console.WriteLine("Supplier" + supplier.Name);
                    dashboardManager.EvolutionFarms(country.Id, supplier.Id);
                    Console.WriteLine("Supplier" + supplier.Name + " finished!");
                }
                Console.WriteLine("Country " + country.Name);
                dashboardManager.EvolutionFarms(country.Id);
                Console.WriteLine("Country " + country.Name + " finished!");
            }
            Console.WriteLine("General");
            dashboardManager.EvolutionFarms();
            Console.WriteLine("General finished!");
        }

        private static void RecreateQualityAttributes()
        {
            var manager = container.Resolve<IQualityAttributeManager>();
            var tempManager = container.Resolve<IAssessmentTemplateManager>();
            var temps = tempManager.GetAll();
            foreach (var temp in temps)
            {
                var attributes = manager.Get(temp.Id);
                foreach (var attribute in attributes)
                {
                    if (attribute.TypeOf.Equals(QualityAttributeTypes.OPEN_TEXT) && attribute.OpenTextAttribute == null)
                    {
                        attribute.OpenTextAttribute = new OpenTextAttributeDTO();
                        manager.Edit(attribute);
                    }
                }
            }
        }

        private static void DuplicateAssessments()
        {
            var manager = container.Resolve<IImpactManager>();
            int count = 0;
            var assessments = manager.GetAll(2012);
            Console.WriteLine("Assessments encontrados: " + assessments.Count());
            foreach (var assessment in assessments)
            {
                assessment.Id = Guid.NewGuid();
                var ans = assessment.Answers;
                assessment.Answers = null;
                assessment.AssessmentTemplate = null;
                assessment.Description = "NOVIEMBRE 2013";
                assessment.Date = assessment.Date.AddYears(1);
                manager.Add(assessment);
                assessment.Answers = ans;
                manager.Edit(assessment.Id, assessment);
                count++;
                Console.WriteLine(count + " Assessment duplicado");
            }
        }

        private static void LoadInvoices()
        {
            var invoiceManager = container.Resolve<InvoiceManager>();
            var lotManager = container.Resolve<LotManager>();
            var farmManager = container.Resolve<FarmManager>();

            string[] lines = File.ReadAllLines(@"C:\Users\JuanGuillermo\Documents\Desarrollo\EFarming\imports\montebonito-invoices-2014.csv");
            foreach (var line in lines)
            {
                string[] fields = line.Split(';');

                var farm = farmManager.GetAll(FarmSpecification.Filter(fields[2], "", null, null, null, null, null, null), f => f.Code).FirstOrDefault();
                if (farm == null)
                {
                    Console.WriteLine(fields[2] + ";" + fields[3] + ";" + fields[4]);
                    continue;
                }
                var lot = lotManager.GetAll().FirstOrDefault(l => l.Code.ToUpper().Equals(fields[10].ToUpper()));
                if (lot == null)
                {
                    lotManager.Add(new LotDTO { Code = fields[10].ToUpper() });
                    lot = lotManager.GetAll().FirstOrDefault(l => l.Code.ToUpper().Equals(fields[10].ToUpper()));
                }
                var invoice = new InvoiceDTO
                {
                    CreatedAt = DateTime.Now,
                    Date = DateTime.Parse(fields[0]),
                    FarmId = farm.Id,
                    Identification = fields[2],
                    Value = Double.Parse(fields[5]),
                    Weight = Double.Parse(fields[4]),
                    InvoiceNumber = Int32.Parse(fields[1])
                };
                invoiceManager.Add(invoice);
            }
        }

        private static void LoadFarms()
        {
            var manager = container.Resolve<FarmManager>();

            string[] lines = File.ReadAllLines(@"C:\Users\JuanGuillermo\Documents\Desarrollo\EFarming\imports\cfx\FarmsNN.csv");

            var count = 1;

            var farmManager = container.Resolve<FarmManager>();
            var assessmentManager = container.Resolve<ImpactManager>();
            var indicatorManager = container.Resolve<IndicatorManager>();

            foreach (var line in lines)
            {
                try
                {
                    string[] fields = line.Split(';');

                    var coop = GetCooperative(fields[7]);
                    var village = GetVillage(fields[0], fields[1], fields[2]);
                    var own = GetOwnershipType(string.Empty);

                    var farm = new FarmDTO
                    {
                        Code = fields[3],
                        CooperativeId = coop.Id,
                        Elevation = string.Empty,
                        Latitude = string.Empty,
                        Longitude = string.Empty,
                        Name = fields[4],
                        VillageId = village.Id,
                        OwnershipTypeId = own.Id,
                        SupplyChainId = Guid.Parse("c8be0458-39c5-40a2-a3af-31387fd3d14c")
                    };

                    farm.Worker.Id = farm.Id;
                    farm.Productivity.Id = farm.Id;
                    farm.FamilyUnitMembers = new List<FamilyUnitMemberDTO>();
                    // Family Members
                    farm.FamilyUnitMembers.Add(new FamilyUnitMemberDTO
                    {
                        Age = DateTime.Now,
                        FarmId = farm.Id,
                        FirstName = fields[9].Length > 32 ? fields[9].Substring(0, 32) : fields[9],
                        LastName = fields[10].Length > 32 ? fields[10].Substring(0, 32) : fields[10],
                        Identification = fields[8].Length > 16 ? fields[8].Substring(0, 16) : fields[8],
                        IsOwner = true
                    });
                    manager.CreateWithoutCommit(farm);
                    Console.Write(".");

                    if (count % 200 == 0)
                    {
                        manager.Commit();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                count++;
            }
            manager.Commit();
        }

        private static void DistributeFarms()
        {
            var farmManager = container.Resolve<IFarmManager>();
            var supplyChainManager = container.Resolve<ISupplyChainManager>();

            var supplychains = supplyChainManager.GetAll().ToArray();
            var farms = farmManager.Find().ToArray();

            var perChain = farms.Length / supplychains.Length;
            var count = 1;
            for (int i = 1; i <= farms.Length; i++)
            {
                var farm = farms[i - 1];
                var sc = supplychains[count - 1];
                Console.WriteLine(sc.Name);
                farm.SupplyChainId = sc.Id;
                farmManager.Edit(farm.Id, farm, FarmManager.FARMS);
                if (i % perChain == 0)
                {
                    count++;
                }
            }
        }

        private static OwnershipTypeDTO GetOwnershipType(string p)
        {
            p = string.IsNullOrEmpty(p) ? "propia" : p;
            var ownManager = container.Resolve<OwnershipTypeManager>();

            var own = ownManager.GetAll().FirstOrDefault(o => o.Name.Equals(p.ToUpper()));
            if (own == null)
            {
                ownManager.Create(new OwnershipTypeDTO { Name = p });
                own = ownManager.GetAll().FirstOrDefault(o => o.Name.Equals(p.ToUpper()));
            }
            return own;
        }

        private static VillageDTO GetVillage(string dept, string muni, string villa)
        {
            var deptManager = container.Resolve<DepartmentManager>();
            var muniManager = container.Resolve<MunicipalityManager>();
            var villaManager = container.Resolve<VillageManager>();

            var department = deptManager.GetAll().FirstOrDefault(d => d.Name.Equals(dept.ToUpper()));
            if (department == null)
            {
                deptManager.Create(new DepartmentDTO
                {
                    Name = dept
                });
                department = deptManager.GetAll().FirstOrDefault(d => d.Name.Equals(dept.ToUpper()));
            }

            var municipality = muniManager.GetAll().FirstOrDefault(m => m.Name.Equals(muni.ToUpper()));
            if (municipality == null)
            {
                muniManager.Create(new MunicipalityDTO
                {
                    Name = muni,
                    DepartmentId = department.Id
                });
                municipality = muniManager.GetAll().FirstOrDefault(m => m.Name.Equals(muni.ToUpper()));
            }

            var village = villaManager.GetAll().FirstOrDefault(v => v.Name.Equals(villa.ToUpper()));
            if (village == null)
            {
                villaManager.Create(new VillageDTO
                {
                    Name = villa,
                    MunicipalityId = municipality.Id
                });
                village = villaManager.GetAll().FirstOrDefault(v => v.Name.Equals(villa.ToUpper()));
            }
            return village;
        }

        private static CooperativeDTO GetCooperative(string cooperative)
        {
            var coopManager = container.Resolve<CooperativeManager>();
            var coop = coopManager.GetAll().FirstOrDefault(c => c.Name.Equals(cooperative.ToUpper()));

            if (coop == null)
            {
                coopManager.Create(new CooperativeDTO
                {
                    Name = cooperative
                });
            }
            return coopManager.GetAll().FirstOrDefault(c => c.Name.Equals(cooperative.ToUpper()));
        }

        private static int Menu()
        {
            int op = 0;

            Console.WriteLine("\tMenu");
            Console.WriteLine("1. Load farms");
            Console.WriteLine("2. Load invoices");
            Console.WriteLine("3. Duplicate assessments");
            Console.WriteLine("4. Recreate quality attributes");
            Console.WriteLine("5. Distribute farms across other countries");
            Console.WriteLine("6. Regenerate charts");
            Console.WriteLine("7. Remove duplicate family");
            Console.Write("Choose -> ");
            op = int.Parse(Console.ReadLine());

            return op;
        }
    }
}
