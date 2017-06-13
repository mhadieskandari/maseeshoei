using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaaseShooei.Areas.Management.Models
{
    public partial class T_BurdenInformations
    {
        [Display(Name="ID")]
        [Key]
        public long _BurdenInformationID { get  {return BurdenInformationID; }  set  { BurdenInformationID=value; } } 

        [Display(Name = "ماشین")]
        public Nullable<int> _TruckID { get { return TruckID; } set { TruckID = value; } }

        [Display(Name = "ماشین")]
        public string _TruckNumber { get { return T_Trucks._Number; } }

        
        [Display(Name = "تاریخ ساعت بارگیری")]
        [Required]
        public string _LoadDateTime { get { return LoadDateTime.Substring(0,17); } set { LoadDateTime = value; } }

        //[Display(Name = "تاریخ ساعت بارگیری")]
        //public DateTime _ENLoadDateTime { get { return  PersianDateTime.Parse( LoadDateTime).ToDateTime();  }  }


        [Required]
        [DisplayFormat(DataFormatString = "{0:0,0}", ApplyFormatInEditMode = true)]
        [Display(Name = "وزن پر بارگیری")]
        public Nullable<float> _LoadFullTruckWeight { get { return LoadFullTruckWeight; } set { LoadFullTruckWeight = value; } }

        [Required]
        [DisplayFormat(DataFormatString = "{0:0,0}", ApplyFormatInEditMode = true)]
        [Display(Name = "وزن خالی بارگیری")]
        public Nullable<float> _LoadFreeTruckWeight { get { return LoadFreeTruckWeight; } set { LoadFreeTruckWeight = value; } }


        [Required]
        [Display(Name = "تاریخ ساعت تخلیه")]
        public string _UnLoadDateTime { get { return UnLoadDateTime.Substring(0, 17); } set { UnLoadDateTime = value; } }

        //[Display(Name = "تاریخ ساعت تخلیه")]
        //public DateTime _ENUnLoadDateTime { get { return PersianDateTime.Parse( UnLoadDateTime).ToDateTime(); }  }


        [Required]
        [DisplayFormat(DataFormatString = "{0:0,0}", ApplyFormatInEditMode = true)]
        [Display(Name = "وزن پر تخلیه")]
        public Nullable<float> _UnLoadFullTruckWeight { get { return UnLoadFullTruckWeight; } set { UnLoadFullTruckWeight = value; } }

        [Required]
        [DisplayFormat(DataFormatString = "{0:0,0}", ApplyFormatInEditMode = true)]
        [Display(Name = "وزن خالی تخلیه")]
        public Nullable<float> _UnLoadFreeTruckWeight { get { return UnLoadFreeTruckWeight; } set { UnLoadFreeTruckWeight = value; } }

       
        [Display(Name = "کاربر ثبت کننده")]
        public Nullable<int> _UserID { get { return UserID; } set { UserID = value; } }

        [Display(Name = "رسید بارگیری")]
        public string _ResidLoadNumber { get { return ResidLoudNumber; } set { ResidLoudNumber = value; } }

        [Display(Name = "رسید تخلیه")]
        public string _ResidUnLoadNumber { get { return ResidUnLoudNumber; } set { ResidUnLoudNumber = value; } }

        [Display(Name = "تاریخ ثبت")]
        public string _RegisterDateTime { get { return RegisterDateTime; } set { RegisterDateTime = value; } }

        //[Display(Name = "تاریخ ثبت")]
        //public DateTime _ENRegisterDateTime { get { return PersianDateTime.Parse(RegisterDateTime).ToDateTime(); } }


        [Display(Name="انتخاب")]
        public bool _selected { get; set; }


        
        [DisplayFormat(DataFormatString = "{0:0,0}", ApplyFormatInEditMode = true)]
        [Display(Name = "وزن خالص تخلیه")]
        public Nullable<float> _UnLoadPureWeight { get { return UnLoadFullTruckWeight - UnLoadFreeTruckWeight; } }


        
        [DisplayFormat(DataFormatString = "{0:0,0}", ApplyFormatInEditMode = true)]
        [Display(Name = "وزن خالص بارگیری")]
        public Nullable<float> _LoadPureWeight { get { return LoadFullTruckWeight - LoadFreeTruckWeight; } }

        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        [Display(Name = "قیمت کل خریدار وزن تخلیه")]
        public  Nullable<decimal> _UnloadBurdenConsumerPrice
        {
            get
            {
                return decimal.Parse(_UnLoadPureWeight.ToString()) * T_ConsumerProducePrices.Price / 1000;
            }
        }
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        [Display(Name = "قیمت کل تولیدکننده وزن تخلیه")]
        //[DataType(DataType.Currency)]
        public Nullable<decimal> _UnloadBurdenProducerPrice
        {
            get
            {
                return decimal.Parse((_UnLoadPureWeight).ToString()) * T_ProducerProducePrices.Price / 1000;
            }
        }
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        [Display(Name = "قیمت کل خریدار وزن بارگیری")]
        //[DataType(DataType.Currency)]
        public Nullable<decimal> _loadBurdenConsumerPrice
        {
            get
            {
                return decimal.Parse((_LoadPureWeight).ToString()) * T_ConsumerProducePrices.Price / 1000;
            }
        }

        
        
        [Display(Name = "قیمت کل تولیدکننده وزن بارگیری")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Currency)]
        public Nullable<decimal> _loadBurdenProducerPrice
        {
            get
            {
                return decimal.Parse((_LoadPureWeight).ToString()) * T_ProducerProducePrices.Price / 1000;
            }
        }

        [Display(Name = "مبلغ کل کرایه")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
       // [DataType(DataType.Currency)]
        public Nullable<decimal> _TotalTransportPrice
        {
            get
            {
                return decimal.Parse((_UnLoadPureWeight).ToString()) * T_TransportPrices.Price / 1000;
            }
        }

    }
}