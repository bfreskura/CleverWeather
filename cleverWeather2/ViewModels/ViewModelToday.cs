using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel;


namespace cleverWeather2
{

    [DataContract]
    class ViewModelToday : INotifyPropertyChanged
    {
        /*
         * Icons and city*/
        private string _clothesTorso;
        [DataMember]
        public string ClothesTorso
        {
            get { return _clothesTorso; }
            set { _clothesTorso = value;
            NotifyPropertyChanged("ClothesTorso");
            }
        }
        private string _clothesLegs;
        [DataMember]
        public string ClothesLegs
        {
            get { return _clothesLegs; }
            set { _clothesLegs = value;
            NotifyPropertyChanged("ClothesLegs");
            }
        }
        private string _clothesShoes;
        [DataMember]
        public string ClothesShoes
        {
            get { return _clothesShoes; }
            set { _clothesShoes = value;
            NotifyPropertyChanged("ClothesShoes");
            }
        }
        private string _clothesAccessori1;
        [DataMember]
        public string ClothesAccessori1
        {
            get { return _clothesAccessori1; }
            set { _clothesAccessori1 = value;
            NotifyPropertyChanged("ClothesAccessori1");
            }
        }
        private string _clothesAccessori2;
        [DataMember]
        public string ClothesAccessori2
        {
            get { return _clothesAccessori2; }
            set { _clothesAccessori2 = value;
            NotifyPropertyChanged("ClothesAccessori2");
            }
        }
        private string _clothesAccessori3;
        [DataMember]
        public string ClothesAccessori3
        {
            get { return _clothesAccessori3; }
            set { _clothesAccessori3 = value;
            NotifyPropertyChanged("ClothesAccessori3");
            }
        }

        private string _city;
        [DataMember]
        public string City
        {
            get { return _city; }
            set { _city = value;
            NotifyPropertyChanged("City");
            }
        }

        /*Morning 
         * 
         * */

        private string _morningAccessori1;
        [DataMember]
        public string MorningAccessori1
        {
            get { return _morningAccessori1; }
            set { _morningAccessori1 = value;
            NotifyPropertyChanged("MorningAccessori1");
            }
        }
        private string _morningAccessori2;
        [DataMember]
        public string MorningAccessori2
        {
            get { return _morningAccessori2; }
            set { _morningAccessori2 = value;
            NotifyPropertyChanged("MorningAccessori2");
            }
        }
        private string _morningAccessori3;
        [DataMember]
        public string MorningAccessori3
        {
            get { return _morningAccessori3; }
            set { _morningAccessori3 = value;
            NotifyPropertyChanged("MorningAccessori3");
            }
        }


        private string _morningWeather;
        [DataMember]
        public string MorningWeather
        {
            get { return _morningWeather; }
            set { _morningWeather = value;
            NotifyPropertyChanged("MorningWeather");
            }
        }
        private int _morningTemperature;
        [DataMember]
        public int MorningTemperature
        {
            get { return _morningTemperature; }
            set { _morningTemperature = value;
            NotifyPropertyChanged("MorningTemperature");
            }
        }
        private int _morningPercentage;
        [DataMember]
        public int MorningPercentage
        {
            get { return _morningPercentage; }
            set { _morningPercentage = value;
            NotifyPropertyChanged("MorningPercentage");
            }
        }

        /*Afternoon
        * 
        * */

        private string _afternoonAccessori1;
        [DataMember]
        public string AfternoonAccessori1
        {
            get { return _afternoonAccessori1; }
            set { _afternoonAccessori1 = value;
            NotifyPropertyChanged("AfternoonAccessori1");
            }
        }
        private string _afternoonAccessori2;
        [DataMember]
        public string AfternoonAccessori2
        {
            get { return _afternoonAccessori2; }
            set { _afternoonAccessori2 = value;
            NotifyPropertyChanged("AfternoonAccessori2");
            }
        }
        private string _afternoonAccessori3;
        [DataMember]
        public string AfternoonAccessori3
        {
            get { return _afternoonAccessori3; }
            set { _afternoonAccessori3 = value;
            NotifyPropertyChanged("AfternoonAccessori3");
            }
        }


        private string _afternoonWeather;
        [DataMember]
        public string AfternoonWeather
        {
            get { return _afternoonWeather; }
            set { _afternoonWeather = value;
            NotifyPropertyChanged("AfternoonWeather");
            }
        }
        private int _afternoonTemperature;
        [DataMember]
        public int AfternoonTemperature
        {
            get { return _afternoonTemperature; }
            set { _afternoonTemperature = value;
            NotifyPropertyChanged("AfternoonTemperature");
            }
        }
        private int _afternoonPercentage;
        [DataMember]
        public int AfternoonPercentage
        {
            get { return _afternoonPercentage; }
            set { _afternoonPercentage = value;
            NotifyPropertyChanged("AfternoonPercentage");
            }
        }

        /*Night 
        * 
        * */

        private string _nightAccessori1;
        [DataMember]
        public string NightAccessori1
        {
            get { return _nightAccessori1; }
            set { _nightAccessori1 = value;
            NotifyPropertyChanged("NightAccessori1");
            }
        }
        private string _nightAccessori2;
        [DataMember]
        public string NightAccessori2
        {
            get { return _nightAccessori2; }
            set { _nightAccessori2 = value;
            NotifyPropertyChanged("NightAccessori2");
            }
        }
        private string _nightAccessori3;
        [DataMember]
        public string NightAccessori3
        {
            get { return _nightAccessori3; }
            set { _nightAccessori3 = value;
            NotifyPropertyChanged("NightAccessori3");
            }
        }

        private string _nightWeather;
        [DataMember]
        public string NightWeather
        {
            get { return _nightWeather; }
            set { _nightWeather = value;
            NotifyPropertyChanged("NightWeather");
            }
        }
        private int _nightTemperature;
        [DataMember]
        public int NightTemperature
        {
            get { return _nightTemperature; }
            set { _nightTemperature = value;
            NotifyPropertyChanged("NightTemperature");
            }
        }
        private int _nightPercentage;
        [DataMember]
        public int NightPercentage
        {
            get { return _nightPercentage; }
            set { _nightPercentage = value;
            NotifyPropertyChanged("NightPercentage");
            }
        }


        /*
         * Property Changed Method
         This includes adding a PropertyChanged event and implementing a method called NotifyPropertyChanged, which raises the PropertyChanged event.
         */
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (null != PropertyChanged)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
