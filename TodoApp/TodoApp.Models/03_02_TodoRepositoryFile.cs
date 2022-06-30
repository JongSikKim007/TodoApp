using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TodoApp.Models
{
    public class TodoRepositoryFile : ITodoRepository
    {
        private readonly string _filePath;
        private static List<Todo> _todos = new List<Todo>();
        /*public TodoRepositoryFile()
        {
            _todos = new List<Todo>
            {
                new Todo { Id = 1, Title = "ASP.NET Core 학습", IsDone = false }, // 이런 형식을 개체 리터럴이라고 함
                new Todo { Id = 2, Title = "Blazer 학습", IsDone = false },
                new Todo { Id = 3, Title = "C# 학습", IsDone = true }
            };
        }
        */
        public TodoRepositoryFile(string filePath = @"C:\TodoApp\Todos.txt") 
        {
            this._filePath = filePath;
           string[] todos = File.ReadAllLines(filePath, Encoding.Default);
            foreach (var t in todos)
            {
                string[] line = t.Split(',');
                _todos.Add(new Todo { Id = Convert.ToInt32(line[0]), Title = line[1], IsDone = Convert.ToBoolean(line[2]) });
            }
        }

       public void Add(Todo model)
        {
            model.Id = _todos.Max(t => t.Id) + 1;
            _todos.Add(model);

            //파일로 저장
            string data = "";
            foreach (var t in _todos)
            {
                data += $"{t.Id},{t.Title},{t.IsDone}{Environment.NewLine}"; //한 줄 씩 줄 바꿈 \r\n과 같음
            }
            using (StreamWriter  sw = new StreamWriter(_filePath))
            {
                sw.Write(data);
                sw.Close();
                sw.Dispose();
            }

        }

        public List<Todo> GetAll()
        {
            return _todos.ToList();
        }
    }


}
