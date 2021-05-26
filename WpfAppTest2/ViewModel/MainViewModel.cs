using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WpfAppTest2.db;
using WpfAppTest2.Models;
using WpfAppTest2.Views;

namespace WpfAppTest2.ViewModel
{
   
    public class MainViewModel : ViewModelBase
    {
        
        public MainViewModel()
        {
            localDb = LocalDb.GetInstanse();
            QueryCommand = new RelayCommand(Query);
            ResetCommand = new RelayCommand(()=> 
            {
                Search = string.Empty;
                this.Query();
            
            });
            EditCommand = new RelayCommand<int>(t=>Edit(t));
            DelCommand = new RelayCommand<int>(t => Del(t));
            AddCommand = new RelayCommand(Add);
        }
       

        LocalDb localDb;

        private string search=string.Empty;

        public string Search 
        {
            get { return search; }
            set { search = value;RaisePropertyChanged(); }
        }
        //  表示一个动态数据集合，在添加项、移除项或刷新整个列表时，此集合将提供通知
        private ObservableCollection<Student> gridModelList;

        public ObservableCollection<Student> GridModelList
        {
            get { return gridModelList; }
            set { gridModelList = value;RaisePropertyChanged(); }
        }
        #region Command
        public RelayCommand QueryCommand { get; set; }//查询按钮

        public RelayCommand ResetCommand { get; set; }//重置按钮

        public RelayCommand<int> EditCommand { get; set; }//修改按钮

        public RelayCommand<int> DelCommand { get; set; }//删除按钮
        public RelayCommand AddCommand { get; set; }//新增按钮
        #endregion

        //查询
        public void Query()
        {
            var models = localDb.GetStudentsByName(Search);
            GridModelList = new ObservableCollection<Student>();
            if (models!=null)
            {
                //数据放入动态数据集合  表示一个动态数据集合，在添加项、移除项或刷新整个列表时，此集合将提供通知
                models.ForEach(arg =>
                {
                    GridModelList.Add(arg);
                });
            }
        }
        //修改
        public void Edit(int id) 
        {
           var model= localDb.GetStudentById(id);
            if (model != null)
            {
                UserView userView = new UserView(model);
                var r= userView.ShowDialog();
                if (r.Value)
                {
                    var newModel = GridModelList.FirstOrDefault(t=>t.Id== model.Id);
                    if (newModel!=null)
                    {
                        newModel.Name = model.Name;
                    }
                }
            }
        }
        //删除
        public void Del(int id)
        {
            var model = localDb.GetStudentById(id);
            if (model!=null) 
            {
                var r = MessageBox.Show($"确认删除当前用户:{model.Name}","操作提示",MessageBoxButton.OK,MessageBoxImage.Question);
                if (r == MessageBoxResult.OK)
                    localDb.DelStudent(model.Id);
                this.Query();
            }
        }
        //增加
        public void Add()
        {
            Student stu = new Student();
            UserView userView = new UserView(stu);
            var r = userView.ShowDialog();
            if (r.Value)
            {
                stu.Id = GridModelList.Max(t=>t.Id)+1;
                localDb.AddStudent(stu);
                this.Query();
            }

        }
    }
}