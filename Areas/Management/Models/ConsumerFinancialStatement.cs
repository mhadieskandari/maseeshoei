using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MaaseShooei.Areas.Management.Models
{
    public partial class T_ConsumerFinancialStatements
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name="ID")]
        public long _ConsumerFinancialStatementID { get { return ConsumerFinancialStatementID; } set { ConsumerFinancialStatementID = value; } }

        [Display(Name = "از تاریخ")]
        [Required]
        public string _FromDate { get { return FromDate; } set { FromDate = value; } }

        [Display(Name = "تا تاریخ")]
        [Required]
        public string _ToDate { get { return ToDate; } set { ToDate = value; } }

        [Display(Name = "توضیحات")]
        public string _Descryption { get { return Descryption; } set { Descryption = value; } }

        [Display(Name = "مشتری")]
        [Required]
        public int _ConsumerID { get { return ConsumerID; } set { ConsumerID = value; } }

        //[Display(Name = "مشتری")]
        //public string _CompanyName { get { return T_Consumers.CompanyName; } }

        [Display(Name = "وضعیت")]
        public short _StateID { get { return StateID; } set { StateID = value; } }

        [Display(Name = "تعداد حواله ها")]
        public int _BurdonsCount { get {return T_BurdenInformations.Count; } }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        [Display(Name = "مبلغ کل ")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public double _BurdonsSumPrices
        {
            get
            {

                decimal sumOfPrices = 0;

                foreach (var item in T_BurdenInformations.ToList())
                {
                    sumOfPrices += (item.T_ConsumerProducePrices.Price / 1000 )*decimal.Parse( (item.UnLoadFullTruckWeight - item.UnLoadFreeTruckWeight).ToString());
                }


                return double.Parse( sumOfPrices.ToString());

            }


        }
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        [Display(Name = "مبلغ بستانکاری ")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public double _Creditor 
        {
            get
            {




                return _BurdonsSumPrices - _Payed;

            }


        }

        [Display(Name = "مبلغ پرداختی ")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public double _Payed
        {
            get
            {

                decimal sumOfPrices = 0;

                foreach (var item in T_ConsumersPays.ToList())
                {
                    sumOfPrices += item.Price;
                }


                return double.Parse (sumOfPrices.ToString());

            }


        }

        //[Display(Name = "وضعیت")]
        //public string _StateName { get { return T_FinancialStates.StateName; } }
    
    }
}
