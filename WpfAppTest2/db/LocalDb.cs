using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppTest2.Models;

namespace WpfAppTest2.db
{
    public class LocalDb
    {
        public LocalDb() 
        {
            Init();
        
        }
        private static LocalDb _localDb;
        public static LocalDb GetInstanse()
        {
            if (_localDb==null)
            {
                _localDb = new LocalDb();
            }
            return _localDb;
        }

        private List<Student> Students;

        private void Init()
        {
            Students = new List<Student>();
            for (int i=0;i<30;i++)
            {
                Students.Add(new Student()
                {
                    Id = i,
                    Name=$"Sample{i}"
                }) ;
            }
        }

        //查询所有
        public List<Student> GetStudents()
        {
            return Students;
        }
        //增加
        public void AddStudent(Student stu) 
        {
            Students.Add(stu);
            
        }
        //修改
        public void UpdateStudent(Student stu)
        {
            Students.Where(q => q.Id == stu.Id).First().Name = stu.Name;
        
        }

        //删除
        public void DelStudent(int id) 
        {
            var model= Students.FirstOrDefault(t => t.Id == id);
            if (model!=null) 
            {
                Students.Remove(model);
            
            }
        }
        //根据名称模糊查找
        public List<Student> GetStudentsByName(string name) 
        {
            return Students.Where(q => q.Name.Contains(name)).ToList();
        }

        public Student GetStudentById(int id)
        {
            var model=Students.FirstOrDefault(t => t.Id == id);
            if (model!=null) 
            {
                return new Student()
                {
                    Id = model.Id,
                    Name = model.Name
                };
            }
            return null;
        }

    }
}
