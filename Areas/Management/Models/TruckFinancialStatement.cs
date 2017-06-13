using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MaaseShooei.Areas.Management.Models
{
    public partial class T_TruckFinancialStatements
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name="ID")]
        public long _TruckFinancialStatementID { get { return TruckFinancialStatementID; } set { TruckFinancialStatementID = value; } }

        [Display(Name = "از تاریخ")]
        [Required]
        public string _FromDate { get { return FromDate; } set { FromDate = value; } }

        [Display(Name = "تا تاریخ")]
        [Required]
        public string _ToDate { get { return ToDate; } set { ToDate = value; } }

        [Display(Name = "توضیحات")]
        public string _Descryption { get { return Descryption; } set { Descryption = value; } }

        [Display(Name = "ماشین")]
        [Required]
        public int _TruckID { get { return TruckID; } set { TruckID = value; } }

        //[Display(Name = "تولید کننده")]
        //public string _Number { get { return T_Trucks.Number; } }

        [Display(Name = "وضعیت")]
        public short _StateID { get { return StateID; } set { StateID = value; } }


        [Display(Name = "تعداد حواله ها")]
        public int _BurdonsCount { get { return T_BurdenInformations.Count; } }


        [Display(Name = "مبلغ کل ")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Currency)]
        public double _BurdonsSumPrices
        {
            get
            {

                decimal sumOfPrices = 0;

                foreach (var item in T_BurdenInformations.ToList())
                {
                    sumOfPrices += (item.T_TransportPrices.Price / 1000) * decimal.Parse((item.UnLoadFullTruckWeight - item.UnLoadFreeTruckWeight).ToString()) ;
                }


                return double.Parse( sumOfPrices.ToString());

            }


        }

        [Display(Name = "مبلغ بستانکاری ")]
        //[DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public double _Creditor
        {
            get
            {




                return _BurdonsSumPrices - _Payed;

            }


        }

        [Display(Name = "مبلغ پرداختی ")]
        //[DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public double _Payed
        {
            get
            {

                decimal sumOfPrices = 0;

                foreach (var item in T_TrucksPays.ToList())
                {
                    sumOfPrices += item.Price;
                }


                return double.Parse( sumOfPrices.ToString());

            }


        }
    
    }
}
