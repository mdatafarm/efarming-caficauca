using EFarming.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DTO.FertilizersCalculatorModule
{
    public class FertilizerInformationDTO : EntityWithIntIdDTO
    {
        public string Name { get; set; }

        private string _kg;
        public string kg
        {
            get { return _kg; }
            set { _kg = value.Replace(",", "."); }
        }
        private string _Price;
        public string Price
        {
            get { return _Price; }
            set { _Price = value.Replace(".", ","); }
        }
        private string _N;
        public string N
        {
            get { return _N; }
            set { _N = value.Replace(".", ","); }
        }
        private string _P2O5;
        public string P2O5
        {
            get { return _P2O5; }
            set { _P2O5 = value.Replace(".", ","); }
        }
        private string _K20;
        public string K20
        {
            get { return _K20; }
            set { _K20 = value.Replace(".", ","); }
        }
        private string _CaO;
        public string CaO
        {
            get { return _CaO; }
            set { _CaO = value.Replace(".", ","); }
        }
        private string _MgO;
        public string MgO
        {
            get { return _MgO; }
            set { _MgO = value.Replace(".", ","); }
        }
        private string _SO4;
        public string SO4
        {
            get { return _SO4; }
            set { _SO4 = value.Replace(".", ","); }
        }
        private string _B;
        public string B
        {
            get { return _B; }
            set { _B = value.Replace(".", ","); }
        }
        private string _Zn;
        public string Zn
        {
            get { return _Zn; }
            set { _Zn = value.Replace(".", ","); }
        }
        private string _Cu;
        public string Cu
        {
            get { return _Cu; }
            set { _Cu = value.Replace(".", ","); }
        }
        private string _Fe;
        public string Fe
        {
            get { return _Fe; }
            set { _Fe = value.Replace(".", ","); }
        }
        private string _Mn;
        public string Mn
        {
            get { return _Mn; }
            set { _Mn = value.Replace(".", ","); }
        }
        private string _Mo;
        public string Mo
        {
            get { return _Mo; }
            set { _Mo = value.Replace(".", ","); }
        }
        private string _SiO;
        public string SiO
        {
            get { return _SiO; }
            set { _SiO = value.Replace(".", ","); }
        }
    }
}
