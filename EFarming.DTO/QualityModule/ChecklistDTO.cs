using EFarming.Common;
using EFarming.DTO.AdminModule;
using EFarming.DTO.FarmModule;
using System;

namespace EFarming.DTO.QualityModule
{
    public class ChecklistDTO : HistoricalDTO
    {
        public ChecklistDTO()
        {
            Date = DateTime.Now;

            // Initialize Data
            Farm = new FarmDTO();
            Farm.Village = new VillageDTO();
            Farm.Village.Municipality = new MunicipalityDTO();
            Farm.Village.Municipality.Department = new DepartmentDTO();
        }

        public string Observaciones { get; set; }

        public Guid UserId { get; set; }

        public Guid FarmId { get; set; }

        public string TechnicianSignatureUrl { get; set; }

        public string FarmerSignatureUrl { get; set; }

        public Guid AlmacenamientoId { get; set; }

        public Guid DespulpadoId { get; set; }

        public Guid FermentacionId { get; set; }

        public Guid InfraestucturaId { get; set; }

        public Guid MantenimientoId { get; set; }

        public Guid RecoleccionId { get; set; }

        public Guid SecadoId { get; set; }

        public FarmDTO Farm { get; set; }

        public UserDTO User{ get; set; }

        public AlmacenamientoDTO Almacenamiento { get; set; }

        public DespulpadoDTO Despulpado { get; set; }

        public FermentacionDTO Fermentacion { get; set; }

        public InfraestructuraDTO Infraestructura { get; set; }

        public MantenimientoDTO Mantenimiento { get; set; }

        public RecoleccionDTO Recoleccion { get; set; }

        public SecadoDTO Secado { get; set; }
    }
}
