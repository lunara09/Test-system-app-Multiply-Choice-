using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTests.MyClasses
{
    [Serializable]
    public partial class FLAGLAR
    {
        public int IdQuestion { get; set; }
        public int IdUserAnswer { get; set; }
        public List<bool> UserAnswerText { get; set; }
        public string textQ { get; set; }
        //public List<int> order { get; set; }
    }


    [Serializable]
    public partial class Answerss
    {
        public int id { get; set; }
        public int QuestionId { get; set; }
        public List<Answerss> orderAnswera { get; set; }
        public string AnswerText { get; set; }
        public bool IsAnswerCorrect { get; set; }
    }
    [Serializable]
    public partial class Proverka
    {
        public string textO { get; set; }
        public bool IsChoosed { get; set; }

    }

    [Serializable]
    public partial class ResultClassMy
    {
        public int QuestionId { get; set; }
        public bool Result { get; set; }
    }

    [Serializable]
    public partial class Questionss
    {
        public int ID { get; set; }
        public List<Answerss> answerss { get; set; }
        public string QuestName { get; set; }
        public int? enumq { get; set; }

    }

    [Serializable]
    public class ResultClass
    {
        public int QuestionId { get; set; }
        public bool Result { get; set; }
    }
    public class classOrder
    {
        public int order { get; set; }
    }


}