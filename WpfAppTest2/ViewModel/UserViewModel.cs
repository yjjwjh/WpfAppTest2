using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WpfAppTest2.db;
using WpfAppTest2.Models;

namespace WpfAppTest2.ViewModel
{
    public class UserViewModel : ViewModelBase
    {
        private Student model;

        LocalDb localDb;
        public UserViewModel(Student stu)
        {
            model=stu;
            localDb = LocalDb.GetInstanse();
            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);
        }
        public Student Model 
        {
            get { return model; }
            set { model = value;RaisePropertyChanged(); }
        }
        private delegate void MyDelegate();

        private ObservableCollection<Student> gridModelList;
        public ObservableCollection<Student> GridModelList
        {
            get { return gridModelList; }
            set { gridModelList = value; RaisePropertyChanged(); }
        }
        #region Command
        //确定按钮
        public RelayCommand ConfirmCommand { get; set; }
        //取消按钮
        public RelayCommand CancelCommand { get; set; }
        #endregion
        //确定
        public void Confirm()
        {
            DialogResult = true;
        }
        //取消
        public void Cancel()
        {
            DialogResult = false;

        }
        private bool? _dialogResult;

        public bool? DialogResult
        {
            get { return _dialogResult; }
            set
            {
                if (_dialogResult!=value)
                {
                    _dialogResult = value;
                    RaisePropertyChanged(nameof(DialogResult));
                }
            
            }
        }

    }
}
