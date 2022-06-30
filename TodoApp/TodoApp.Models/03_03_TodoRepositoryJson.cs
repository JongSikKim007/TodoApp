using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TodoApp.Models
{
    public class TodoRepositoryJson : ITodoRepository
    {
        private readonly string _filePath;
        private static List<Todo> _todos = new List<Todo>();

        public TodoRepositoryJson(string filePath = @"C:\\TodoApp\\Todos.json")
        {
            this._filePath = filePath;
            var todos = File.ReadAllText(filePath, Encoding.Default);
            _todos = JsonConvert.DeserializeObject<List<Todo>>(todos); // DeserializeObject키워드는 파일 문자열을 모두를 받아 C#의 컬랙션으로 변경
        }  // serializeObject키워드는 C#의 컬랙션을 모두를 받아 파일 문자열로 변경

        public void Add(Todo model)
        {
            model.Id = _todos.Max(t => t.Id) + 1;
            _todos.Add(model);

            //JSON 파일로 저장
            string json = JsonConvert.SerializeObject(_todos, Formatting.Indented); // Indented들여쓰기 맞춤
            File.WriteAllText(_filePath, json);
        }

        public List<Todo> GetAll()
        {
            return _todos.ToList();
        }
    }


}
