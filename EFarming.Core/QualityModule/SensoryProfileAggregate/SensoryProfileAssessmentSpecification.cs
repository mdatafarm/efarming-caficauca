using EFarming.Core.AdminModule.AssessmentAggregate;
using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;

using System;
using System.Linq;

namespace EFarming.Core.QualityModule.SensoryProfileAggregate
{
    /// <summary>
    /// SensoryProfileAssessment Specification 
    /// </summary>
    public static class SensoryProfileAssessmentSpecification
    {
        /// <summary>
        /// Filters the by farm.
        /// </summary>
        /// <param name="id">The identifier.</param>        
        /// <returns></returns>
        public static Specification<SensoryProfileAssessment> FilterByFarm(Guid id)
        {
            Specification<SensoryProfileAssessment> spec = new TrueSpecification<SensoryProfileAssessment>();

            if (Guid.Empty != id)
                spec &= new DirectSpecification<SensoryProfileAssessment>(spa => spa.FarmId.Value.Equals(id));

            return spec;
        }

        /// <summary>
        /// Filter the farm by template
        /// </summary>
        /// <param name="id">ID of Farm</param>
        /// <param name="templateId">ID Template</param>
        /// <returns></returns>
        public static Specification<SensoryProfileAssessment> FilterByFarmAndTemplate(Guid id, Guid templateId)
        {
            Specification<SensoryProfileAssessment> spec = new TrueSpecification<SensoryProfileAssessment>();

            if (Guid.Empty != id)
            {
                spec &= new DirectSpecification<SensoryProfileAssessment>(spa => spa.FarmId.Value.Equals(id));
                spec &= new DirectSpecification<SensoryProfileAssessment>(spa => spa.AssessmentTemplateId.Equals(templateId));
            }

            return spec;
        }

        /// <summary>
        /// Tracks the by location.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        public static Specification<SensoryProfileAssessment> TrackByLocation(int year)
        {
            Specification<SensoryProfileAssessment> spec = new TrueSpecification<SensoryProfileAssessment>();
            spec &= new DirectSpecification<SensoryProfileAssessment>(ia => ia.Date.Year == year);
            return spec;
        }

        /// <summary>
        /// Tracks the by department.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        public static Specification<SensoryProfileAssessment> TrackByDepartment(int year, Guid departmentId)
        {
            Specification<SensoryProfileAssessment> spec = TrackByLocation(year);
            spec &= new DirectSpecification<SensoryProfileAssessment>(ia => ia.Farm.Village.Municipality.DepartmentId.Equals(departmentId));
            return spec;
        }

        /// <summary>
        /// Filters the by range and taster.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="tasterId">The taster identifier.</param>
        /// <param name="assessmentTemplateId">The assessment template identifier.</param>
        /// <returns></returns>
        public static Specification<SensoryProfileAssessment> FilterByRangeAndTaster(DateTime? start, DateTime? end, Guid? tasterId, Guid assessmentTemplateId)
        {
            Specification<SensoryProfileAssessment> spec = new TrueSpecification<SensoryProfileAssessment>();
            if (start.HasValue)
            {
                spec &= new DirectSpecification<SensoryProfileAssessment>(spa => spa.Date >= start.Value);
            }
            if (end.HasValue)
            {
                spec &= new DirectSpecification<SensoryProfileAssessment>(spa => spa.Date <= end.Value);
            }
            if (tasterId.HasValue)
            {
                spec &= new DirectSpecification<SensoryProfileAssessment>(spa => spa.UserId.Equals(tasterId.Value));
            }
            spec &= new DirectSpecification<SensoryProfileAssessment>(spa => spa.AssessmentTemplateId.Equals(assessmentTemplateId));
            return spec;
        }

        /// <summary>
        /// Filters the by user and description.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static Specification<SensoryProfileAssessment> FilterByUserAndDescription(string description, Guid? userId)
        {
            Specification<SensoryProfileAssessment> spec = new TrueSpecification<SensoryProfileAssessment>();

            if (userId.HasValue)
            {
                spec &= new DirectSpecification<SensoryProfileAssessment>(spa => spa.UserId.Equals(userId.Value));
            }
            if (!string.IsNullOrEmpty(description))
            {
                spec &= new DirectSpecification<SensoryProfileAssessment>(spa => spa.Description.Equals(description));
            }

            return spec;
        }
        public static Specification<SensoryProfileAssessment> FilterByAnything(  Guid? assessmentTemplateId, Guid? villageId, Guid? municipalityId,
            Guid? departmentId, string farmCode, Guid? tasterId)
        {

            Specification<SensoryProfileAssessment> spec = new TrueSpecification<SensoryProfileAssessment>();

            //El filtro por defecto es que la muestra contenga una finca 
            spec &= new DirectSpecification<SensoryProfileAssessment>(spa => spa.Farm != null);
            
            if (departmentId.HasValue && !departmentId.Value.Equals(Guid.Empty))
            {
                spec &= new DirectSpecification<SensoryProfileAssessment>(ia => ia.Farm.Village.Municipality.DepartmentId.Equals(departmentId.Value));
            }
            if (municipalityId.HasValue && !municipalityId.Value.Equals(Guid.Empty))
            {
                spec &= new DirectSpecification<SensoryProfileAssessment>(ia => ia.Farm.Village.Municipality.Id.Equals(municipalityId.Value));
            }
            if (villageId.HasValue && !villageId.Value.Equals(Guid.Empty))
             {
                spec &= new DirectSpecification<SensoryProfileAssessment>(ia => ia.Farm.Village.Id.Equals(villageId.Value));
            }
            if (farmCode!= null && farmCode != "")
            {
                spec &= new DirectSpecification<SensoryProfileAssessment>(ia => ia.Farm.Code.Equals(farmCode));
            }
            if (tasterId.HasValue && !tasterId.Value.Equals(Guid.Empty))
            {
                spec &= new DirectSpecification<SensoryProfileAssessment>(ia => ia.UserId.Equals(tasterId.Value));
            }
            
            return spec;
        }
        public static Specification<SensoryProfileAssessment> FilterById(Guid Id)
        {
            Specification<SensoryProfileAssessment> spec = new TrueSpecification<SensoryProfileAssessment>();
            //El filtro por defecto es que la muestra contenga una finca 
            spec &= new DirectSpecification<SensoryProfileAssessment>(spa => spa.Farm != null);
            if ( !Id.Equals(Guid.Empty))
            {
                spec &= new DirectSpecification<SensoryProfileAssessment>(ia => ia.Id.Equals(Id));
            }
            return spec;
        }
    }
}
