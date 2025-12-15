using EFarming.DAL;
using EFarming.Manager.Implementation;
using EFarming.Repository.TraceabilityModule;
using EFarming.Web.Controllers;
using EFarming.Web.Coocentral;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace EFarming.Web.Util
{
    public class UpdateInvoicesJob : IJob
    {
        private UnitOfWork db = new UnitOfWork();

        public async void Execute(IJobExecutionContext context)
        {
            UpdateDataController UpdateInvoiceInformation = new UpdateDataController();
            var Invoices = await UpdateInvoiceInformation.UpdateInvoiceInformation();
            UpdateDataController UpdateFertilizerInformation = new UpdateDataController();
            var Fertilizers = await UpdateFertilizerInformation.UpdateFertilizerInformation();
        }
    }
}