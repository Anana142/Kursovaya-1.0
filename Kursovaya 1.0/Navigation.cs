using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Kursovaya_1._0
{
    class Navigation : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private Navigation() { }
        static Navigation instance;
        public static Navigation GetInstance()
        {
            if (instance == null)
                instance = new();
            return instance;
        }

        private Page currentPage;
        public Page CurrentPage { get => currentPage; set { currentPage = value; Signal(); } }
    }
}
