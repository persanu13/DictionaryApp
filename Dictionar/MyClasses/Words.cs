using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace Dictionar.MyClasses
{
    [Serializable]
    public class Words
    {
        [XmlArray]
        public ObservableCollection<Word> list_Words { get; set; }

        [XmlIgnore]
        public ObservableCollection<Word> m_Words { get; set; }
        [XmlIgnore]
        public ObservableCollection<string> m_Category {  get; set; }
        public Words()
        {
            list_Words = new ObservableCollection<Word>();
            m_Words = new ObservableCollection<Word>();
            m_Category = new ObservableCollection<string>();
        }
        public void readElements()
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(Words));
            FileStream file = new FileStream("words.xml", FileMode.Open);
            Words reader = (xmlser.Deserialize(file) as Words);
            list_Words = new ObservableCollection<Word>(reader.list_Words);
            m_Words = new ObservableCollection<Word>(reader.list_Words);
            foreach (var word in m_Words)
            {
                if (!m_Category.Contains(word.Category))
                {
                    m_Category.Add(word.Category);
                }
            }
            m_Category = new ObservableCollection<string>(m_Category.Order());
            m_Category.Insert(0,"None");
            file.Dispose();
        }
        public void InitialState()
        {
            m_Words = new ObservableCollection<Word>(list_Words);
        }
        public void writeElements()
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(Words));
            FileStream file = new FileStream("words.xml", FileMode.Create);
            xmlser.Serialize(file, this);
            file.Dispose();
        }
        public void AddWord(Word word)
        {
            list_Words.Add(word);
            if (!m_Category.Contains(word.Category))
            {
                m_Category.Add(word.Category);
            }
            list_Words = new ObservableCollection<Word>(list_Words.OrderBy(w => w.Name));
            m_Words.Clear();
            foreach (var l_word in list_Words)
            {
                m_Words.Add(l_word);
            }
            writeElements();
        }
        public void RemoveWord(string wordName)
        {
            Word deleted_word = new Word();
            foreach (var word in list_Words)
            {
               if(word.Name == wordName)
                {
                    deleted_word = word;
                }
            }
            list_Words.Remove(deleted_word);
            m_Words.Remove(deleted_word);
            if(deleted_word.Image != "None")
            {
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string destinationFolder = System.IO.Path.Combine(appDirectory, "Images");
                string filePath = System.IO.Path.Combine(destinationFolder, deleted_word.Image);
                File.Delete(filePath);
            }
            writeElements();
        }
        public bool Have(string wordName)
        {
            foreach (var word in list_Words)
            {
                if (word.Name.ToLower() == wordName.ToLower())
                { 
                    return true;
                }
            }
            return false;
        }
        public Word GetWord(string wordName)
        {
            foreach (var word in list_Words)
            {
                if (word.Name.ToLower() == wordName.ToLower())
                {
                    return word;
                }
            }
            return null;
        }
        public void filter(string filterString,string Category = null)
        {
            var filteredWords = new ObservableCollection<Word>(list_Words.Where(w => w.Name.StartsWith(filterString, StringComparison.CurrentCultureIgnoreCase)));
            m_Words.Clear();
            foreach (var word in filteredWords)
            {
                if(Category == null || Category == "None" || word.Category == Category)
                {
                    m_Words.Add(word);
                }
            }
        }
    }
}
