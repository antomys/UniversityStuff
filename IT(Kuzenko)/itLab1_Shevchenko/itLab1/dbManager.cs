using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itLab1
{
    class dbManager
    {
        DataBase db;

        public bool CreateDB(string dbname)
        {
            if (dbname.Equals("")) return false;

            db = new DataBase(dbname);
            return true;
        }

        public bool AddTable(string tname)
        {
            if (tname.Equals("")) return false;
            if (db == null) return false;

            db.dbTablesList.Add(new Table(tname));
            return true;
        }

        public Table GetTable(int index)
        {
            if (index == -1) index = 0;
            return db.dbTablesList[index];
        }

        public bool AddColumn(int tIndex, string cname, string ctype)
        {
            if (db == null) return false;
            if (db.dbTablesList.Count <= 0) return false;

            db.dbTablesList[tIndex].tColumnsList.Add(new Column(cname, ctype));
            for(int i = 0; i < db.dbTablesList[tIndex].tRowsList.Count; ++i)
            {
                db.dbTablesList[tIndex].tRowsList[i].rValuesList.Add("");
            }
            return true;
        }

        public bool AddRow(int tIndex)
        {
            if (db == null) return false;
            if (db.dbTablesList.Count <= 0) return false;
            if (db.dbTablesList[tIndex].tColumnsList.Count <= 0) return false;

            db.dbTablesList[tIndex].tRowsList.Add(new Row());
            for (int i = 0; i < db.dbTablesList[tIndex].tColumnsList.Count; ++i)
            {
                db.dbTablesList[tIndex].tRowsList.Last().rValuesList.Add("");
            }
            return true;
        }

        public bool ChangeValue(string newValue, int tind, int cind, int rind)
        {
            if (db.dbTablesList[tind].tColumnsList[cind].cType.Validation(newValue))
            {
                db.dbTablesList[tind].tRowsList[rind].rValuesList[cind] = newValue;
                return true;
            }

            return false;
        }

        public void DeleteRow(int tind, int rind)
        {
            db.dbTablesList[tind].tRowsList.RemoveAt(rind);
        }

        public void DeleteColumn(int tind, int cind)
        {
            db.dbTablesList[tind].tColumnsList.RemoveAt(cind);
            for (int i = 0; i < db.dbTablesList[tind].tRowsList.Count; ++i)
            {
                db.dbTablesList[tind].tRowsList[i].rValuesList.RemoveAt(cind);
            }
        }

        public void DeleteTable(int tind)
        {
            db.dbTablesList.RemoveAt(tind);
        }

        char sep = '$';
        char space = '#';
        public void SaveDB(string path)
        {
            StreamWriter sw = new StreamWriter(path);

            sw.WriteLine(db.dbName);
            foreach(Table t in db.dbTablesList)
            {
                sw.WriteLine(sep);
                sw.WriteLine(t.tName);
                foreach(Column c in t.tColumnsList)
                {
                    sw.Write(c.cName + space);
                }
                sw.WriteLine();
                foreach (Column c in t.tColumnsList)
                {
                    sw.Write(c.typeName + space);
                }
                sw.WriteLine();
                foreach (Row r in t.tRowsList)
                {
                    foreach (string s in r.rValuesList)
                    {
                        sw.Write(s + space);
                    }
                    sw.WriteLine();
                }
            }

            sw.Close();
        }

        public void OpenDB(string path)
        {
            StreamReader sr = new StreamReader(path);
            string file = sr.ReadToEnd();
            string[] parts = file.Split(sep);

            db = new DataBase(parts[0]);

            for (int i = 1; i < parts.Length; ++i)
            {
                parts[i] = parts[i].Replace("\r\n", "\r");
                List<string> buf = parts[i].Split('\r').ToList();
                buf.RemoveAt(0);
                buf.RemoveAt(buf.Count - 1);

                if (buf.Count > 0)
                {
                    db.dbTablesList.Add(new Table(buf[0]));
                }
                if (buf.Count > 2)
                {
                    string[] cname = buf[1].Split(space);
                    string[] ctype = buf[2].Split(space);
                    int length = cname.Length - 1;
                    for (int j = 0; j < length; ++j)
                    {
                        db.dbTablesList[i-1].tColumnsList.Add(new Column(cname[j], ctype[j]));
                    }

                    for (int j = 3; j < buf.Count; ++j)
                    {
                        db.dbTablesList[i-1].tRowsList.Add(new Row());
                        List<string> values = buf[j].Split(space).ToList();
                        values.RemoveAt(values.Count - 1);
                        db.dbTablesList[i-1].tRowsList.Last().rValuesList.AddRange(values);
                    }
                }
            }

            sr.Close();
        }

        public List<string> GetTableNameList()
        {
            List<string> res = new List<string>();
            foreach (Table t in db.dbTablesList)
                res.Add(t.tName);
            return res;
        }
    }
}
