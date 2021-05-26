using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAppTest2.Models;
using WpfAppTest2.ViewModel;

namespace WpfAppTest2.Views
{
    /// <summary>
    /// UserView.xaml 的交互逻辑
    /// </summary>
    public partial class UserView : Window
    {
        private Student model;
        public UserView(Student stu)
        {
            InitializeComponent();
            model = stu;
            UserViewModel userViewModel = new UserViewModel(model);
            this.DataContext = userViewModel;
        }

       
    }
}
