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
        //  ��ʾһ����̬���ݼ��ϣ��������Ƴ����ˢ�������б�ʱ���˼��Ͻ��ṩ֪ͨ
        private ObservableCollection<Student> gridModelList;

        public ObservableCollection<Student> GridModelList
        {
            get { return gridModelList; }
            set { gridModelList = value;RaisePropertyChanged(); }
        }
        #region Command
        public RelayCommand QueryCommand { get; set; }//��ѯ��ť

        public RelayCommand ResetCommand { get; set; }//���ð�ť

        public RelayCommand<int> EditCommand { get; set; }//�޸İ�ť

        public RelayCommand<int> DelCommand { get; set; }//ɾ����ť
        public RelayCommand AddCommand { get; set; }//������ť
        #endregion

        //��ѯ
        public void Query()
        {
            var models = localDb.GetStudentsByName(Search);
            GridModelList = new ObservableCollection<Student>();
            if (models!=null)
            {
                //���ݷ��붯̬���ݼ���  ��ʾһ����̬���ݼ��ϣ��������Ƴ����ˢ�������б�ʱ���˼��Ͻ��ṩ֪ͨ
                models.ForEach(arg =>
                {
                    GridModelList.Add(arg);
                });
            }
        }
        //�޸�
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
        //ɾ��
        public void Del(int id)
        {
            var model = localDb.GetStudentById(id);
            if (model!=null) 
            {
                var r = MessageBox.Show($"ȷ��ɾ����ǰ�û�:{model.Name}","������ʾ",MessageBoxButton.OK,MessageBoxImage.Question);
                if (r == MessageBoxResult.OK)
                    localDb.DelStudent(model.Id);
                this.Query();
            }
        }
        //����
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